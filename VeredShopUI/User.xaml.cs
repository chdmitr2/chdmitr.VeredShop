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
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        VeredContext dataBase;
  
        public User()
        {
            dataBase = new VeredContext();
            InitializeComponent();
            showData();
        }

        private void showData()
        {
           clientGrid.ItemsSource = dataBase.Clients.ToList();
           sellerGrid.ItemsSource = dataBase.Sellers.ToList();
           
        }

        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (txbxUserFirstName.Text == "")
            {
                MessageBox.Show("Name Should be Filled!", "Warning!", MessageBoxButton.OK);
                txbxUserFirstName.Focus();
            }

            else
            {
                if (chkbx1.IsChecked == true)
                {
                    var client = new Client()
                    {
                        FirstName = txbxUserFirstName.Text,
                        LastName = txbxUserLastName.Text,
                        Email = txbxEmail.Text,
                        Password = txbxPassword.Text,
                    };
                    dataBase.Clients.Add(client);
                    dataBase.SaveChanges();
                    showData();
                    MessageBox.Show("New Client Is Added");
                }

                if (chkbx2.IsChecked == true)
                {
                    var seller = new Seller()
                    {
                        FirstName = txbxUserFirstName.Text,
                       
                    };
                    dataBase.Sellers.Add(seller);
                    dataBase.SaveChanges();
                    showData();
                    MessageBox.Show("New Seller Is Added");
                }


            }
        }

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var Data = clientGrid.SelectedItem;
            if (chkbx1.IsChecked == true)
            {
                if (MessageBox.Show("Are You Sure Want to Delete This Client?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var str = (clientGrid.SelectedCells[0].Column.GetCellContent(Data) as TextBlock).Text;
                        int id = Convert.ToInt32(str);
                        var delete = dataBase.Clients.Where(i => i.ClientId == id).FirstOrDefault();
                        dataBase.Clients.Remove(delete);
                        dataBase.SaveChanges();
                        MessageBox.Show("Client has been delete.", "Caution", MessageBoxButton.OK);
                        showData();

                    }
                    catch (Exception)
                    {

                    }
            }   }
        }

        private void OnMain_Click(object sender, RoutedEventArgs e)
        {
           Menu menu = new Menu();
            menu.Show();
            this.Close();
        }

        private void ExitUser_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected void userGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Data = clientGrid.SelectedItem;
            var Data2 = sellerGrid.SelectedItem;
            var Data3 = storekeeperGrid.SelectedItem;

            if (Data != null)
            {
                var FirstName = (clientGrid.SelectedCells[0].Column.GetCellContent(Data) as TextBlock).Text;
                txbxUserFirstName.Text = FirstName;
                var LastName = (clientGrid.SelectedCells[1].Column.GetCellContent(Data) as TextBlock).Text;
                txbxUserLastName.Text = LastName;
                
            }
            else
            {
                txbxUserFirstName.Text = " ";
                txbxUserLastName.Text = " ";
                txbxEmail.Text = " ";
                txbxPassword.Text = " ";
            }
            if (Data2 != null)
            {
                var Id = (clientGrid.SelectedCells[0].Column.GetCellContent(Data2) as TextBlock).Text;
                txbxUserFirstName.Text = Id;
                var Name = (clientGrid.SelectedCells[1].Column.GetCellContent(Data2) as TextBlock).Text;
                txbxUserLastName.Text = Name;
            }
            else
            {
                txbxUserFirstName.Text = " ";
                txbxUserLastName.Text = " ";
                txbxEmail.Text = " ";
                txbxPassword.Text = " ";
            }
            if (Data3 != null)
            {
                var Id = (clientGrid.SelectedCells[0].Column.GetCellContent(Data3) as TextBlock).Text;
                txbxUserFirstName.Text = Id;
                var Name = (clientGrid.SelectedCells[1].Column.GetCellContent(Data3) as TextBlock).Text;
                txbxUserLastName.Text = Name;
                var Price = (clientGrid.SelectedCells[2].Column.GetCellContent(Data3) as TextBlock).Text;
                txbxEmail.Text = Price;
                var Count = (clientGrid.SelectedCells[3].Column.GetCellContent(Data3) as TextBlock).Text;
                txbxPassword.Text = Count;
            }
            else
            {
                txbxUserFirstName.Text = " ";
                txbxUserLastName.Text = " ";
                txbxEmail.Text = " ";
                txbxPassword.Text = " ";
            }
        }
    }
}
