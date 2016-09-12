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
using System.Windows.Shapes;

namespace ProjectHealth
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Window
    {
        Database db;

        public SearchPage()
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
               
            }
            InitializeComponent();
            try
            {
                List<Recipe> list = db.GetAllRecipes();
                dgRecipeList.ItemsSource = list;
            }
            catch (Exception )
            {
                // TODO: show a message box
                MessageBox.Show("Unable to fetch records from database",
                    "Database error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // select a recipe from combobox
        

        private void cbListRecipe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Recipe r = (Recipe)dgRecipeList.SelectedItem;
            if (r == null)
            {

                MessageBox.Show("Please select an item ",
                    "Invalid action", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            List<Recipe> list = db.GetAllRecipes();
            dgRecipeList.ItemsSource = list;
            /*
             * lblId.Content = p.Id;
            tbName.Text = p.Name;
            tbAge.Text = p.Age + "";
            */
        }

        

    }
    
}
