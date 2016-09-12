using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHealth
{

    class Database

    {
        const string CONN_STRING = @"Data Source=ipd8vs.database.windows.net;Initial Catalog=ProjectHealthDB;Integrated Security=False;User ID=sqladmin;Password=IPD8rocks!;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

          
       

        private SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(CONN_STRING);
            conn.Open();
        }
    }
}
