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
        List<Meal> mealList = new List<Meal>();

        public SearchPage()
        {
            InitializeComponent();
            dgMealList.IsReadOnly = true;
            dgMealList.SelectionMode = DataGridSelectionMode.Single;
            dgMealList.ItemsSource = mealList;
            btnAdd.IsEnabled = true;
        }



        private void cbListMeal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbListMeal.SelectedIndex > 0)
            {
                string item = cbListMeal.SelectedItem.ToString();
                if (item == null)
                {
                   return;
                }
            //    MessageBox.Show("items is = " + item);
                // dgMealList.Items.Add(item);
                cbListMeal.Items.Refresh();


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
            title = cbListMeal.Text;

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
