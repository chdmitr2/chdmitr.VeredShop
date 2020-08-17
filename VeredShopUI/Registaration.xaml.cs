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
    /// Interaction logic for Registaration.xaml
    /// </summary>
    public partial class Registaration : Window
    {
        VeredContext dataBase;
        public Registaration()
        {
            InitializeComponent();
            dataBase = new VeredContext();
        }
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (NameBox.Text == "" || SurnameBox.Text == "" || EmailBox.Text == "" || PassBox1.Password == "")
                {
                    MessageBox.Show("Fill in blank fields!");
                }
                else
                {
                    if (NameBox.Text.Length > 20)
                    {
                        MessageBox.Show("FirstName too long! Try enter shorter name.");
                    }
                    else if (SurnameBox.Text.Length > 20)
                    {
                        MessageBox.Show("LastName too long! Try enter shorter surname.");
                    }
                    else if (EmailBox.Text.Length > 30)
                    {
                        MessageBox.Show("Email not more 30 symbols!");
                    }
                    else if (EmailBox.Text.Length < 7)
                    {
                        MessageBox.Show("Email not more 7 symbols");
                    }
                    else if (PassBox1.Password.Length > 15)
                    {
                        MessageBox.Show("Password not more 15 symbols");
                    }
                    else if (PassBox1.Password.Length < 5)
                    {
                        MessageBox.Show("Password not shorter 5 symbols!");
                    }
                    else if (PassBox1.Password != PassBox2.Password)
                    {
                        MessageBox.Show("Repeat password correctly!");
                        PassBox2.Password = "";
                    }
                    else
                    {
                        try
                        {

                            var client = new Client()
                            {
                                FirstName = NameBox.Text,
                                LastName = SurnameBox.Text,
                                Email = EmailBox.Text,
                                Password = PassBox1.Password,
                            };
                            dataBase.Clients.Add(client);
                            dataBase.SaveChanges();
                            
                        }
                        
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        MessageBox.Show("You are successfully registred!");

                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Wrong data!!!.");
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
