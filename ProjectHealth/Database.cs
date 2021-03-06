﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectHealth
{
    public class Recipe
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

        public void AddPerson(Person p)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO [USER] (Name, Age, Weight, Height, Gender, Mealid) VALUES (@Name, @Age, @Weight, @Height, @Gender,@Mealid)"))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Age", p.Age);
                cmd.Parameters.AddWithValue("@Weight", p.Weight);
                cmd.Parameters.AddWithValue("@Height", p.Height);
                cmd.Parameters.AddWithValue("@Gender", p.Gender);
                cmd.Parameters.AddWithValue("@Mealid", p.Mealid);
                cmd.ExecuteNonQuery();
                //show message window user was saved
                //MessageBox.Show("Saved to USER table", "INFORMATION");             
            }
        }

        

        // during prototyping stage we make methods that are
        // not yet implemented throw new NotImplementedException();
        public void AddRecipe(Recipe r)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Recipe (Title, Fat, Protein, Calories) VALUES (@Title, @Fat, @Protein, @Calories)"))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Title", r.Title);
                cmd.Parameters.AddWithValue("@Calories", r.Calories);
                cmd.Parameters.AddWithValue("@Fat", r.Fat);
                cmd.Parameters.AddWithValue("@Protein", r.Protein);
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
                        double calories = reader.GetDouble(reader.GetOrdinal("Calories"));
                        double fat = reader.GetDouble(reader.GetOrdinal("Fat"));
                        double protein = reader.GetDouble(reader.GetOrdinal("Protein"));

                        Recipe r = new Recipe() { Title = title, Calories = calories, Fat = fat, Protein = protein };
                        list.Add(r);
                        // Console.WriteLine("Person[{0}]: {1} is {2} y/o", id, name, age);
                    }
                }
            }
            return list;
        }

        public List<string> GetTitle()
        {
            List<string> listTitle = new List<string>();
            SqlCommand cmd = new SqlCommand("SELECT Title FROM Recipe", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    listTitle.Add(reader["Title"].ToString());
                }

            }
            return (listTitle);
        }

        public Recipe GetRecipeById(int Id)
        {
            Recipe r = new Recipe();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Recipe WHERE Id=@Id", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
            return r;
        }

        public void DeleteRecipeById(int Id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Recipe WHERE Id=@Id", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateRecipe(Recipe r)
        {
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Recipe SET Title = @Title, Fat = @Fat , Calories = @Calories, Protein = @Protein, Type = @Type, Carb = @Carb WHERE Id=@Id", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Title", r.Title);
                cmd.Parameters.AddWithValue("@Fat", r.Fat);
                cmd.Parameters.AddWithValue("@Protein", r.Protein);
                cmd.Parameters.AddWithValue("@Calories", r.Calories);
                cmd.Parameters.AddWithValue("@Carb", r.Carb);
                cmd.Parameters.AddWithValue("@Id", r.Id);
                cmd.ExecuteNonQuery();
            }
        }

    }//end of Database class
}//end of namespace ProjectHealth

