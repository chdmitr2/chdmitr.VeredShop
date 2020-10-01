#region USING
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VeredShopBL.VeredShopModel;
#endregion

namespace VeredShopUI
{
    public partial class MainWindow : Window
    {
        #region Defining Objects
        VeredContext DB = new VeredContext();
        Client client;
        Seller seller;
        Storekeeper storekeeper;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();           
        }
        #endregion

        #region Allows A Window To Be Dragged By  A Mouse
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
            this.DragMove();
        }
        #endregion

        #region Go To Information Screen
        private void About_Click(object sender, RoutedEventArgs e)
        {
            var about = new About();
            about.Show();
            this.Close();
        }
        #endregion

        #region Exit From Application
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Enter Email and Password To Log In 
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text == "admin" && PassBox.Password == "admin")
            {
                var menu = new Menu();
                menu.Show();
                this.Close();
            }
            else
            {
                try
                {
                    var email = DB.Clients.Where(u => u.Email == LoginBox.Text).FirstOrDefault();
                    var email2 = DB.Sellers.Where(u => u.Email == LoginBox.Text).FirstOrDefault();
                    var email3 = DB.Storekeepers.Where(u => u.Email == LoginBox.Text).FirstOrDefault();

                    if ((LoginBox.Text == "") || (PassBox.Password == ""))
                    {
                        if (LoginBox.Text == "")
                        {
                            MessageBox.Show("Email is Required!", "Caution", MessageBoxButton.OK);
                            LoginBox.Focus();
                        }
                        else if (PassBox.Password == "")
                        {
                            MessageBox.Show("Password is Required!", "Caution", MessageBoxButton.OK);
                            PassBox.Focus();
                        }
                    }
                    else
                    {
                        if (email != null)
                        {
                            var pass = email.Password;
                            pass = PassBox.Password;
                            if (PassBox.Password == pass)
                            {
                                MessageBox.Show("Login Successfully!", "Login Success", MessageBoxButton.OK);
                                client = DB.Clients.Where(u => u.Email == LoginBox.Text).FirstOrDefault();
                                var menu = new Menu(client);
                                menu.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Email or Password are wrong!");
                            }
                        }
                        else if(email2 != null)
                        {
                            var pass = email2.Password;
                            pass = PassBox.Password;
                            if (PassBox.Password == pass)
                            {
                                MessageBox.Show("Login Successfully!", "Login Success", MessageBoxButton.OK);
                                seller = DB.Sellers.Where(u => u.Email == LoginBox.Text).FirstOrDefault();
                                var menu = new Menu(seller);
                                menu.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Email and Password or wrong!");
                            }
                        }
                        else if (email3 != null)
                        {
                            var pass = email3.Password;
                            pass = PassBox.Password;
                            if (PassBox.Password == pass)
                            {
                                MessageBox.Show("Login Successfully!", "Login Success", MessageBoxButton.OK);
                                storekeeper = DB.Storekeepers.Where(u => u.Email == LoginBox.Text).FirstOrDefault();
                                var menu = new Menu(storekeeper);
                                menu.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Email and Password or wrong!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Email and Password is invalid");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region Go To Registration Screen
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Registaration registration = new Registaration();
            registration.Show();
            this.Close();

        }
        #endregion

        private void Forgot_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Enter to the screen About to contact with store Vered and change password", "Caution", MessageBoxButton.OK);
        }
    }
}
