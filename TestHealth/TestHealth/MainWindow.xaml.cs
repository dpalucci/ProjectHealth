using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestHealth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database db;

        public MainWindow()
        {

            try
            {
                db = new Database();
            }
            catch (Exception )
            {
                // TODO: show a message box
                MessageBox.Show("Fatal error: unable to connect to database",
                    "Fatal error", MessageBoxButton.OK, MessageBoxImage.Stop);
                // TODO: write details of the exception to log text file
                Environment.Exit(1);
                //throw e;
            }
            InitializeComponent();
            try
            {
                List<Recipe> list = db.GetAllRecipes();
                dgRecipeList.ItemsSource = list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // TODO: show a message box
                MessageBox.Show("Unable to fetch records from database",
                    "Database error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }


        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            string title = cbTitle.Text;
            float fat = float.Parse(tbFat.Text);
            float protein = float.Parse(tbProtein.Text);
            float caloris = float.Parse(tbCalories.Text);
            float carb = float.Parse(tbCarb.Text);
            Recipe r = new Recipe() { Title = title, Protein = protein, Calories = caloris, Carb = carb, Fat = fat  };
            db.AddRecipe(r);
            tbProtein.Text = "";
            tbFat.Text = "";
            tbCarb.Text = "";
            tbCalories.Text = "";
            List<Recipe> list = db.GetAllRecipes();
            dgRecipeList.ItemsSource = list;

        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        //Combo box
        private void cbTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Data Grid
        private void dgRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

