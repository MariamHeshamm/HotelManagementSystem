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
    /// Interaction logic for FinalizePayment.xaml
    /// </summary>
    public partial class FinalizePayment : Window
    {
        public int totalAmountFromFrontend = 0;
        public int foodBill = 0;
        public bool d5ltData = false;
        public int FinalTotalFinalized { get; set; }
        public string PaymentType { get; set; }

        public string PaymentCardNumber { get; set; }
        public int MM_Of_Card { get; set; }
        public int YY_Of_Card { get; set; }

        public string CVC_Of_Card;
        public string CardType { get; set; }
        public FinalizePayment()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> months = new List<string>() {"January","Febuary","March","April","May","June",
            "July","August","September","October","November","December"};
            comboMonthCard.ItemsSource = months;
            List<int> years = new List<int>() { 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032};
            comboYearCard.ItemsSource = years;
            List<string> PaymentTypes = new List<string>()
            { "Debit","Credit"};
            comboPaymentType.ItemsSource = PaymentTypes;



            double totalWithTax = Convert.ToDouble(totalAmountFromFrontend) * 0.07;
            double FinalTotal = Convert.ToDouble(totalAmountFromFrontend) + totalWithTax + foodBill;
            lblCurrentBill.Content = "$" + Convert.ToString(totalAmountFromFrontend) + " USD";
            lblFoodBill.Content = "$" + Convert.ToString(foodBill) + " USD";
            lblTax.Content = "$" + Convert.ToString(totalWithTax) + " USD";
            lblTotal.Content = "$" + Convert.ToString(FinalTotal) + " USD";
            FinalTotalFinalized = Convert.ToInt32(FinalTotal);
           // resetFinalizePayment();
            //  MM_Of_Card = comboMonthCard.SelectedIndex + 1;
            //    YY_Of_Card = (int)comboYearCard.SelectedValue;
        }
        public void resetFinalizePayment()
        {
            lblCardType.Content = "Unknown";
            lblCurrentBill.Content = "$0 USD";
            lblFoodBill.Content = "$0 USD";
            lblTax.Content = "$0 USD";
            lblTotal.Content = "$0 USD";
           /* foreach (var control in Grid1.Children)
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
            }*/
        }

        private void txtCardNum_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txtCardNum.Text.Length != 0)
            {
                if (txtCardNum.Text.Substring(0, 1) == "3")
                {
                    lblCardType.Content = "AmericanExpress";
                }
                else if (txtCardNum.Text.Substring(0, 1) == "4")
                {
                    lblCardType.Content = "Visa";
                }
                else if (txtCardNum.Text.Substring(0, 1) == "5")
                {
                    lblCardType.Content = "MasterCard";
                }
                else if (txtCardNum.Text.Substring(0, 1) == "6")
                {
                    lblCardType.Content = "Discover";
                }
                else
                    lblCardType.Content = "Unknown";
            }
            else { MessageBox.Show("Please Enter Card Number First"); }
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PaymentType = comboPaymentType.Text;
                PaymentCardNumber = txtCardNum.Text;
                MM_Of_Card = comboMonthCard.SelectedIndex + 1;
                YY_Of_Card =(int) comboYearCard.SelectedValue;
                CVC_Of_Card = txtCVV.Text;
                CardType = (string)lblCardType.Content;
                d5ltData = true;
                this.Hide();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Closing the window");
            }
        }
    }
}
