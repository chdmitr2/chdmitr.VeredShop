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
            storekeeperGrid.ItemsSource = dataBase.Storekeepers.ToList();
        }
        void clientGrid_AutoGenerateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            if (headername == "Orders")
                e.Cancel = true;
        }
        void sellerGrid_AutoGenerateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            if (headername == "Orders")
                e.Cancel = true;
        }
        void storekeeperGrid_AutoGenerateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            if (headername == "Products")
                e.Cancel = true;
        }

        #region  Allows A Window To Be Dragged By A Mouse
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region Add User (Client,Seller,Storekeeper)
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((chkbx1.IsChecked == true && chkbx2.IsChecked == true) || (chkbx1.IsChecked == true && chkbx3.IsChecked == true) ||
               (chkbx3.IsChecked == true && chkbx2.IsChecked == true) || (chkbx1.IsChecked == true && chkbx2.IsChecked == true && chkbx3.IsChecked == true))
                {
                    MessageBox.Show("You can choose only one table!", "Caution", MessageBoxButton.OK);
                    txbxUserFirstName.Clear();
                    txbxUserLastName.Clear();
                    txbxEmail.Clear();
                    txbxPassword.Clear();
                    txbxSalary.Clear();
                }

                if (txbxUserFirstName.Text == "" || txbxUserLastName.Text == "" || txbxEmail.Text == "" || txbxPassword.Text == "")
                {
                    MessageBox.Show("Name Should be Filled!", "Warning!", MessageBoxButton.OK);
                    txbxUserFirstName.Focus();
                    txbxUserLastName.Focus();
                    txbxEmail.Focus();
                    txbxPassword.Focus();
                }
                else if (chkbx1.IsChecked == true)
                {
                    txbxSalary.IsEnabled = false;
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
                    txbxUserFirstName.Clear();
                    txbxUserLastName.Clear();
                    txbxEmail.Clear();
                    txbxPassword.Clear();
                    txbxSalary.Clear();

                }

                if (chkbx2.IsChecked == true)
                {
                    if (txbxUserFirstName.Text == "" || txbxUserLastName.Text == "" || txbxEmail.Text == "" || txbxPassword.Text == "" || txbxSalary.Text == "")
                    {
                        MessageBox.Show("Name Should be Filled!", "Warning!", MessageBoxButton.OK);
                        txbxUserFirstName.Focus();
                        txbxUserLastName.Focus();
                        txbxEmail.Focus();
                        txbxPassword.Focus();
                        txbxSalary.Focus();
                    }
                    var seller = new Seller()
                    {
                        FirstName = txbxUserFirstName.Text,
                        LastName = txbxUserLastName.Text,
                        Email = txbxEmail.Text,
                        Password = txbxPassword.Text,
                        Salary = Convert.ToInt32(txbxSalary.Text),
                    };
                    dataBase.Sellers.Add(seller);
                    dataBase.SaveChanges();
                    showData();
                    MessageBox.Show("New Seller Is Added");
                    txbxUserFirstName.Clear();
                    txbxUserLastName.Clear();
                    txbxEmail.Clear();
                    txbxPassword.Clear();
                    txbxSalary.Clear();
                }

                if (chkbx3.IsChecked == true)
                {
                    if (txbxUserFirstName.Text == "" || txbxUserLastName.Text == "" || txbxEmail.Text == "" || txbxPassword.Text == "" || txbxSalary.Text == "")
                    {
                        MessageBox.Show("Name Should be Filled!", "Warning!", MessageBoxButton.OK);
                        txbxUserFirstName.Focus();
                        txbxUserLastName.Focus();
                        txbxEmail.Focus();
                        txbxPassword.Focus();
                        txbxSalary.Focus();
                    }
                    var storekeeper = new Storekeeper()
                    {
                        FirstName = txbxUserFirstName.Text,
                        LastName = txbxUserLastName.Text,
                        Email = txbxEmail.Text,
                        Password = txbxPassword.Text,
                        Salary = Convert.ToInt32(txbxSalary.Text),
                    };
                    dataBase.Storekeepers.Add(storekeeper);
                    dataBase.SaveChanges();
                    showData();
                    MessageBox.Show("New Storekepper Is Added");
                    txbxUserFirstName.Clear();
                    txbxUserLastName.Clear();
                    txbxEmail.Clear();
                    txbxPassword.Clear();
                    txbxSalary.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion
        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if ((chkbx1.IsChecked == true && chkbx2.IsChecked == true) || (chkbx1.IsChecked == true && chkbx3.IsChecked == true) ||
               (chkbx3.IsChecked == true && chkbx2.IsChecked == true) || (chkbx1.IsChecked == true && chkbx2.IsChecked == true && chkbx3.IsChecked == true))
            {
                MessageBox.Show("You can choose only one table!", "Caution", MessageBoxButton.OK);
                txbxUserFirstName.Clear();
                txbxUserLastName.Clear();
                txbxEmail.Clear();
                txbxPassword.Clear();
                txbxSalary.Clear();
            }
            else if (chkbx1.IsChecked == true)
            {
                try
                {
                    string email = txbxEmail.Text;
                    var update = dataBase.Clients.Where(i => i.Email == email).FirstOrDefault();

                    update.FirstName = txbxUserFirstName.Text;
                    update.LastName = txbxUserLastName.Text;
                    update.Email = txbxEmail.Text;
                    update.Password = txbxPassword.Text;

                    dataBase.SaveChanges();
                    showData();
                    MessageBox.Show("Data is Updated");
                    txbxUserFirstName.Clear();
                    txbxUserLastName.Clear();
                    txbxEmail.Clear();
                    txbxPassword.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else if (chkbx2.IsChecked == true)
            {
                try
                {
                    string email = txbxEmail.Text;
                    var update = dataBase.Sellers.Where(i => i.Email == email).FirstOrDefault();

                    update.FirstName = txbxUserFirstName.Text;
                    update.LastName = txbxUserLastName.Text;
                    update.Email = txbxEmail.Text;
                    update.Password = txbxPassword.Text;
                    update.Salary= Convert.ToInt32(txbxSalary.Text);

                    dataBase.SaveChanges();
                    showData();
                    MessageBox.Show("Data is Updated");
                    txbxUserFirstName.Clear();
                    txbxUserLastName.Clear();
                    txbxEmail.Clear();
                    txbxPassword.Clear();
                    txbxSalary.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

           else if (chkbx3.IsChecked == true)
            {
                try
                {
                    string email = txbxEmail.Text;
                    var update = dataBase.Storekeepers.Where(i => i.Email == email).FirstOrDefault();

                    update.FirstName = txbxUserFirstName.Text;
                    update.LastName = txbxUserLastName.Text;
                    update.Email = txbxEmail.Text;
                    update.Password = txbxPassword.Text;
                    update.Salary = Convert.ToInt32(txbxSalary.Text);

                    dataBase.SaveChanges();
                    showData();
                    MessageBox.Show("Data is Updated");
                    txbxUserFirstName.Clear();
                    txbxUserLastName.Clear();
                    txbxEmail.Clear();
                    txbxPassword.Clear();
                    txbxSalary.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        #region Delete User (Client,Seller,Storekeeper)
        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var Data = clientGrid.SelectedItem;
            var Data2 = sellerGrid.SelectedItem;
            var Data3 = storekeeperGrid.SelectedItem;

            if ((chkbx1.IsChecked == true && chkbx2.IsChecked == true) || (chkbx1.IsChecked == true && chkbx3.IsChecked == true)||
                (chkbx3.IsChecked == true && chkbx2.IsChecked == true) ||  (chkbx1.IsChecked == true && chkbx2.IsChecked == true && chkbx3.IsChecked == true))
            {
                MessageBox.Show("You can choose only one table!", "Caution", MessageBoxButton.OK);
                txbxUserFirstName.Clear();
                txbxUserLastName.Clear();
                txbxEmail.Clear();
                txbxPassword.Clear();
                txbxSalary.Clear();
            }

            
            else  if (chkbx1.IsChecked == true)
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
                        txbxUserFirstName.Clear();
                        txbxUserLastName.Clear();
                        txbxEmail.Clear();
                        txbxPassword.Clear();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            
           else  if (chkbx2.IsChecked == true)
           {
                if (MessageBox.Show("Are You Sure Want to Delete This Seller?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var str = (sellerGrid.SelectedCells[0].Column.GetCellContent(Data2) as TextBlock).Text;
                        int id = Convert.ToInt32(str);
                        var delete = dataBase.Sellers.Where(i => i.SellerId == id).FirstOrDefault();
                        dataBase.Sellers.Remove(delete);
                        dataBase.SaveChanges();
                        MessageBox.Show("Seller has been delete.", "Caution", MessageBoxButton.OK);
                        showData();
                        txbxUserFirstName.Clear();
                        txbxUserLastName.Clear();
                        txbxEmail.Clear();
                        txbxPassword.Clear();
                        txbxSalary.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
           } 

           
             else if (chkbx3.IsChecked == true)
             {
                if (MessageBox.Show("Are You Sure Want to Delete This Storekeeper?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var str = (storekeeperGrid.SelectedCells[0].Column.GetCellContent(Data3) as TextBlock).Text;
                        int id = Convert.ToInt32(str);
                        var delete = dataBase.Storekeepers.Where(i => i.StorekeeperId == id).FirstOrDefault();
                        dataBase.Storekeepers.Remove(delete);
                        dataBase.SaveChanges();
                        MessageBox.Show("Storekeeper has been delete.", "Caution", MessageBoxButton.OK);
                        showData();
                        txbxUserFirstName.Clear();
                        txbxUserLastName.Clear();
                        txbxEmail.Clear();
                        txbxPassword.Clear();
                        txbxSalary.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }
        #endregion

        #region Back To Main Menu
        private void OnMain_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }

        #endregion

        #region Exit From Application
        private void ExitUser_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        protected void clientGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Data = clientGrid.SelectedItem;
            if (chkbx1.IsChecked == true)
            {
                txbxSalary.Visibility = Visibility.Hidden;
                lblSalary.Visibility = Visibility.Hidden;
                if (Data != null)
                {
                    var FirstName = (clientGrid.SelectedCells[1].Column.GetCellContent(Data) as TextBlock).Text;
                    txbxUserFirstName.Text = FirstName;
                    var LastName = (clientGrid.SelectedCells[2].Column.GetCellContent(Data) as TextBlock).Text;
                    txbxUserLastName.Text = LastName;
                    var Email = (clientGrid.SelectedCells[3].Column.GetCellContent(Data) as TextBlock).Text;
                    txbxEmail.Text = Email;
                    var Password = (clientGrid.SelectedCells[4].Column.GetCellContent(Data) as TextBlock).Text;
                    txbxPassword.Text = Password;
                }
                else
                {
                    txbxUserFirstName.Clear();
                    txbxUserLastName.Clear();
                    txbxEmail.Clear();
                    txbxPassword.Clear();
                }
            }

           
        }
        protected void sellerGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Data2 = sellerGrid.SelectedItem;
            if (chkbx2.IsChecked == true)
            {
                if (Data2 != null)
                {
                    var FirstName = (sellerGrid.SelectedCells[1].Column.GetCellContent(Data2) as TextBlock).Text;
                    txbxUserFirstName.Text = FirstName;
                    var LastName = (sellerGrid.SelectedCells[2].Column.GetCellContent(Data2) as TextBlock).Text;
                    txbxUserLastName.Text = LastName;
                    var Email = (sellerGrid.SelectedCells[3].Column.GetCellContent(Data2) as TextBlock).Text;
                    txbxEmail.Text = Email;
                    var Password = (sellerGrid.SelectedCells[4].Column.GetCellContent(Data2) as TextBlock).Text;
                    txbxPassword.Text = Password;
                    var Salary = (sellerGrid.SelectedCells[5].Column.GetCellContent(Data2) as TextBlock).Text;
                    txbxSalary.Text = Salary;
                }
                else
                {
                    txbxUserFirstName.Clear();
                    txbxUserLastName.Clear();
                    txbxEmail.Clear();
                    txbxPassword.Clear();
                    txbxSalary.Clear();
                }
            }
           
        }
        protected void storekeeperGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Data3 = storekeeperGrid.SelectedItem;
            if(chkbx3.IsChecked == true)
            {
                if (Data3 != null)
                {
                    var FirstName3 = (storekeeperGrid.SelectedCells[1].Column.GetCellContent(Data3) as TextBlock).Text;
                    txbxUserFirstName.Text = FirstName3;
                    var LastName3 = (storekeeperGrid.SelectedCells[2].Column.GetCellContent(Data3) as TextBlock).Text;
                    txbxUserLastName.Text = LastName3;
                    var Email3 = (storekeeperGrid.SelectedCells[3].Column.GetCellContent(Data3) as TextBlock).Text;
                    txbxEmail.Text = Email3;
                    var Password3 = (storekeeperGrid.SelectedCells[4].Column.GetCellContent(Data3) as TextBlock).Text;
                    txbxPassword.Text = Password3;
                    var Salary3 = (storekeeperGrid.SelectedCells[5].Column.GetCellContent(Data3) as TextBlock).Text;
                    txbxSalary.Text = Salary3;
                }
                else
                {
                    txbxUserFirstName.Clear();
                    txbxUserLastName.Clear();
                    txbxEmail.Clear();
                    txbxPassword.Clear();
                    txbxSalary.Clear();
                }
            }
           
        }
  

        private void History_Click(object sender, RoutedEventArgs e)
        {
            History History = new History();
            History.Show();
            this.Close();
        }

        List<Client> search = new List<Client>();
        private void searchClient_Click(object sender, RoutedEventArgs e)
        {
            search.Clear();
            if (txbxUserFirstName.Text.Equals("") && txbxUserFirstName.Text.Equals("") && txbxEmail.Text.Equals(""))
            {
                MessageBox.Show("Enter Data search");
                search.AddRange(dataBase.Clients);
                clientGrid.ItemsSource = search.ToList();
                txbxUserFirstName.Clear();
                txbxUserLastName.Clear();
                txbxEmail.Clear();
                txbxPassword.Clear();
                txbxSalary.Clear();
            }
            else
            {
                foreach (Client client in dataBase.Clients)
                {
                    if (client.FirstName.Equals(txbxUserFirstName.Text) || client.LastName.Equals(txbxUserLastName.Text)
                        || client.FirstName.Equals(txbxUserFirstName.Text))
                    {
                        search.Add(client);
                    }
                }

                clientGrid.ItemsSource = search.ToList();
                clientGrid.RowBackground = new SolidColorBrush(Colors.Yellow);
                txbxUserFirstName.Clear();
                txbxUserLastName.Clear();
                txbxEmail.Clear();
                txbxPassword.Clear();
                txbxSalary.Clear();
            }

        }
    }
}
