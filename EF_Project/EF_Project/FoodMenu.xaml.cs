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

namespace EF_Project
{
    /// <summary>
    /// Interaction logic for FoodMenu.xaml
    /// </summary>
    public partial class FoodMenu : Window
    {
        public FoodMenu()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
           // resetFoodMenu();
        }
        public int LunchQ { get; set; }
        public int DinnerQ { get; set;}
        public int BreakfastQ { get; set;}
        public bool Towels { get; set; }
        public bool Sweetest_Surprise { get; set; }
        public bool Cleaning { get; set; }

        private void checkboxBreakfast_Checked(object sender, RoutedEventArgs e)
        {
            if (checkboxBreakfast.IsChecked == true)
                txtBreakFastQ.IsEnabled = true;
        }

        private void checkboxLaunch_Checked(object sender, RoutedEventArgs e)
        {
            if (checkboxLaunch.IsChecked == true)
                txtLaunchQ.IsEnabled = true;    
            
        }

        private void checkboxDinner_Checked(object sender, RoutedEventArgs e)
        {
            if(checkboxDinner.IsChecked == true) 
                 txtDinnerQ.IsEnabled = true;
            
        }
        private void checkboxLaunch_Unchecked(object sender, RoutedEventArgs e)
        {
            txtLaunchQ.IsEnabled = false;
        }

        private void checkboxBreakfast_Unchecked(object sender, RoutedEventArgs e)
        {
            txtBreakFastQ.IsEnabled = false;
        }
        private void resetFoodMenu()
        {
            foreach (var control in Grid1.Children)
            {
                if (control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndex = -1; // clear the selected item
                }
                else if (control is TextBox)
                {
                    ((TextBox)control).Clear(); // clear the text
                }
                else if (control is StackPanel)
                {
                    foreach (var innerControl in ((StackPanel)control).Children)
                    {
                        if (innerControl is ComboBox)
                        {
                            ((ComboBox)innerControl).SelectedIndex = -1; // clear the selected item
                        }
                        else if (innerControl is TextBox)
                        {
                            ((TextBox)innerControl).Clear(); // clear the text
                        }
                        else if (innerControl is CheckBox)
                        {
                            ((CheckBox)innerControl).IsChecked = false;
                        }
                    }
                }
            }
            foreach (var control in needsGrid.Children)
            {
                if (control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndex = -1; // clear the selected item
                }
                else if (control is TextBox)
                {
                    ((TextBox)control).Clear(); // clear the text
                }
                else if (control is StackPanel)
                {
                    foreach (var innerControl in ((StackPanel)control).Children)
                    {
                        if (innerControl is ComboBox)
                        {
                            ((ComboBox)innerControl).SelectedIndex = 0; // clear the selected item
                        }
                        else if (innerControl is TextBox)
                        {
                            ((TextBox)innerControl).Clear(); // clear the text
                        }
                        else if (innerControl is CheckBox)
                        {
                            ((CheckBox)innerControl).IsChecked = false;
                        }
                    }
                }
            }
        }

        private void checkboxDinner_Unchecked(object sender, RoutedEventArgs e)
        {
            txtDinnerQ.IsEnabled = false;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (checkboxBreakfast.IsChecked == true)
            {
                BreakfastQ = Convert.ToInt32(txtBreakFastQ.Text);
            }
            if (checkboxLaunch.IsChecked == true)
            {
                LunchQ = Convert.ToInt32(txtLaunchQ.Text);
            }
            if (checkboxDinner.IsChecked ==true)
            {
                DinnerQ = Convert.ToInt32(txtDinnerQ.Text);
            }
            if (checkboxCleaning.IsChecked == true)
            {
                Cleaning = true;
            }
            if (checkboxTowels.IsChecked==true)
            {
                Towels = true;
            }
            if (checkboxSweetestSureise.IsChecked == true)
            {
                Sweetest_Surprise = true;
            }
            this.Hide();
        }       
    }
}
