#region USING
using System;
using System.Windows;
using System.Windows.Input;
using System.Net.Mail;
using System.Net;
using VeredShopBL.VeredShopModel;
using VeredShopUI.Pattern;
#endregion

namespace VeredShopUI
{
    public partial class Registaration : Window
    {
        #region Defining Object
        VeredContext dataBase;
        Context context;
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

                            MailAddress from = new MailAddress("chdmitr2@gmail.com", "Vered Shop");

                            MailAddress to = new MailAddress(EmailBox.Text);

                            MailMessage mail = new MailMessage(from, to);

                            mail.Subject = "New Client of Vered Shop!!!";

                            mail.Body = $"<h2>Hello, You are new Client of Vered Shop!!! </h2>";

                            mail.IsBodyHtml = true;

                            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                            smtp.Credentials = new NetworkCredential("chdmitr2@gmail.com", "algebra12");
                            smtp.EnableSsl = true;
                            smtp.Send(mail);

                            MessageBox.Show("Message has been sent.", "Message", MessageBoxButton.OK);

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
                            MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK);
                            EmailBox.Text = "";
                        }
                        MessageBox.Show("You are successfully registred!");

                        context = new Context(new ConcreteStrategyF());
                        context.ContextInterface();
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
            context = new Context(new ConcreteStrategyF());
            context.ContextInterface();
            this.Close();
        }
        #endregion
    }
}
