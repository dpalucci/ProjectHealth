


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

        public double caloriesSum()
        {
            double calorissum = 0;
            foreach(Recipe r in recipeList ){
                calorissum += r.Calories;
            }
            return calorissum;
        }

        public double fatSum()
        {
            double fatsum = 0;
            foreach (Recipe r in recipeList)
            {
                fatsum += r.Calories;
            }
            return fatsum;
        }

        public double proteinSum()
        {
            double proteinsum = 0;
            foreach (Recipe r in recipeList)
            {
                proteinsum += r.Calories;
            }
            return proteinsum;
        } 

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
            cbTitle.ItemsSource = db.GetTitle();
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

                cbTitle.Text = "";
                tbFat.Text = "";
                tbProtein.Text = "";
                tbCarb.Text = "";
                tbCalories.Text = "";
                tbCalories1.Text = caloriesSum()+"";
                tbFat1.Text = fatSum() + "";
                tbProtein1.Text = proteinSum() + "";

               /* double sumCalories = 0;
                for (int i = 0; i < dgRecipeList.Items.Count; i++)
                {
                    TextBlock tbCalories1 = dgRecipeList.Columns[2].GetCellContent(dgRecipeList.SelectedItems[i]) as TextBlock;
                    double test = Convert.ToDouble(tbCalories1.Text);
                    sumCalories += (double.Parse((dgRecipeList.Columns[2].GetCellContent(dgRecipeList.Items[i]) as TextBlock).Text));

                }
                */
                
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

            DeleteRecord();
            dgRecipeList.ItemsSource = recipeList;

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
                lblId.Content = r.Id;
                cbTitle.Text = r.Title;
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
                if (items == null)
                {
                    return;
                }
               else if (items.CanRemove)
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
        public bool ValidateInput()
        {

            if (cbTitle.SelectedItem == null && cbTitle.Text == "")
            {
                MessageBox.Show("Please choose a title from the drop down menu or add it manually", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate for null values in input fields
            if (tbCalories.Text == null || tbCalories.Text == "")
            {
                MessageBox.Show("Please enter value for Calories", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (tbFat.Text == null || tbFat.Text == "")
            {
                MessageBox.Show("Please enter value for Fat", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (tbProtein.Text == null || tbProtein.Text == "")
            {
                MessageBox.Show("Please enter value for Protein", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (tbCarb.Text == null || tbCarb.Text == "")
            {
                MessageBox.Show("Please enter value for Carbs", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double calories = double.Parse(tbCalories.Text);
            if (calories < 0 || calories > 10000)
            {
                MessageBox.Show("Calories must be a number betwee 0-10000", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double fat = double.Parse(tbFat.Text);
            if (fat < 0 || fat > 100)
            {
                MessageBox.Show("Fat must be a number betwee 0-1000", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double protein = double.Parse(tbProtein.Text);
            if (protein < 0 || protein > 1000)
            {
                MessageBox.Show("Protein must be a number betwee 0-1000", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double carb = double.Parse(tbCarb.Text);
            if (calories < 0 || calories > 1000)
            {
                MessageBox.Show("Carbs must be a number betwee 0-1000", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        
    }
}
