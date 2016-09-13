using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                string title = cbTitle.Text;
                double fat = double.Parse(tbFat.Text);
                double protein = double.Parse(tbProtein.Text);
                double caloris = double.Parse(tbCalories.Text);
                double carb = double.Parse(tbCarb.Text);
                Recipe r = new Recipe() { Title = title, Protein = protein, Calories = caloris, Carb = carb, Fat = fat };
                db.AddRecipe(r);
                tbProtein.Text = "";
                tbFat.Text = "";
                tbCarb.Text = "";
                tbCalories.Text = "";
                List<Recipe> list = db.GetAllRecipes();
                dgRecipeList.ItemsSource = list;
            }
            catch (IOException ex)
            {
                MessageBox.Show("Enter correct data format" + ex.Message);
            }
        }



        

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            Recipe r = (Recipe)dgRecipeList.SelectedItem;
            if (r == null)
            {
                MessageBox.Show("Please select an item for deletion",
                    "Invalid action", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            db.DeleteRecipeById(r.Id);
            List<Recipe> list = db.GetAllRecipes();
            dgRecipeList.ItemsSource = list;

        }

        //Combo box
        private void cbTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Data Grid
        private void dgRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Recipe r = (Recipe) dgRecipeList.SelectedItem;
            if (r == null)
            {
                //if there is no selection dissable buttons Update and Add
                btUpdate.IsEnabled = false;
                btDelete.IsEnabled = false;
            }
            else
            {
                //if there is a selectoin enable buttons Update and Add
                btUpdate.IsEnabled = true;
                btDelete.IsEnabled = true;
                //if there is a selection populate text boxes and combo box with the properties of the objetc selected in data grid
                
                cbTitle.SelectItems = r.Title;
                tbFat.Text = r.Fat + "";
                tbProtein.Text = r.Protein + "";
                tbCarb.Text = r.Carb + "";
                tbCalories.Text = r.Calories + "";

            }


        }

        //Validations
        private bool ValidateInput()
        {
            //TODO: Ask teacher if it is a good idea to validate properties in setters of Animal class, and to catch exception at instantiation rather than valiadate input fields
            if (cbTitle.SelectedItem == null && cbTitle.Text == "")
            {
                MessageBox.Show("Please choose a title from the drop down menu or write it in the input field", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double calories = double.Parse(tbCalories.Text);
            if (calories < 0 || calories >10000)
            {
                MessageBox.Show("Caloris must be a number betwee 0-10000", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double fat = double.Parse(tbFat.Text);
            if (fat < 0 || fat > 100)
            {
                MessageBox.Show("Caloris must be a number betwee 0-100", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double protein = double.Parse(tbProtein.Text);
            if (protein < 0 || protein > 1000)
            {
                MessageBox.Show("Caloris must be a number betwee 0-1000", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double carb = double.Parse(tbCarb.Text);
            if (calories < 0 || calories > 1000)
            {
                MessageBox.Show("Caloris must be a number betwee 0-1000", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

    }
}

