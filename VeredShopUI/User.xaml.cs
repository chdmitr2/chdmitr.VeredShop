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
            if (txbxUserName.Text == "")
            {
                MessageBox.Show("Name Should be Filled!", "Warning!", MessageBoxButton.OK);
                txbxUserName.Focus();
            }

            else
            {
               
                    var client = new Client()
                    {
                        Name = txbxUserName.Text,
                    };
                    dataBase.Clients.Add(client);
                    dataBase.SaveChanges();
                    showData();
                    MessageBox.Show("New Client Is Added");
               

            }
        }

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnMain_Click(object sender, RoutedEventArgs e)
        {

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
                var Id = (clientGrid.SelectedCells[0].Column.GetCellContent(Data) as TextBlock).Text;
                txbxUserID.Text = Id;
                var Name = (clientGrid.SelectedCells[1].Column.GetCellContent(Data) as TextBlock).Text;
                txbxUserName.Text = Name;
                
            }
            else
            {
                txbxUserID.Text = "0";
                txbxUserName.Text = "Name";
                txbxPhoneNumber.Text = "Phone";
                txbxEmail.Text = "Mail";
            }
            if (Data2 != null)
            {
                var Id = (clientGrid.SelectedCells[0].Column.GetCellContent(Data2) as TextBlock).Text;
                txbxUserID.Text = Id;
                var Name = (clientGrid.SelectedCells[1].Column.GetCellContent(Data2) as TextBlock).Text;
                txbxUserName.Text = Name;
            }
            else
            {
                txbxUserID.Text = "0";
                txbxUserName.Text = "Name";
                txbxPhoneNumber.Text = "Phone";
                txbxEmail.Text = "Mail";
            }
            if (Data3 != null)
            {
                var Id = (clientGrid.SelectedCells[0].Column.GetCellContent(Data3) as TextBlock).Text;
                txbxUserID.Text = Id;
                var Name = (clientGrid.SelectedCells[1].Column.GetCellContent(Data3) as TextBlock).Text;
                txbxUserName.Text = Name;
                var Price = (clientGrid.SelectedCells[2].Column.GetCellContent(Data3) as TextBlock).Text;
                txbxPhoneNumber.Text = Price;
                var Count = (clientGrid.SelectedCells[3].Column.GetCellContent(Data3) as TextBlock).Text;
                txbxEmail.Text = Count;
            }
            else
            {
                txbxUserID.Text = "0";
                txbxUserName.Text = "Name";
                txbxPhoneNumber.Text = "Phone";
                txbxEmail.Text = "Mail";
            }
        }
    }
}
