using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace OperatorRegistration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Scripter : YONGTOK KIM
        /// SCRIPTED DATE : 17 Jan 2012
        /// MOVE TO MAIN WINDOW AFTER LOGIN
        /// </summary>

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Login
            LogIn login = new LogIn();

            // success to login
            if (login.ShowDialog() == true)
                new MainWindow().ShowDialog();

            Shutdown();
        }
    }
}
