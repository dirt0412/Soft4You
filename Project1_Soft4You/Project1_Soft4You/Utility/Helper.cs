using System.Configuration;

namespace Project1_Soft4You.Utility
{
    public static class Helper
    {
        public static SqlConnection GetDBConnection()
        {
            try
            {
                var connStringSetting = ConfigurationManager.ConnectionStrings["DbBase"];
                string connString = connStringSetting?.ConnectionString ?? throw new InvalidOperationException("Brak connection stringa 'DbBase' w App.config.");

                var conn = new SqlConnection(connString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd połączenia z bazą danych:\n{ex.Message}",
                    "Błąd połączenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
