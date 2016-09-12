using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHealth
{

    class Database
<<<<<<< HEAD
    {
        const string CONN_STRING = @"Data Source=ipd8vs.database.windows.net;Initial Catalog=ProjectHealthDB;Integrated Security=False;User ID=sqladmin;Password=IPD8rocks!;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
=======
    {       
        const string CONN_STRING = @"Data Source = ipd8vs.database.windows.net; Initial Catalog = ProjectHealthDB; Integrated Security = False; User ID = sqladmin; Password=IPD8rocks!;Connect Timeout = 15; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //const string CONN_STRING = @"Data Source=ipd8.database.windows.net;Initial Catalog=ProjectHealth;Integrated Security=False;User ID=ipd8abbott;Password=Abbott2000;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
>>>>>>> fb7087c259c52dac476feab113ffc6fb75eab77f

        private SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(CONN_STRING);
            conn.Open();
        }
    }
}
