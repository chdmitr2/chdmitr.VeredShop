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
    public partial class History : Window
    {
        VeredContext dataBase;

        public History()
        {
            dataBase = new VeredContext();
            InitializeComponent();
            showData();
        }

        private void showData()
        {
          historyGrid.ItemsSource = dataBase.Orders.ToList();
        }

        void historyGrid_AutoGenerateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            if (headername == "Client" || headername == "Sells" || headername == "Seller")
                e.Cancel = true;
        }

        #region  Allows A Window To Be Dragged By A Mouse
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        private void Box_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsLetter((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
                MessageBox.Show("Choose Date!");
            }
        }

        private void ToPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var shopUsers = new User();
            shopUsers.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
        private void historyGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        List<Order> search = new List<Order>();
        private void ToShow_Click(object sender, RoutedEventArgs e)
        {
            decimal amount = 0;
            search.Clear();

            if ((chkbxDate.IsChecked == true && chkbxOrder.IsChecked == true) )
            {
                MessageBox.Show("You can choose only one search!", "Caution", MessageBoxButton.OK);
                txbxSearchByOrder.Clear();
                dtpFrom.SelectedDate = null;
                dtpTo.SelectedDate = null;
            }
            else if (chkbxOrder.IsChecked == true)
            {
                if (txbxSearchByOrder.Text.Equals(""))
                {

                    MessageBox.Show("Enter Number of Order");
                    search.AddRange(dataBase.Orders);
                    txbxSearchByOrder.Clear();
                    dtpFrom.SelectedDate = null;
                    dtpTo.SelectedDate = null;
                }
                else
                {
                    foreach (Order order in dataBase.Orders)
                    {
                        if (order.OrderId.Equals(Convert.ToInt32(txbxSearchByOrder.Text)))
                        {
                            search.Add(order);
                        }
                    }
                    historyGrid.ItemsSource = search.ToList();
                    historyGrid.RowBackground = new SolidColorBrush(Colors.Yellow);
                    txbxSearchByOrder.Clear();
                }
            }
            else if (chkbxDate.IsChecked == true)
            {
                if(dtpFrom.Text.Equals("") || dtpTo.Text.Equals(""))
                {
                    MessageBox.Show("Enter Number of Order");
                    search.AddRange(dataBase.Orders);
                    txbxSearchByOrder.Clear();
                    dtpFrom.SelectedDate = null;
                    dtpTo.SelectedDate = null;
                }
                else if(Convert.ToDateTime(dtpFrom.Text) > Convert.ToDateTime(dtpTo.Text))
                {
                    MessageBox.Show("Wrong Range of Dates");
                    search.AddRange(dataBase.Orders);
                    txbxSearchByOrder.Clear();
                    dtpFrom.SelectedDate = null;
                    dtpTo.SelectedDate = null;
                }
                else
                {
                    foreach (Order order in dataBase.Orders)
                    {
                        if (order.Created >= Convert.ToDateTime(dtpFrom.Text) && 
                             order.Created <= Convert.ToDateTime(dtpTo.Text))
                        {
                            search.Add(order);
                            amount += order.Amount;
                        }
                    }
                    historyGrid.ItemsSource = search.ToList();
                    historyGrid.RowBackground = new SolidColorBrush(Colors.Yellow);
                    dtpFrom.SelectedDate = null;
                    dtpTo.SelectedDate = null;
                }
            }
            
        }
        
        private void txbxSearchByOrder_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
