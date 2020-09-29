#region USING
using System;
using System.Windows;
using System.Windows.Input;
using VeredShopBL.VeredShopModel;
#endregion

namespace VeredShopUI
{
    public partial class Registaration : Window
    {
        #region Defining Object
        VeredContext dataBase;
        #endregion

        #region Constructor
        public Registaration()
        {
            InitializeComponent();
            dataBase = new VeredContext();
        }
        #endregion

        #region Allows A Window To Be Dragged By  A Mouse
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region Registration Process
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
        #endregion

        #region Exit From Application
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Back To Main window
        private void OnMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        #endregion
    }
}
