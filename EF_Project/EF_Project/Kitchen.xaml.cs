using EF_Project.context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for Kitchen.xaml
    /// </summary>
    public partial class Kitchen : Window
    {
        List<string> kitchenList = new();
        int totalBill = 0;
        int foodBill = 0;
        int primaryID;
        int breakfast, dinner, launch;
        bool supply_status,cleaning,towel,sweetestsrp;

        public Kitchen()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void kitchenWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadForDataGridView();
            listBoxFromDataBase();
        }
        private void LoadForDataGridView()
        {
          /*  queryString = "Select ID, first_name, last_name, phone_number, room_type,
           *  room_floor, room_number, break_fast, lunch, dinner, cleaning,
           *  towel, s_surprise, supply_status, food_bill
           *  from reservation where check_in = '" + "True" + "' 
           *  AND supply_status= '" + "False" + "'";*/
          using FrontendContext context = new FrontendContext();
          context.States.Load();
            context.Rooms.Load();
            context.Reservations.Load();
            context.Customers.Load();
            var res = (from p in context.Customers
                      join reserve in context.Reservations on p.CustomerId equals reserve.Customer.CustomerId
                      join room in context.Rooms on reserve.Room.RoomId equals room.RoomId
                      where reserve.Check_In == true && reserve.Supply_Status == false
                      select new 
                      {
                          p.Firstname,p.Lastname,p.Phonenumber,
                          room.RoomType,room.RoomFloor,reserve.BreakFast,reserve.Launch,
                          reserve.Dinner,reserve.Cleaning,reserve.Towels,reserve.SweetestSurprise,
                          reserve.Supply_Status,reserve.Food_Bill
                      }).ToList(); 

            datagridOverview.ItemsSource = res;
        }
        private void listBoxFromDataBase()
        {
            
            kitchenList.Clear();
            listBox1.ItemsSource = null;
            listBox1.Items.Clear();
            /*queryString = "Select * from reservation where check_in = '" + 
             * "True" + "' AND supply_status='" + "False" + "'";*/
            using FrontendContext context = new FrontendContext();
            context.States.Load();
            context.Rooms.Load();
            context.Reservations.Load();
            context.Customers.Load();
            var res = (from p in context.Customers
                       join reserve in context.Reservations on p.CustomerId equals reserve.Customer.CustomerId
                       join room in context.Rooms on reserve.Room.RoomId equals room.RoomId
                       where reserve.Check_In == true && reserve.Supply_Status == false
                       select new
                       {
                           reserve.ReservationId,
                           p.Firstname,
                           p.Lastname,
                           p.Phonenumber,
                       }).ToList();
            foreach(var item in res)
            {
                string txt = item.ReservationId.ToString() + " [" + item.Firstname.ToString() + " " +
                     item.Lastname.ToString() + "] " + item.Phonenumber.ToString();
                kitchenList.Add(txt);
            }
            
            listBox1.ItemsSource = kitchenList;
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                resetKitchen();
                string[] reservationRow = listBox1.SelectedValue.ToString().Split(" ");
                int reservationId = int.Parse(reservationRow[0]);
                using FrontendContext context = new FrontendContext();
                context.States.Load();
                context.Rooms.Load();
                context.Reservations.Load();
                context.Customers.Load();
                var reservatinObj = (from p in context.Customers
                                     join reserve in context.Reservations on p.CustomerId equals reserve.Customer.CustomerId
                                     join room in context.Rooms on reserve.Room.RoomId equals room.RoomId
                                     where reserve.ReservationId == reservationId
                                     select new
                                     {
                                         reserve.ReservationId,
                                         p.Firstname,
                                         p.Lastname,
                                         p.Phonenumber,
                                         reserve.Food_Bill,
                                         reserve.Total_Bill,
                                         room.RoomType,
                                         room.RoomFloor,
                                         room.RoomNumber,
                                         reserve.Cleaning,
                                         reserve.Towels,
                                         reserve.SweetestSurprise,
                                         reserve.Launch,
                                         reserve.Dinner,
                                         reserve.BreakFast,
                                         reserve.Supply_Status,
                                     }).FirstOrDefault();

                if (reservatinObj?.Cleaning == true)
                {
                    cleaning = true;
                    chCleaning.IsChecked = true;
                }
                else { chCleaning.IsChecked = false; }

                if (reservatinObj?.Towels == true)
                {
                    towel = true;
                    chTowels.IsChecked = true;
                }
                else { chTowels.IsChecked = false; }

                if (reservatinObj?.SweetestSurprise == true)
                {
                    sweetestsrp = true;
                    chSweetestSurprise.IsChecked = true;
                }
                else
                {
                    chSweetestSurprise.IsChecked = false;
                }

                if (reservatinObj?.Supply_Status == true)
                {
                    chFoodSupplyStatus.IsChecked = true;
                }
                else { chFoodSupplyStatus.IsChecked = false; }

                //binding
                txtLaunchQTY.Text = reservatinObj?.Launch.ToString();
                txtBreakfastQTY.Text = reservatinObj?.BreakFast.ToString();
                txtDinnerQTY.Text = reservatinObj?.Dinner.ToString();
                txtFirstName.Text = reservatinObj?.Firstname;
                txtLastName.Text = reservatinObj?.Lastname;
                txtPhone.Text = reservatinObj?.Phonenumber;
                txtRoomType.Text = reservatinObj?.RoomType;
                txtFloor.Text = reservatinObj?.RoomFloor.ToString();
                txtRoomNum.Text = reservatinObj?.RoomNumber.ToString();
                totalBill = Convert.ToInt32(reservatinObj?.Total_Bill);
                foodBill = Convert.ToInt32(reservatinObj?.Food_Bill);
                launch = reservatinObj.Launch;
                breakfast = reservatinObj.BreakFast;
                dinner = reservatinObj.Dinner;
                totalBill -= foodBill;
                primaryID = reservationId;
            }
            catch (Exception)
            {
                MessageBox.Show("Updated");
            }
        }
        private void chFoodSupplyStatus_Checked(object sender, RoutedEventArgs e)
        {
            chCleaning.IsChecked = false;
            lblCleaning.Content = "Cleaned";

            chTowels.IsChecked = false;
            lblTowels.Content = "Toweled";
            
            chSweetestSurprise.IsChecked = false;
            lblSweetestSurprise.Content = "Surprised";
            
            supply_status = true;
        }

        private void kitchenWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnUpdateChanges_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new FrontendContext())
            {
                var reservations = context.Reservations
                    .Include(r => r.Customer)
                    .Include(r => r.Room)
                    .Where(r => r.ReservationId == primaryID)
                    .FirstOrDefault();

             /*   queryString = "update reservation set total_bill='" +
              *   totalBill + foodBill + "', break_fast='" + breakfast 
              *   + "', lunch= '" + lunch + "', dinner='" + dinner + "',
              *   supply_status='" + supply_status + "',cleaning='" 
              *   + Convert.ToInt32(cleaning) + "',towel='" + Convert.ToInt32(towel)
              *   + "',s_surprise='" + Convert.ToInt32(surprise) + "',food_bill='"
              *   + foodBill + "' WHERE Id = '" + primaryID + "';";*/

                if(reservations != null)
                {
                    reservations.Total_Bill = totalBill;
                    reservations.Food_Bill = foodBill;
                    reservations.BreakFast = breakfast;
                    reservations.Launch = launch; ;
                    reservations.Dinner = dinner;
                    reservations.Supply_Status = chFoodSupplyStatus.IsChecked;
                    reservations.Cleaning = cleaning;
                    reservations.Towels = towel;
                    reservations.SweetestSurprise = sweetestsrp;
                    reservations.Food_Bill = foodBill;
                }
            context.SaveChanges();
      //      MessageBox.Show("Updated");
            listBoxFromDataBase();
            LoadForDataGridView();
            resetKitchen();                
            }
        }

        private void resetKitchen()
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
            foreach (var control in Grid2.Children)
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

        private void btnChangeFoodSelection_Click(object sender, RoutedEventArgs e)
        {
            FoodMenu food_menu = new FoodMenu();
            food_menu.needsGrid.Visibility = Visibility.Hidden;
            food_menu.btnNext.Visibility = Visibility.Visible;

            food_menu.ShowDialog();

            breakfast = food_menu.BreakfastQ;
            launch = food_menu.LunchQ;
            dinner = food_menu.DinnerQ;

            int bfast = 0, Lnch = 0, di_ner = 0;
            if (breakfast > 0)
            {
                bfast = 7 * breakfast;
            }
            if (launch > 0)
            {
                Lnch = 15 * launch;
            }
            if (dinner > 0)
            {
                di_ner = 15 * dinner;
            }
            foodBill += (bfast + Lnch + di_ner);
        }
    }
}
