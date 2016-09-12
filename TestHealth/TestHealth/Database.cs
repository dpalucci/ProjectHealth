using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDB
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
        const string CONN_STRING = @"Data Source=ipd8vs.database.windows.net;Initial Catalog=ProjectHealthDB;Persist Security Info=True;User ID=sqladmin;Password=***********";

        private SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(CONN_STRING);
            conn.Open();
        }

        // during prototyping stage we make methods that are
        // not yet implemented throw new NotImplementedException();
        public void AddPerson(Recipe r)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Recipe (Title, Fat, Protein, Calories) VALUES (@Title, @Fat, @Protein, @Calories)"))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Title", r.Title);
                cmd.Parameters.AddWithValue("@Fat", r.Fat);
                cmd.Parameters.AddWithValue("@Protein", r.Protein);
                cmd.Parameters.AddWithValue("@Calories", r.Calories);
                cmd.ExecuteNonQuery();
            }
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
                        // column by name - the better (preferred) way
                        
                        string title = reader.GetString(reader.GetOrdinal("Title"));
                        float fat = reader.GetFloat(reader.GetOrdinal("Fat"));
                        float Protein = reader.GetFloat(reader.GetOrdinal("Protein"));
                        float Calories = reader.GetFloat(reader.GetOrdinal("Calories"));
                        Recipe r = new Recipe() { Id = id, Title = title, Fat = fat, Protein = Protein, Calories = Calories };
                        list.Add(p);
                        // Console.WriteLine("Person[{0}]: {1} is {2} y/o", id, name, age);
                    }
                }
            }
            return list;
        }

        public Person GetPersonById(int Id)
        {
            throw new NotImplementedException();
        }

        public void DeletePersonById(int Id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Person WHERE Id=@Id", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePerson(Person p)
        {
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Person SET Name = @Name, Age = @Age WHERE Id=@Id", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Age", p.Age);
                cmd.Parameters.AddWithValue("@Id", p.Id);
                cmd.ExecuteNonQuery();
            }
        }

    }
}

