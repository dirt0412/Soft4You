using Project1_Soft4You.Models;
using Project1_Soft4You.Utility;

namespace Project1_Soft4You.Data
{
    public class KlienciRepository
    {
        private static object DbNullIfEmpty(string s) => string.IsNullOrWhiteSpace(s) ? DBNull.Value : s;
        public int GetCount(string filterNazwa, string filterNip, string filterTel, string filterEmail)
        {
            using (var con = Helper.GetDBConnection())
            using (var cmd = new SqlCommand(@"
                    SELECT COUNT(*) FROM dbo.klienci
                    WHERE (@nazwa IS NULL OR nazwa LIKE '%' + @nazwa + '%')
                      AND (@nip IS NULL OR nip LIKE '%' + @nip + '%')
                      AND (@tel IS NULL OR nr_tel LIKE '%' + @tel + '%')
                      AND (@mail IS NULL OR email LIKE '%' + @mail + '%');", con))
            {
                cmd.Parameters.Add("@nazwa", SqlDbType.NVarChar, 200).Value = DbNullIfEmpty(filterNazwa);
                cmd.Parameters.Add("@nip", SqlDbType.NVarChar, 10).Value = DbNullIfEmpty(filterNip);
                cmd.Parameters.Add("@tel", SqlDbType.NVarChar, 30).Value = DbNullIfEmpty(filterTel);
                cmd.Parameters.Add("@mail", SqlDbType.NVarChar, 320).Value = DbNullIfEmpty(filterEmail);

                return (int)cmd.ExecuteScalar();
            }
        }

        public DataTable GetPage(int page, int pageSize,
            string filterNazwa, string filterNip, string filterTel, string filterEmail, string orderBy, bool desc)
        {
            if (page < 1) page = 1;
            int offset = (page - 1) * pageSize;

            // kolumny po ktorych sortujemy
            string orderColumn = (orderBy?.ToLowerInvariant()) switch
            {
                "nazwa" => "nazwa",
                _ => "nazwa"
            };
            string direction = desc ? "DESC" : "ASC";

            string sql = $@"
                    SELECT klient_id, nazwa, nip, adres, nr_tel, email, created_at, updated_at
                    FROM dbo.klienci
                    WHERE (@nazwa IS NULL OR nazwa LIKE '%' + @nazwa + '%')
                      AND (@nip   IS NULL OR nip   LIKE '%' + @nip   + '%')
                      AND (@tel   IS NULL OR nr_tel LIKE '%' + @tel  + '%')
                      AND (@mail  IS NULL OR email LIKE '%' + @mail  + '%')
                    ORDER BY {orderColumn} {direction}, klient_id ASC
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

            var dt = new DataTable();
            using (var con = Helper.GetDBConnection())
            using (var da = new SqlDataAdapter(sql, con))
            {
                da.SelectCommand.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
                da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                da.SelectCommand.Parameters.Add("@nazwa", SqlDbType.NVarChar, 200).Value = DbNullIfEmpty(filterNazwa);
                da.SelectCommand.Parameters.Add("@nip", SqlDbType.NVarChar, 10).Value = DbNullIfEmpty(filterNip);
                da.SelectCommand.Parameters.Add("@tel", SqlDbType.NVarChar, 30).Value = DbNullIfEmpty(filterTel);
                da.SelectCommand.Parameters.Add("@mail", SqlDbType.NVarChar, 320).Value = DbNullIfEmpty(filterEmail);

                da.Fill(dt);
            }
            return dt;
        }

        public bool DeleteById(int klientId)
        {
            const string sql = "DELETE FROM dbo.klienci WHERE klient_id = @id;";
            using (var con = Helper.GetDBConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = klientId });
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public KlientDto GetById(int id)
        {
            const string sql = @"
                    SELECT klient_id, nazwa, nip, adres, nr_tel, email, rowver
                    FROM dbo.klienci
                    WHERE klient_id = @id;";

            using (var con = Helper.GetDBConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
                using (var r = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!r.Read()) return null;
                    return new KlientDto
                    {
                        KlientId = r.GetInt32(r.GetOrdinal("klient_id")),
                        Nazwa = r.GetString(r.GetOrdinal("nazwa")),
                        Nip = r.GetString(r.GetOrdinal("nip")),
                        Adres = r.GetString(r.GetOrdinal("adres")),
                        NrTel = r.GetString(r.GetOrdinal("nr_tel")),
                        Email = r.GetString(r.GetOrdinal("email")),
                        RowVer = (byte[])r["rowver"]
                    };
                }
            }
        }

        public int Insert(KlientDto k)
        {
            const string sql = @"
                INSERT INTO dbo.klienci (nazwa, nip, adres, nr_tel, email)
                VALUES (@nazwa, @nip, @adres, @tel, @mail);
                DECLARE @newId int = SCOPE_IDENTITY();
                SELECT @newId AS klient_id, rowver FROM dbo.klienci WHERE klient_id = @newId;";

            using (var con = Helper.GetDBConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@nazwa", SqlDbType.NVarChar, 200).Value = k.Nazwa ?? "";
                cmd.Parameters.Add("@nip", SqlDbType.NVarChar, 10).Value = k.Nip ?? "";
                cmd.Parameters.Add("@adres", SqlDbType.NVarChar, 300).Value = k.Adres ?? "";
                cmd.Parameters.Add("@tel", SqlDbType.NVarChar, 30).Value = k.NrTel ?? "";
                cmd.Parameters.Add("@mail", SqlDbType.NVarChar, 320).Value = k.Email ?? "";

                using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!rdr.Read())
                        throw new InvalidOperationException("Insert nie zwrócił nowego rekordu.");

                    k.KlientId = rdr.GetInt32(0);
                    k.RowVer = (byte[])rdr[1];
                    return k.KlientId;
                }
            }
        }


        public bool Update(KlientDto k)
        {
            const string sql = @"
                    UPDATE dbo.klienci
                       SET nazwa = @nazwa,
                           nip   = @nip,
                           adres = @adres,
                           nr_tel= @tel,
                           email = @mail
                    WHERE klient_id = @id AND rowver = @rowver;

                    IF @@ROWCOUNT = 1
                        SELECT rowver FROM dbo.klienci WHERE klient_id = @id;
                    ELSE
                        SELECT CAST(NULL AS varbinary(8)) AS rowver;";

            using (var con = Helper.GetDBConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = k.KlientId;
                cmd.Parameters.Add("@rowver", SqlDbType.Binary, 8).Value = (object?)k.RowVer ?? DBNull.Value;

                cmd.Parameters.Add("@nazwa", SqlDbType.NVarChar, 200).Value = k.Nazwa ?? "";
                cmd.Parameters.Add("@nip", SqlDbType.NVarChar, 10).Value = k.Nip ?? "";
                cmd.Parameters.Add("@adres", SqlDbType.NVarChar, 300).Value = k.Adres ?? "";
                cmd.Parameters.Add("@tel", SqlDbType.NVarChar, 30).Value = k.NrTel ?? "";
                cmd.Parameters.Add("@mail", SqlDbType.NVarChar, 320).Value = k.Email ?? "";

                var newRvObj = cmd.ExecuteScalar();
                if (newRvObj == DBNull.Value || newRvObj == null)
                    return false; // konflikt współbieżności

                k.RowVer = (byte[])newRvObj;
                return true;
            }
        }


    }
}
