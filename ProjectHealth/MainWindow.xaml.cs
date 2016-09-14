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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // FIXME: verify user data is correct
            string name = tbName.Text;
            if(name.Length < 3)
            {
                MessageBox.Show("Invalid Name: Invalid name entered, must be > 2 characters",
                    "error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            int age = int.Parse(tbAge.Text);
            if (age < 0 || age > 130)
            {
                MessageBox.Show("Invalid Age: Invalid age entered, must be >0 and < 130",
                    "error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            float weight = float.Parse(tbWeight.Text);
            if (weight < 90 || weight > 650)
            {
                MessageBox.Show("Invalid Weight: Invalid weight entered, must be >=90 and <= 650",
                    "error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            float height = float.Parse(tbHeight.Text);
            if (height < 3.5 || height > 8)
            {
                MessageBox.Show("Invalid Height: Invalid height entered, must be >=3.5 and <= 8",
                    "error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            string gender = tbGender.Text;
            Person p = new Person() { Name = name, Age = age, Weight = weight, Height = height, Gender = gender };
            db.AddPerson(p);
                      
            
        }
    }

    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Gender { get; set; }
    }
}
