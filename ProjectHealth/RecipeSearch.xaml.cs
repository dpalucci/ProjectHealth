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
            btnAdd.IsEnabled = false;
        }

        private void listMeal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ((ComboBoxItem)cbListMeal.SelectedItem).Content;
            if (item == null)
            {
                return;
            }
            else
            {
                cbListMeal.Items.Add(cbListMeal.SelectedItem);
                cbListMeal.Items.Refresh();
            }

        }
    }
    internal class Meal
    {
        public string Recipe { get; set; }
        public string Type { get; set; }
        public string MealName { get; set; }
        public int Calories { get; set; }
    }
}
