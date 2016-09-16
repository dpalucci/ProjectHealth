using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class MainWindow : Window
    {
        Database db;
        public MainWindow()
        {
            try
            {
                db = new Database();
            }
            catch (Exception e)
            {
                   MessageBox.Show("Fatal error: unable to connect to database",
                    "Fatal error", MessageBoxButton.OK, MessageBoxImage.Stop);
                // TODO: write details of the exception to log text file
                Environment.Exit(1);
                //throw e;
            }
            InitializeComponent();
        }


        public bool ValidateInput()
        {
            // Validate for null  values in input fields
            if (tbName.Text == null || tbName.Text== "") 
            {
                MessageBox.Show("Please enter a name ", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            // Validate for null  values in input fields
            if (tbAge.Text == null || tbAge.Text == "")
            {
                MessageBox.Show("Please enter value for age", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            // Validate for null  values in input fields
            if (cbGender.SelectedItem == null || cbGender.Text == "")
            {
                MessageBox.Show("Please enter value for Gender input field", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            // Validate for null  values in input fields
            if (tbWeight.Text == null || tbWeight.Text == "")
            {
                MessageBox.Show("Please enter value for Weight", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            // Validate for null  values in input fields
            if (tbHeight.Text == null || tbHeight.Text == "")
            {
                MessageBox.Show("Please enter value for Height", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // validate name
            if ((!Regex.Match(tbName.Text, "^[A-Z][a-zA-Z]*$").Success) || (tbName.Text.Length < 3))
             {
                    //  name was incorrect
                    MessageBox.Show("Invalid name, must be > 2 valid characters and start with a Capital letter", "Name Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbName.Focus();
                    return false;
              }

            //Age validation: if (age < 0 || age > 130)
            int age = int.Parse(tbAge.Text);
            if (age < 0 || age > 130)
              {
                    MessageBox.Show("Invalid Age: Invalid age entered, must be >0 and < 130",
                        "error", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return false;
              }

            //Weight Validation
            double weight = float.Parse(tbWeight.Text);
            if (weight < 90 || weight > 650)
                {
                    MessageBox.Show("Invalid Weight: Invalid weight entered, must be >=90 and <= 650",
                        "error", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return false;
                }
            //Height Validation
            double height = float.Parse(tbHeight.Text);
            if (height < 3.5 || height > 8)
             {
                    MessageBox.Show("Invalid Height: Invalid height entered, must be >=3.5 and <= 8",
                        "error", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return false;
             }
            return true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            //check for null values in input - if yes,then return
            if (!ValidateInput())
            {
                return;
            }
            // validate name
            string name = tbName.Text;

            //Age validation
            int age = int.Parse(tbAge.Text);
 
            //Weight Validation
            double weight = float.Parse(tbWeight.Text);
  
            //Height Validation
            double height = float.Parse(tbHeight.Text);
  
            //Gender Selection thru Combobox
            string gender = cbGender.Text;

            //  int plan = null;
            int plan = 0 ;

            if (rbtnPlan1.IsChecked == true)
                plan = 1;
            else if (rbtnPlan2.IsChecked == true)
                plan = 2;
            else if (rbtnPlan2.IsChecked == true)
                plan = 3;

            Person p = new Person() { Name = name, Age = age, Weight = weight, Height = height, Gender = gender, Mealid = plan};
            db.AddPerson(p);

            MealPicker win = new MealPicker();
            win.Show();
            this.Close();


        }// end of btnAdd_Click 
    }

    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Gender { get; set; }
        public int Mealid { get; set; }

    }
}
