using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHealth
{
    class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Calories { get; set; }
        public double Fat { get; set; }
        public double Carb { get; set; }
        public double Protein { get; set; }
        public string Type { get; set; }
    }

    class Database

    {

        const string CONN_STRING = @"Data Source=ipd8vs.database.windows.net;Initial Catalog=ProjectHealthDB;Integrated Security=False;User ID=sqladmin;Password=IPD8rocks!;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

         

        private SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(CONN_STRING);
            conn.Open();
        }

        public void AddPerson(string name, int age, float weight, float height, string gender)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO USER (Name, Age, Weight, Height, Gender) VALUES (@Name, @Age, @Weight, @Height, @Gender)"))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Weight", weight);
                cmd.Parameters.AddWithValue("@Height", height);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddPerson(Person p)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Person (Name, Age) VALUES (@Name, @Age)"))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Age", p.Age);
                cmd.ExecuteNonQuery();
            }
        }

        /*
        public List<Recipe> GetAllRecipes()
        {
            List<Recipe> list = new List<Recipe>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Recipe", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //int id = reader.GetInt32(reader.GetOrdinal("Id"));
                        string title = reader.GetString(reader.GetOrdinal("Title"));
                        double calories = reader.GetFloat(reader.GetOrdinal("Protein"));
                        double protein = reader.GetFloat(reader.GetOrdinal("Calories"));
                        double fat = reader.GetFloat(reader.GetOrdinal("Fat"));
                        Recipe r = new Recipe() { Title = title, Protein = protein, Calories = calories, Fat = fat };
                        list.Add(r);
                    }
                }
            }
            return list;
        }
        */
    }
}
