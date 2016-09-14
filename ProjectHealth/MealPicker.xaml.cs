


using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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

namespace ProjectHealth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MealPicker : Window
    {
        Database db;
        List<string> titleList = new List<string>();
        List<Recipe> recipeList = new List<Recipe>();

        public MealPicker()
        {
            try
            {
                db = new Database();
            }
            catch (Exception)
            {
                // TODO: show a message box
                MessageBox.Show("Fatal error: unable to connect to database",
                    "Fatal error", MessageBoxButton.OK, MessageBoxImage.Stop);
                // TODO: write details of the exception to log text file
                Environment.Exit(1);
                //throw e;
            }

            InitializeComponent();

            dgRecipeList.ItemsSource = recipeList;
            cbTitle.ItemsSource = titleList;
            btDelete.IsEnabled = false;
            btUpdate.IsEnabled = false;
            dgRecipeList.SelectionMode = DataGridSelectionMode.Single;

            //Get a CollectionViewSource wich would implement Interface INotifyCollectionChanged to handle events related to update ItemSource in datagrid
            CollectionView dgRecipeListCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(dgRecipeList.Items);
            ((INotifyCollectionChanged)dgRecipeListCollectionView).CollectionChanged += new NotifyCollectionChangedEventHandler(dgRecipeList_CollectionChanged);

            try
            {
                List<Recipe> list = db.GetAllRecipes();
                //dgRecipeList.ItemsSource = list;
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
            if (!ValidateInput())
            {
                return;
            }


            Recipe recipe = getNewRecipe();
            //Add the new Recipe to the grid
            recipeList.Add(recipe);
            dgRecipeList.Items.Refresh();
            //Add the new title to the list of title or change the list of title )
            if (!titleList.Contains(recipe.Title))
            {
                titleList.Add(recipe.Title);
                cbTitle.Items.Refresh();
            }
        }







        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            Recipe newRecipe = getNewRecipe();
            Recipe selectedRecipe = (Recipe)dgRecipeList.SelectedItem;
            selectedRecipe.Title = newRecipe.Title;
            selectedRecipe.Fat = newRecipe.Fat;
            selectedRecipe.Calories = newRecipe.Calories;
            selectedRecipe.Carb = newRecipe.Carb;
            selectedRecipe.Protein = newRecipe.Protein;
            //refresh the grid
            dgRecipeList.Items.Refresh();
            //Add the new title to the title list 
            if (!titleList.Contains(newRecipe.Title))
            {

                titleList.Add(newRecipe.Title);
                cbTitle.Items.Refresh();
            }
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
        private void dgRecipeList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Enable  Context menu if there are items in datagrid view and disable if there are not

            DeleteContextMenu.IsEnabled = (dgRecipeList.Items.Count != 0);
        }

        private void dgRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Recipe r = (Recipe)dgRecipeList.SelectedItem;
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

                cbTitle.SelectedItem = r.Title;
                tbFat.Text = r.Fat + "";
                tbProtein.Text = r.Protein + "";
                tbCarb.Text = r.Carb + "";
                tbCalories.Text = r.Calories + "";

            }


        }

        private void DeleteRecord()
        {

            MessageBoxResult answer = MessageBox.Show("Are you sure you want to delete row " + (dgRecipeList.SelectedIndex + 1) + "?", "Delete", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (answer == MessageBoxResult.OK)
            {
                IEditableCollectionView items = dgRecipeList.Items;
                if (items.CanRemove)
                {
                    items.RemoveAt(dgRecipeList.SelectedIndex);
                }

            }
        }


        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            DeleteRecord();
        }

        private Recipe getNewRecipe()
        {
            string title = "";
            if (cbTitle.SelectedItem == null)
            {
                title = cbTitle.Text.Trim();
            }
            else
            {
                title = cbTitle.SelectedItem.ToString();
            }

            double fat = double.Parse(tbFat.Text);
            double protein = double.Parse(tbProtein.Text);
            double caloris = double.Parse(tbCalories.Text);
            double carb = double.Parse(tbCarb.Text);
            Recipe recipe = new Recipe() { Title = title, Protein = protein, Calories = caloris, Carb = carb, Fat = fat };
            db.AddRecipe(recipe);
            tbProtein.Text = "";
            tbFat.Text = "";
            tbCarb.Text = "";
            tbCalories.Text = "";


            return recipe;
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
            if (calories < 0 || calories > 10000)
            {
                MessageBox.Show("Caloris must be a number betwee 0-10000", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double fat = double.Parse(tbFat.Text);
            if (fat < 0 || fat > 100)
            {
                MessageBox.Show("Caloris must be a number betwee 0-1000", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
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

