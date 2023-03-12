using EF_Project.context;
using EF_Project.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Resources;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EF_Project
{
    /// <summary>
    /// Interaction logic for HotelFrontend.xaml
    /// </summary>
    public partial class HotelFrontend : Window
    {
        public int totalAmount = 0;//bta3t elroom
        public IEnumerable<int> roomFloors= new List<int>();
        public IEnumerable<string> roomTypes = new List<string>();
        public IEnumerable<int> roomNumbersChecked = new List<int>();
        public IEnumerable<int> roomNumbersUnChecked = new List<int>();
        public List<int> allrooms = new List<int>();
        public List<int> singleRooms = new List<int>();
        public List<int> duplexRooms = new List<int>();
        public List<int> suiteRooms = new List<int>();
        public List<int> TwinRooms = new List<int>();
        public List<int> doubleRooms = new List<int>();

        int breakfastQ = 0;
        int launchQ = 0;
        int dinnerQ = 0;
        bool cleaning = false;
        bool towels = false;
        bool sweetestsurprise = false;
        int foodBill = 0;
        private int finalizedTotalAmount = 0;
        int primaryK = 0;
        private string paymentType;
        private string paymentCardNumber;
        private int MM_Of_Card;
        private int YY_Of_Card;
        private string CVC_Of_Card;
        private string CardType;
        public Boolean editClicked = false;
        bool taskFinder = false;
        public string FPayment, FCnumber, FcardExpOne, FcardExpTwo, FCardCVC;
        public DateTime cardExpDate;
        public int foodStatus = 0;
        public int checkIINn = 0;

        public HotelFrontend()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            datagridSearchAllinreservation.Visibility = Visibility.Hidden;
            Load_dataGridView();
            getOccupiedRoom();
            ReservedRoom();
            getChecked();//3shan ageeb arkam elrooms ely mesh fdya
            reservationComboItems();
        }
        private void Load_dataGridView()
        {
            using FrontendContext context = new FrontendContext();
            context.States.Load();
            context.Rooms.Load();
            context.Reservations.Load();
            context.Customers.Load();
            var res =
                (from reserve in context.Reservations
                 join room in context.Rooms on reserve.Room.RoomNumber equals room.RoomNumber
                 join customer in context.Customers on reserve.Customer.CustomerId equals customer.CustomerId
                 select new
                 {
                     customer.Firstname,
                     customer.Lastname,
                     customer.Birthdate,
                     customer.gender,
                     customer.Phonenumber,
                     customer.EmailAddress,
                     customer.City,
                     customer.ApartementSuite,
                     customer.Zipcode,
                     customer.StreetAddress,
                     room.RoomNumber,
                     room.RoomType,
                     room.RoomFloor,
                     reserve.ArrivalTime,
                     reserve.LeavingTime,
                     reserve.Number_of_Guests,
                     reserve.BreakFast,
                     reserve.Launch,
                     reserve.Dinner,
                     reserve.Cleaning,
                     reserve.Towels,
                     reserve.SweetestSurprise,
                     reserve.Card_Cvv,
                     reserve.Card_ExpDate,
                     reserve.Card_Number,
                     reserve.Card_Type,
                     reserve.Debit,
                     reserve.Credit,
                     reserve.Food_Bill,
                     reserve.Total_Bill
                 }).ToList();
            datagridResevervationlist.ItemsSource = res;
        }
        private void getOccupiedRoom()
        {
            List<string> listBoxItems1 = new List<string>();
            using FrontendContext context = new FrontendContext();
            context.States.Load();
            context.Rooms.Load();
            context.Reservations.Load();
            context.Customers.Load();

            var resCheckIn =
                (from reserve in context.Reservations
                 join room in context.Rooms on reserve.Room.RoomNumber equals room.RoomNumber
                 join customer in context.Customers on reserve.Customer.CustomerId equals customer.CustomerId
                 where reserve.Check_In == true && room.Availability == false
                 select new
                 {
                     customer.CustomerId,
                     customer.Firstname,
                     customer.Lastname,
                     customer.Phonenumber,
                     room.RoomNumber,
                     room.RoomType,

                 }).ToList();
            foreach (var item in resCheckIn)
            {
                string txt = "[" + item.RoomNumber.ToString() + "]    " + item.RoomType.ToString() + " "
                     + item.CustomerId.ToString() + " [" + item.Firstname.ToString() + " " +
                     item.Lastname.ToString() + "] " + item.Phonenumber.ToString();
                listBoxItems1.Add(txt);
            }
            listBox1.ItemsSource = listBoxItems1;

        }
        private void ReservedRoom()
        {
            List<string> listBoxItems2 = new List<string>();
            using FrontendContext context = new FrontendContext();
            context.States.Load();
            context.Rooms.Load();
            context.Reservations.Load();
            context.Customers.Load();

            var resReserved =
                (from reserve in context.Reservations
                 join room in context.Rooms on reserve.Room.RoomNumber equals room.RoomNumber
                 join customer in context.Customers on reserve.Customer.CustomerId equals customer.CustomerId
                 where reserve.Check_In == false && room.Availability == false
                 select new
                 {
                     customer.CustomerId,
                     customer.Firstname,
                     customer.Lastname,
                     customer.Phonenumber,
                     room.RoomNumber,
                     room.RoomType,
                 }).ToList();

            foreach (var item in resReserved)
            {
                string txt = "[" + item.RoomNumber.ToString() + "]    " + item.RoomType.ToString() + " "
                    + item.CustomerId.ToString() + " [" + item.Firstname.ToString() + " " +
                    item.Lastname.ToString() + "] " + item.Phonenumber.ToString();
                listBoxItems2.Add(txt);
            }
            listBox2.ItemsSource = listBoxItems2;
            int k = 0;
        }
        private void getChecked()
        {
            using FrontendContext context = new FrontendContext();
            context.States.Load();
            context.Rooms.Load();
            context.Reservations.Load();
            context.Customers.Load();
             /*roomNumbersChecked = (from room in context.Rooms
                                  join reserva in context.Reservations on room.RoomId equals reserva.Room.RoomId
                                  where room.Availability == true
                                  select  
                            room.RoomNumber
                            
                                  ).ToList();*/
            allrooms = (from room in context.Rooms
                        where room.Availability == true
                        select room.RoomNumber
                        ).ToList();

            singleRooms = (from room in context.Rooms
                        where room.Availability == true && room.RoomType =="Single"
                        select room.RoomNumber
                        ).ToList();

            doubleRooms = (from room in context.Rooms
                        where room.Availability == true && room.RoomType=="Double"
                        select room.RoomNumber
                        ).ToList();

            TwinRooms = (from room in context.Rooms
                        where room.Availability == true && room.RoomType=="Twin"
                        select room.RoomNumber
                        ).ToList();

            duplexRooms = (from room in context.Rooms
                        where room.Availability == true && room.RoomType=="Duplex"
                        select room.RoomNumber
                        ).ToList();

            suiteRooms = (from room in context.Rooms
                           where room.Availability == true && room.RoomType=="Suite"
                           select room.RoomNumber
                       ).ToList();

            /* foreach (var room in roomNumbersChecked)
             {
                 if (allrooms.Contains(room))
                 {
                     allrooms.Remove(room);
                 }
             }*/
           // comboRoomNumber.ItemsSource = allrooms;
           // comboStates.ItemsSource = context.Rooms.Local.ToBindingList();
            //comboRoomNumber.DisplayMemberPath = "RoomNumber";
           // comboRoomNumber.SelectedValuePath = "RoomId";

        }
        private void reservationComboItems()
        {
            using FrontendContext context = new FrontendContext();
            context.States.Load();
            context.Rooms.Load();
            context.Reservations.Load();
            context.Customers.Load();
            List<string> reservation = new List<string>();
            var reservationList =
            (from reserve in context.Reservations
             join room in context.Rooms on reserve.Room.RoomNumber equals room.RoomNumber
             join customer in context.Customers on reserve.Customer.CustomerId equals customer.CustomerId

             select new
             {
                 customer.CustomerId,
                 customer.Firstname,
                 customer.Lastname,
                 customer.Phonenumber,
                 room.RoomNumber,
             }).ToList();
            foreach (var item in reservationList)
            {
                string txt = "[" + item.RoomNumber.ToString() + "]" + " "
                     + item.CustomerId.ToString() + " [" + item.Firstname.ToString() + " " +
                     item.Lastname.ToString() + "] " + item.Phonenumber.ToString();
                reservation.Add(txt);
            }
            //comboReservationList.ItemsSource = reservation;
            comboReservationList.ItemsSource = context.Reservations.Local.ToBindingList();
            comboReservationList.DisplayMemberPath = "Customer";
            comboReservationList.SelectedValuePath = "ReservationId";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            using FrontendContext context = new FrontendContext();
            context.States.Load();
            context.Rooms.Load();
            context.Reservations.Load();
            context.Customers.Load();
           
            List<string> Gender = new List<string>() { "Female", "Male" };
            comboGender.ItemsSource = Gender;
            List<int> days = new List<int>() {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18
            ,19,20,21,22,23,24,25,26,27,28,29,30,31};
            comboDays.ItemsSource = days;
            List<string> months = new List<string>() {"January","Febuary","March","April","May","June",
            "July","August","September","October","November","December"};
            comboMonths.ItemsSource = months;

            List<int> numOfGuests = new List<int>() { 1, 2, 3, 4, 5, 6 };
            comboNumOfGuests.ItemsSource = numOfGuests;
            var states = (from p in context.States
                         select new {p.StatesName,p.StatesId }).ToList();
            // comboStates.ItemsSource = states;
            comboStates.ItemsSource = context.States.Local.ToBindingList();
            comboStates.DisplayMemberPath = "StatesName";
            comboStates.SelectedValuePath = "StatesId";


            roomFloors = (from room in context.Rooms
                             select room.RoomFloor).ToList().Distinct();
            comboFloorNumber.ItemsSource = roomFloors;

            roomTypes = (from room in context.Rooms
             select room.RoomType).ToList().Distinct();
            comboRoomTypes.ItemsSource = roomTypes;

            
        }

        private void comboRoomTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           using FrontendContext context = new FrontendContext();
            context.Reservations.Load();
            context.Rooms.Load();
            context.Customers.Load();
            context.States.Load();
           
            if (comboRoomTypes.SelectedItem.Equals("Single"))
            {
                var roomPrice = context.Rooms.Where(r => r.RoomType == "Single").Select(r => r.Price).FirstOrDefault();
                totalAmount = roomPrice;
                comboFloorNumber.SelectedItem = 1;
                comboRoomNumber.ItemsSource = singleRooms;
            }
            else if (comboRoomTypes.SelectedItem.Equals("Double"))
            {
                var roomPrice = context.Rooms.Where(r => r.RoomType == "Double").Select(r => r.Price).FirstOrDefault();
                totalAmount = roomPrice;
                comboFloorNumber.SelectedItem = 2 ;
                comboRoomNumber.ItemsSource = doubleRooms;
            }
            else if (comboRoomTypes.SelectedItem.Equals("Twin"))
            {
                var roomPrice = context.Rooms.Where(r => r.RoomType == "Twin").Select(r => r.Price).FirstOrDefault();
                totalAmount = roomPrice;
                comboFloorNumber.SelectedItem = 3;
                comboRoomNumber.ItemsSource = TwinRooms;
            }
            else if (comboRoomTypes.SelectedItem.Equals("Duplex"))
            {
                var roomPrice = context.Rooms.Where(r => r.RoomType == "Duplex").Select(r => r.Price).FirstOrDefault();
                totalAmount = roomPrice;
                comboFloorNumber.SelectedItem = 4;
                comboRoomNumber.ItemsSource = duplexRooms;
            }
            else if (comboRoomTypes.SelectedItem.Equals("Suite"))
            {
                var roomPrice = context.Rooms.Where(r => r.RoomType == "Suite").Select(r => r.Price).FirstOrDefault();
                totalAmount = roomPrice;
                comboFloorNumber.SelectedItem = 5;
                comboRoomNumber.ItemsSource = suiteRooms;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                datagridSearchAllinreservation.Visibility = Visibility.Visible;
                using FrontendContext context = new FrontendContext();
                context.States.Load();
                context.Rooms.Load();
                context.Reservations.Load();
                context.Customers.Load();
                Thickness newMargin = new Thickness(10, 10, 10, 10);
                txtSearch.Margin = newMargin;
                /*
                 *   string query = "Select * from reservation where Id like '%" + searchTextBox.Text + 
                 *   "%' OR last_name like '%" + searchTextBox.Text + "%' OR first_name like '%" + searchTextBox.Text +
                 *   "%' OR gender like '%" + searchTextBox.Text + "%' OR state like '%" + searchTextBox.Text +
                 *   "%' OR city like '%" + searchTextBox.Text + "%' OR room_number like '%" + searchTextBox.Text +
                 *   "%' OR room_type like '%" + searchTextBox.Text + "%' OR email_address like '%" + searchTextBox.Text +
                 *   "%' OR phone_number like '%" + searchTextBox.Text + "%'";
          
                 */
               
                var k = txtSearch.Text;
                var res1 =
                     (from reserve in context.Reservations
                      join room in context.Rooms on reserve.Room.RoomNumber equals room.RoomNumber
                      join customer in context.Customers on reserve.Customer.CustomerId equals customer.CustomerId
                      where customer.Lastname.Contains(txtSearch.Text) 
                      || customer.Firstname.Contains(txtSearch.Text)
                      || customer.City.Contains(txtSearch.Text)
                      || customer.gender.Contains(txtSearch.Text)
                      || customer.Phonenumber.Contains(txtSearch.Text)
                      || customer.EmailAddress.Contains(txtSearch.Text)
                      || room.RoomType.Contains(txtSearch.Text)
                      select new
                      {
                          customer.Firstname,
                          customer.Lastname,
                          customer.Birthdate,
                          customer.gender,
                          customer.Phonenumber,
                          customer.EmailAddress,
                          customer.City,
                          customer.ApartementSuite,
                          customer.Zipcode,
                          customer.StreetAddress,
                          room.RoomNumber,
                          room.RoomType,
                          room.RoomFloor,
                          reserve.ArrivalTime,
                          reserve.LeavingTime,
                          reserve.Number_of_Guests,
                          reserve.BreakFast,
                          reserve.Launch,
                          reserve.Dinner,
                          reserve.Cleaning,
                          reserve.Towels,
                          reserve.SweetestSurprise,
                          reserve.Card_Cvv,
                          reserve.Card_ExpDate,
                          reserve.Card_Number,
                          reserve.Card_Type,
                          reserve.Debit,
                          reserve.Credit,
                          reserve.Food_Bill,
                          reserve.Total_Bill
                      }).ToList();
                datagridSearchAllinreservation.ItemsSource = res1;
            }
        }
      
        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            /*string query = "Select * from reservation where check_in = '" + "False" + "';";*/           
        }

        private void comboReservationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try { 
            //comboReservationList.SelectedIndex = 0;
            taskFinder = true;
            object selectedValue = comboReservationList.SelectedValue;
            using FrontendContext context = new FrontendContext();
            context.States.Load();
            context.Rooms.Load();
            context.Reservations.Load();
            context.Customers.Load();
            var reservationRow = context.Reservations.Include(R => R.Customer)
                .Include(R => R.Room).Where(R => R.ReservationId == int.Parse(comboReservationList.SelectedValue.ToString()))
                .FirstOrDefault();
            txtFirstName.Text = reservationRow?.Customer.Firstname;
            txtLastName.Text = reservationRow?.Customer.Lastname;
            comboDays.Text = reservationRow?.Customer.Birthdate.Day.ToString();
            comboMonths.SelectedIndex = reservationRow.Customer.Birthdate.Month - 1;
            txtYear.Text = reservationRow?.Customer.Birthdate.Year.ToString();
            comboGender.Text = reservationRow?.Customer.gender;
            txtPhone.Text = reservationRow?.Customer.Phonenumber;
            txtEmail.Text = reservationRow?.Customer.EmailAddress;
            txtStreetAddress.Text = reservationRow?.Customer.StreetAddress;
            txtAptSuite.Text = reservationRow?.Customer.ApartementSuite.ToString();
            txtCity.Text = reservationRow?.Customer.City;
            comboStates.Text = reservationRow?.Customer?.State?.StatesName;
            txtZipCode.Text = reservationRow?.Customer.Zipcode;
            comboNumOfGuests.SelectedValue = reservationRow?.Number_of_Guests;
            comboRoomTypes.SelectedValue = reservationRow?.Room.RoomType;
            comboFloorNumber.SelectedValue = reservationRow?.Room.RoomFloor;
                if (reservationRow.Room.RoomType == "Single") 
                {
                    singleRooms.Add(reservationRow.Room.RoomNumber);
                    comboRoomNumber.ItemsSource = singleRooms;
                    comboRoomNumber.SelectedIndex = comboRoomNumber.Items.IndexOf(reservationRow.Room.RoomNumber);//(object) reservationRow.Room.RoomNumber;
                }
                else if (reservationRow.Room.RoomType == "Double") 
                {
                    doubleRooms.Add(reservationRow.Room.RoomNumber);
                    comboRoomNumber.ItemsSource = doubleRooms;
                    comboRoomNumber.SelectedIndex = comboRoomNumber.Items.IndexOf(reservationRow.Room.RoomNumber);//(object) reservationRow.Room.RoomNumber;
                }
                else if (reservationRow.Room.RoomType == "Twin")
                {
                    TwinRooms.Add(reservationRow.Room.RoomNumber);
                    comboRoomNumber.ItemsSource = TwinRooms;
                    comboRoomNumber.SelectedIndex = comboRoomNumber.Items.IndexOf(reservationRow.Room.RoomNumber);//(object) reservationRow.Room.RoomNumber;
                }
                else if (reservationRow.Room.RoomType == "Duplex")
                {
                    duplexRooms.Add(reservationRow.Room.RoomNumber);
                    comboRoomNumber.ItemsSource = duplexRooms;
                    comboRoomNumber.SelectedIndex = comboRoomNumber.Items.IndexOf(reservationRow.Room.RoomNumber);//(object) reservationRow.Room.RoomNumber;
                }
                else if (reservationRow.Room.RoomType == "Suite") 
                {
                    suiteRooms.Add(reservationRow.Room.RoomNumber);
                    comboRoomNumber.ItemsSource = suiteRooms;
                    comboRoomNumber.SelectedIndex = comboRoomNumber.Items.IndexOf(reservationRow.Room.RoomNumber);//(object) reservationRow.Room.RoomNumber;
                }
            dataArrival.Text = reservationRow?.ArrivalTime.ToString();
            dataDeparture.Text = reservationRow?.LeavingTime.ToString();
            checkin.IsChecked = reservationRow?.Check_In;

            cleaning = reservationRow.Cleaning;
            towels = reservationRow.Towels;
            sweetestsurprise = reservationRow.SweetestSurprise;

            if (reservationRow.Debit == true) { FPayment = "Debit"; }
            else if (reservationRow.Credit == true) { FPayment = "Credit"; }
            FCnumber = reservationRow.Card_Number;
            FCardCVC = reservationRow.Card_Cvv;
            FcardExpOne = reservationRow.Card_ExpDate.Month.ToString();
            FcardExpTwo = reservationRow.Card_ExpDate.Year.ToString();
                foodBill = reservationRow.Food_Bill;
                totalAmount = reservationRow.Total_Bill;

            launchQ = reservationRow.Launch;
            breakfastQ = reservationRow.BreakFast;
            dinnerQ = reservationRow.Dinner;

            FoodOrSupply.IsChecked = reservationRow.Supply_Status;
            primaryK = Convert.ToInt32(selectedValue);
        }
            catch (Exception ){ MessageBox.Show("Please select reservation to Edit"); }
        }

        private void Edit_Reservation_Button_Click(object sender, RoutedEventArgs e)
        {
           // reset_frontend();
          //  reservationComboItems();
            //Load_dataGridView();
            editClicked = true;
            btnDeleteReservation.Visibility = Visibility.Visible;
            btnSubmit.Visibility = Visibility.Hidden;
            btnUpdateReservation.Visibility = Visibility.Visible;
            comboReservationList.Visibility = Visibility.Visible;
            lblSelectReservations.Visibility = Visibility.Visible;          
        }
        
        private void btnNewReservation_Click(object sender, RoutedEventArgs e)
        {
            btnSubmit.Visibility = Visibility.Visible;
            btnUpdateReservation.Visibility = Visibility.Hidden;
            btnDeleteReservation.Visibility = Visibility.Hidden;
            lblSelectReservations.Visibility= Visibility.Hidden;
            comboReservationList.Visibility= Visibility.Hidden;
            taskFinder = false;
            reset_frontend();
         //   FinalizePayment finalizebil = new FinalizePayment();
       //     finalizebil.resetFinalizePayment();
        }
        private void reset_frontend()
        {
            foreach (var control in grid1.Children)
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
                    }
                }
            }
            foreach (var control in grid2.Children)
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
                    }
                }
            }
            checkin.IsChecked = false;
            FoodOrSupply.IsChecked = false;
            dataArrival.SelectedDate = null;
            dataDeparture.SelectedDate = null;
            comboFloorNumber.SelectedIndex = -1;
            comboNumOfGuests.SelectedIndex = -1;
            comboRoomNumber.SelectedIndex = -1;
          //  comboRoomTypes.SelectedIndex = -1;//hna bygen exception
            //foreach (var control in grid3.Children)
            //{
            //    if (control is ComboBox)
            //    {
            //        ((ComboBox)control).SelectedIndex = -1; // clear the selected item
            //    }
            //    else if (control is TextBox)
            //    {
            //        ((TextBox)control).Clear(); // clear the text
            //    }
            //}
        }

      

        private void FoodOrSupply_Checked(object sender, RoutedEventArgs e)
        {
            
            lblFoodSupply.Content = "Food/Supply: Complete";
            foodStatus = 1;
        }

        private void FoodOrSupply_Unchecked(object sender, RoutedEventArgs e)
        {
            foodStatus = 0;
         
            lblFoodSupply.Content = "Food/Supply status?";
        }

        private void checkin_Checked(object sender, RoutedEventArgs e)
        {
            lblCheckin.Content = "Checked in";
            checkIINn = 1;
        }

        private void checkin_Unchecked(object sender, RoutedEventArgs e)
        {
            lblCheckin.Content = "Check in ?";
            checkIINn = 0;
        }
        private void btnDeleteReservation_Click(object sender, RoutedEventArgs e)
        {
            FrontendContext context = new FrontendContext();
            context.Reservations.Load();
            context.Rooms.Load();
            context.States.Load();
            context.Customers.Load();

            var reservationRow = context.Reservations.Include(R => R.Customer)
               .Include(R => R.Room).Where(R => R.ReservationId == int.Parse(comboReservationList.SelectedValue.ToString()))
               .FirstOrDefault();

            var reservation = context.Reservations.Find(comboReservationList.SelectedValue);
            context.Reservations.Remove(reservation);

            var customer = context.Customers.Find(reservationRow.Customer.CustomerId);
            context.Customers.Remove(customer);

            var room = context.Rooms.Find(reservationRow.Room.RoomId);
            room.Availability = true;

            context.SaveChanges();
            MessageBox.Show("Deleted Succesfullyy");
            reservationComboItems();
            Load_dataGridView();
            reset_frontend();
            getOccupiedRoom();
            ReservedRoom();
            getChecked();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            FrontendContext context = new FrontendContext();

            context.States.Load();
            context.Rooms.Load();
            context.Reservations.Load();
            context.Customers.Load();
            bool b = false;
            if (paymentType == "Debit") { b = true; }
            else if (paymentType == "Credit") { b = true; }

            int roomId = context.Rooms.Where(p => p.RoomNumber == (int)comboRoomNumber.SelectedValue)
                .Select(p => p.RoomId).FirstOrDefault();
            Room room = context.Rooms.Find(roomId);
            room.Availability = false;

            int stateId = context.States.Where(p => p.StatesName == comboStates.Text)
                .Select(p => p.StatesId).FirstOrDefault();
            State state = context.States.Find(stateId);

            Customer customer = new Customer()
            {
                Firstname = txtFirstName.Text,
                Lastname = txtLastName.Text,
                Birthdate = new DateTime(int.Parse(txtYear.Text), comboMonths.SelectedIndex + 1, comboDays.SelectedIndex + 1),
                EmailAddress = txtEmail.Text,
                ApartementSuite = int.Parse(txtAptSuite.Text),
                City = txtCity.Text,
                gender = (string)comboGender.SelectedValue,
                Phonenumber = txtPhone.Text,
                StreetAddress = txtStreetAddress.Text,
                Zipcode = txtZipCode.Text,
                State = state,
            };
            long getphn = Convert.ToInt64(paymentCardNumber);
            string formatString = String.Format("{0:0000-0000-0000-0000}", getphn);
            paymentCardNumber = formatString;
            Reservation reservation = new()
            {
                Cleaning = cleaning,
                Towels = towels,
                SweetestSurprise = sweetestsurprise,
                ArrivalTime = dataArrival.SelectedDate,
                LeavingTime = dataDeparture.SelectedDate,
                Number_of_Guests = (int)comboNumOfGuests.SelectedValue,
                BreakFast = breakfastQ,
                Launch = launchQ,
                Dinner = dinnerQ,
                Card_Cvv = CVC_Of_Card,
                Card_ExpDate = cardExpDate,
                Check_In = checkin.IsChecked,
                Credit = b,
                Debit = !b,
                Food_Bill = foodBill,
                Total_Bill = totalAmount,
                Supply_Status = FoodOrSupply.IsChecked,
                Card_Type = CardType,
                Card_Number = paymentCardNumber,
                Customer = customer,
                Room = room,
            };
            context.Reservations.Add(reservation);
            context.SaveChanges();
            MessageBox.Show("Saved Succesfullyy");
            reservationComboItems();
            Load_dataGridView();
            reset_frontend();
            getOccupiedRoom();
            ReservedRoom();
            getChecked();
        }
        private void btnUpdateReservation_Click(object sender, RoutedEventArgs e)
        {
            bool b = false;
            if (paymentType == "Debit") { b = true; }
            else if (paymentType == "Credit") { b = true; }
            FrontendContext context = new FrontendContext();
            context.Reservations.Load();
            context.Rooms.Load();
            context.States.Load();
            context.Customers.Load();
            var reservationRow = context.Reservations.Include(R => R.Customer)
              .Include(R => R.Room).Where(R => R.ReservationId == int.Parse(comboReservationList.SelectedValue.ToString()))
              .FirstOrDefault();

            reservationRow.Customer.Firstname = txtFirstName.Text;
            reservationRow.Customer.Lastname = txtLastName.Text;
            reservationRow.Customer.gender = (string)comboGender.SelectedValue;
            reservationRow.Customer.Birthdate = new DateTime(int.Parse(txtYear.Text), comboMonths.SelectedIndex + 1, comboDays.SelectedIndex + 1);
            reservationRow.Customer.Phonenumber = txtPhone.Text;
            reservationRow.Customer.EmailAddress = txtEmail.Text;
            reservationRow.Customer.City = txtCity.Text;
            reservationRow.Customer.ApartementSuite = int.Parse( txtAptSuite.Text);
            reservationRow.Customer.Zipcode = txtZipCode.Text;
            reservationRow.Customer.StreetAddress = txtStreetAddress.Text;
            reservationRow.Customer.State.StatesName = comboStates.Text;
            reservationRow.Cleaning = cleaning;
            reservationRow.Towels = towels;
            reservationRow.SweetestSurprise = sweetestsurprise;
            reservationRow.ArrivalTime = dataArrival.SelectedDate;
            reservationRow.LeavingTime = dataDeparture.SelectedDate;
            reservationRow.Number_of_Guests = (int)comboNumOfGuests.SelectedValue;
            reservationRow.BreakFast = breakfastQ;
            reservationRow.Dinner = dinnerQ;
            reservationRow.Launch = launchQ;
            reservationRow.Card_Cvv = CVC_Of_Card;
            reservationRow.Card_ExpDate = cardExpDate;
            reservationRow.Check_In = checkin.IsChecked;
            reservationRow.Credit = b;
            reservationRow.Debit = !b;
            reservationRow.Food_Bill = foodBill;
            reservationRow.Total_Bill = totalAmount;
            reservationRow.Supply_Status = FoodOrSupply.IsChecked;
            reservationRow.Card_Type = CardType;
            reservationRow.Card_Number = paymentCardNumber;

            int roomId = context.Rooms.Where(p => p.RoomNumber == (int)comboRoomNumber.SelectedValue)
                .Select(p => p.RoomId).FirstOrDefault(); //ely hwa m8yro dlw2ti
            Room room = context.Rooms.Find(roomId);

            int OldRoomReserved = reservationRow.Room.RoomId; //old
            int NewRoomReserved = roomId;
            if(OldRoomReserved!=NewRoomReserved)
            {
                reservationRow.Room.Availability = true;
                reservationRow.Room = room;
                room.Availability = false;
            }
            // Save the changes to the database
            context.SaveChanges();
            reservationComboItems();
            Load_dataGridView();
            reset_frontend();
            getOccupiedRoom();
            ReservedRoom();
            getChecked();
        }

        private void btnFoodAndMenu_Click(object sender, RoutedEventArgs e)
        {
            FoodMenu foodMenu = new FoodMenu();

            if (taskFinder)
            {
                
                if (breakfastQ > 0)
                {
                    foodMenu.checkboxBreakfast.IsChecked = true;
                    foodMenu.txtBreakFastQ.Text = Convert.ToString(breakfastQ);
                }
                if (launchQ > 0)
                {
                    foodMenu.checkboxLaunch.IsChecked = true;

                    foodMenu.txtLaunchQ.Text = Convert.ToString(launchQ);
                }
                if (dinnerQ > 0)
                {
                    foodMenu.checkboxDinner.IsChecked = true;
                    foodMenu.txtDinnerQ.Text = Convert.ToString(dinnerQ);
                }
                if (cleaning == true)
                {
                    foodMenu.checkboxCleaning.IsChecked = true;
                }
                if (towels == true)
                {
                    foodMenu.checkboxTowels.IsChecked = true;
                }
                if (sweetestsurprise == true)
                {
                    foodMenu.checkboxSweetestSureise.IsChecked = true;
                }
                foodMenu.ShowDialog();
                breakfastQ = foodMenu.BreakfastQ;
                launchQ = foodMenu.LunchQ;
                dinnerQ = foodMenu.DinnerQ;
                cleaning = foodMenu.Cleaning;
                towels = foodMenu.Towels;
                sweetestsurprise = foodMenu.Sweetest_Surprise;


                if (breakfastQ > 0 || launchQ > 0 || dinnerQ > 0)
                {
                    int bfast = 7 * breakfastQ;
                    int Lnch = 15 * launchQ;
                    int di_ner = 15 * dinnerQ;
                    foodBill = bfast + Lnch + di_ner;
                }
            }

            else
            {
                foodMenu.ShowDialog();
                breakfastQ = foodMenu.BreakfastQ;
                launchQ = foodMenu.LunchQ;
                dinnerQ = foodMenu.DinnerQ;
                cleaning = foodMenu.Cleaning;
                towels = foodMenu.Towels;
                sweetestsurprise = foodMenu.Sweetest_Surprise;


                if (breakfastQ > 0 || launchQ > 0 || dinnerQ > 0)
                {
                    int bfast = 7 * breakfastQ;
                    int Lnch = 15 * launchQ;
                    int di_ner = 15 * dinnerQ;
                    foodBill = bfast + Lnch + di_ner;
                }
            }
        }

        private void btnPayment_Click(object sender, RoutedEventArgs e)
        {
            FinalizePayment finalizebil = new FinalizePayment();
        //    finalizebil.resetFinalizePayment();
            finalizebil.totalAmountFromFrontend = totalAmount;
            finalizebil.foodBill = foodBill;
           

            if (taskFinder)
            {
                if (FPayment == "Debit")
                    finalizebil.comboPaymentType.SelectedIndex = 0;
                else if (FPayment == "Credit")
                {
                    finalizebil.comboPaymentType.SelectedIndex = 1;
                }
                finalizebil.txtCardNum.Text = FCnumber;
                finalizebil.comboMonthCard.SelectedIndex = int.Parse(FcardExpOne);
                finalizebil.comboYearCard.SelectedValue = int.Parse(FcardExpTwo);
                finalizebil.txtCVV.Text = FCardCVC;

            }
            finalizebil.ShowDialog();
            if (finalizebil.d5ltData == true)
            {
                finalizedTotalAmount = finalizebil.FinalTotalFinalized;
                paymentType = finalizebil.PaymentType;
                paymentCardNumber = finalizebil.PaymentCardNumber;
                MM_Of_Card = finalizebil.MM_Of_Card;
                YY_Of_Card = finalizebil.YY_Of_Card;
                CVC_Of_Card = finalizebil.CVC_Of_Card;
                CardType = finalizebil.CardType;
                if (MM_Of_Card == 0)
                { MM_Of_Card = 1; }
                cardExpDate = new DateTime(YY_Of_Card, MM_Of_Card, 1);
            }
            if (!editClicked)
            {
                btnSubmit.Visibility = Visibility.Visible;
            }
        }

        private void hotel_frontend_window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void tabItemResevation_Loaded(object sender, RoutedEventArgs e)
        {
            FoodOrSupply.IsEnabled = false;
        }
    }
}
