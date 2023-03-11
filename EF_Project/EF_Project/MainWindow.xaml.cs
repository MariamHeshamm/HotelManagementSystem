using EF_Project.context;
using EF_Project.Entities;
using Microsoft.EntityFrameworkCore;
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

namespace EF_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using FrontendContext frontendContext = new FrontendContext();

            //using LoginManagerDB context = new LoginManagerDB();


            //context.Add
            //    (new Frontend()
            //    {
            //        user_name = "admin",
            //        pass_word = "admin",                 
            //    }
            //    );
            //context.Add
            //    (new Frontend()
            //    {
            //        user_name = "ceaser.krit",
            //        pass_word = "admin123",
            //    }
            //    );
            //context.Add
            //    (new Frontend()
            //    {
            //        user_name = "nazim.amin",
            //        pass_word = "admin",
            //    }
            //    );
            //context.Database.Migrate();
            //context.SaveChanges();
        }
    }
}
