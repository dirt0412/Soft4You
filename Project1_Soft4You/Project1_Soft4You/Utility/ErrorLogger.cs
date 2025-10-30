
namespace Project1_Soft4You.Utility
{
    public static class ErrorLogger
    {
        // loguj komunikat + opcjonalnie wyjątek
        public static void Log(string message, Exception ex = null)
        {            
            try
            {
                using (var conn = Helper.GetDBConnection())
                {
                    if (conn == null) return;

                    const string sql = @"INSERT INTO dbo.app_log (message, details)
                                         VALUES (@m, @d);";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@m", SqlDbType.NVarChar, 4000).Value = (object?)message ?? "";
                        cmd.Parameters.Add("@d", SqlDbType.NVarChar, -1).Value = (object?)ex?.ToString() ?? DBNull.Value;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }//błędy logowania pomijamy
        }

        public static void Log(Exception ex) => Log(ex?.Message, ex);
    }
}
