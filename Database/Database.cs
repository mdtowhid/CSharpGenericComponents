using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DatabaseInfo
    {
        static string ConnectionString = "Data Source=DESKTOP-8KAS1U3;Initial Catalog=TeamCodeDb;Integrated Security=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        public static SqlCommand GetSqlCommand(string query, SqlConnection conn)
        {
            return new SqlCommand(query, conn);
        }
    }
}
