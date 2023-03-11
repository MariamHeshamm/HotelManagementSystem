using EF_Project.context;
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
using EF_Project.Entities;

namespace EF_Project
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        
        private void Signin(object sender, RoutedEventArgs e)
        {
            using LoginManagerDB context = new LoginManagerDB();
            var result1 = (from u in context.Frontends
                          select new { u.user_name, u.pass_word }).ToList();
            var result2 = (from u in context.KitchenManagers
                           select new { u.user_name, u.pass_word }).ToList();
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Password.Trim();
            bool found = false;
            foreach ( var user in result1 ) 
            {
                if(user.user_name ==  username && user.pass_word == password)
                {
                    found = true;
                    var hotelFrontend = new HotelFrontend();
                    hotelFrontend.Show();
                    this.Close();
                    break;
                }
            }
            foreach (var user in result2)
            {
                if (user.user_name == username && user.pass_word == password)
                {
                    found = true;
                    var kitchen = new Kitchen();
                    kitchen.Show();
                    this.Close();
                    break;
                }
            }
        }

        private void license(object sender, RoutedEventArgs e)
        {
            var lincenseForm = new License(); //create your new form.
            lincenseForm.ShowDialog(); //show the new form.
          //  this.Close(); //only if you want to close the current form.
        }
    }
}
