using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHealth
{
    // Entity
    class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Calories { get; set; }
        public float Fat { get; set; }
        public float Carb { get; set; }
        public float Protein { get; set; }
        public string Type { get; set; }
    }
    class Database
    {
        const string CONN_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Download\ProjectHealthDB.mdf;Integrated Security=True;Connect Timeout=30";

        private SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(CONN_STRING);
            conn.Open();
        }


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
                        int id = reader.GetInt32(reader.GetOrdinal("Id"));
                        string title = reader.GetString(reader.GetOrdinal("Title"));
                        float calories = reader.GetFloat(reader.GetOrdinal("Protein"));
                        float protein = reader.GetFloat(reader.GetOrdinal("Calories"));
                        float fat = reader.GetFloat(reader.GetOrdinal("Fat"));
                        Recipe r = new Recipe() { Id = id, Title = title, Protein = protein, Calories = calories, Fat = fat };
                        list.Add(r);
                    }
                }
            }
            return list;

        }
    }
}