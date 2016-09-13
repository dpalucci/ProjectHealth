using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectHealth
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Window
    {
        List<Meal> mealList = new List<Meal>();

        public SearchPage()
        {
            InitializeComponent();

            dgMealList.IsReadOnly = true;
            dgMealList.SelectionMode = DataGridSelectionMode.Single;
            dgMealList.ItemsSource = mealList;
            btnAdd.IsEnabled = true;
            fillCombo();
        }



        public void fillCombo()
        {
            SqlConnection conn = new SqlConnection("SELECT * FROM Recipe", conn);
            using (SqlDataReader reader = conn.ExecuteReader()) ;
            string Query = "SELECT * FROM Recipe";
            using (SqlDataReader reader = conn.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {                     
                        string title = reader.GetString(reader.GetOrdinal("Title"));
                        cbRecipeList.Items.Add(title);
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
                        // column by name - the better (preferred) way
                        int id = reader.GetInt32(reader.GetOrdinal("Id"));
                        string title = reader.GetString(reader.GetOrdinal("Title"));
                        float calories = reader.GetFloat(reader.GetOrdinal("Calories"));
                        float fat = reader.GetFloat(reader.GetOrdinal("Fat"));
                        float carb = reader.GetFloat(reader.GetOrdinal("Carb"));
                        float protein = reader.GetFloat(reader.GetOrdinal("Protein"));
                        string type = reader.GetString(reader.GetOrdinal("Type"));
                        Recipe r = new Recipe() { Id = id, Title = title, Calories = calories, Fat = fat, Carb = carb,
                        Protein = protein, Type = type};
                        list.Add(r);
                        // Console.WriteLine("Person[{0}]: {1} is {2} y/o", id, name, age);
                    }
                }
            }
            return list;
        }
        */
        private void cbRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbRecipeList.SelectedIndex > 0)
            {
                string item = cbRecipeList.SelectedItem.ToString();
                if (item == null)
                {
                   return;
                }
                
                cbRecipeList.Items.Refresh();


                // MessageBox.Show("Please select an item from Menu list");
            }
        }

        private void dgMealList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) 
            
        {

            Meal newMeal = getNewMeal();
            //Add to the grid
            mealList.Add(newMeal);
            dgMealList.Items.Refresh();
        }
        
        private Meal getNewMeal()
        {

            string title = "";
            title = cbRecipeList.Text;

            //         int age = int.Parse(tbAge.Text);
            //       string name = tbName.Text;
            //     double weight = double.Parse(tbWeight.Text);

             Meal meal = new Meal() { Title = title, Type = "Chicken", Calories = 271, Protein = "40", Fat = "100" };
             return meal;
        }



        internal class Meal
        {
            public string Title { get; set; }
            public string Type { get; set; }
            public int Calories { get; set; }
            public string Protein { get; set; }
            public string Fat { get; set; }

        }
    }
}
