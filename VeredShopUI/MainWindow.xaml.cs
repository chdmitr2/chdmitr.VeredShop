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
using VeredShopBL.VeredShopModel;

namespace VeredShopUI
{
    /// <summary>
    /// Interaction logic for Enter.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VeredContext DB = new VeredContext();
        Client client;
        Seller seller;
        Storekeeper storekeeper;
        public MainWindow()
        {
            InitializeComponent();
           
        }
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
            this.DragMove();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            var about = new About();
            about.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


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

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Registaration registration = new Registaration();
            registration.Show();
            this.Close();

        }
    }
}
