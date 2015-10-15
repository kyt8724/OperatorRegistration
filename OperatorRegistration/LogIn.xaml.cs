using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OperatorRegistration.Utilities;

namespace OperatorRegistration
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// SCRIPTED DATE : 18 Jan 2012
    /// MOVE TO MAIN WINDOW AFTER LOGIN
    /// </summary>
    public partial class LogIn : Window
    {
        private int attemptLogin = 0;
        private string userId = "trainee";
        private string password = "trainee";
        private bool dbExist = false;
        public LoginInfo LoginInfoData { get; set; }

        public LogIn()
        {
            InitializeComponent();
        }

        public LogIn(bool isExistDB)
        {
            InitializeComponent();
            dbExist = isExistDB;
        }

        #region Custom Method

        /// <summary>
        /// Scripter : YONGTOK KIM
        /// Script Date : 20 Oct 2010
        /// If success login, close the Login form and set the value. If fail, refresh the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void LoginView()
        {
            if (!dbExist)
            {
                if (userId.Equals(txtCommonID.Text) && password.Equals(txtCommonPassword.Password))
                {
                    DialogResult = true;
                    Close();
                }
                else
                {
                    CheckNumberOfTry();
                }
            }
            else
            {
                UploadLogin login = new UploadLogin();
                byte[] passwd = DataHash.GetHashData(txtCommonPassword.Password, "SHA1");
                LoginInfo li = login.GetLoginInfo(txtCommonID.Text, passwd);

                if (li != null)
                {
                    LoginInfoData = li;
                    if (li.TypeOfUser == "Administrator")
                        DialogResult = true;
                    else
                    {
                        DialogResult = false;
                        MessageBox.Show("You are not authorized person to upload");
                    }
                    Close();
                }
                else
                {
                    CheckNumberOfTry();
                }
            }
        }

        // Check how many time try
        private void CheckNumberOfTry()
        {
            attemptLogin++;
            ClearControls();
            txtCommonID.Focus();
            if (attemptLogin == 3)
            {
                MessageBox.Show("Please check userid and password");
                DialogResult = false;
                Close();
            }
            else
                MessageBox.Show(string.Format("Invalid Login : {0} times", attemptLogin));
        }

        private void ClearControls()
        {
            txtCommonID.Text = string.Empty;
            txtCommonPassword.Password = string.Empty;
        }

        #endregion Custom Method

        private void btnCommonLogin_Clcik(object sender, RoutedEventArgs e)
        {
            LoginView();
        }

        private void btnCommonCancel_Clcik(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtCommonID.Focus();
        }

        private void txtCommonPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginView();
                e.Handled = true;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}