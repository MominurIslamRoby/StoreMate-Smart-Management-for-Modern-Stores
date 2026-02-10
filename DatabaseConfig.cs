using System.Data.SqlClient;

namespace IMS
{
    public static class DatabaseConfig
    {
        // 🔹 Only change this ONE line to update the whole project
        public static string ConnString = @"Data Source=.\SQLEXPRESS;Initial Catalog=StoreMateDB;Integrated Security=True;Encrypt=False";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnString);
        }
    }
}