using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using VeredShopBL.VeredShopModel;

namespace VeredShopUI
{
    public partial class History : Window
    {
        VeredContext dataBase;
        string getOrders = "";
        string getAllClientPurchase = "";

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
            if (chkbxOrder.IsChecked == true)
            {
                MessageBox.Show("Not Print One Row of Data");
            }
            if (chkbxDate.IsChecked == true)
            {
                try
                {
                    using (PdfDocument document = new PdfDocument())
                    {

                        PdfPage page = document.Pages.Add();

                        PdfGraphics graphics = page.Graphics;

                        PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

                        graphics.DrawString(getOrders, font, PdfBrushes.Black, new PointF(0, 0));

                        document.Save("Orders State.pdf");
                    }

                    System.Diagnostics.Process.Start("Orders State.pdf");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (chkbxClient.IsChecked == true)
            {
                try
                {
                    using (PdfDocument document = new PdfDocument())
                    {

                        PdfPage page = document.Pages.Add();

                        PdfGraphics graphics = page.Graphics;

                        PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

                        graphics.DrawString(getAllClientPurchase, font, PdfBrushes.Black, new PointF(0, 0));

                        document.Save("Client Purchaces.pdf");
                    }

                    System.Diagnostics.Process.Start("Client Purchaces.pdf");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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
            
        List<Order> search = new List<Order>();
        private void ToShow_Click(object sender, RoutedEventArgs e)
        {
            decimal clientAmount = 0;
            decimal ordersAmount = 0;
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
                    txbxSearchByClient.Clear();
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
                    txbxSearchByClient.Clear();
                    dtpFrom.SelectedDate = null;
                    dtpTo.SelectedDate = null;
                }
                else if(Convert.ToDateTime(dtpFrom.Text) > Convert.ToDateTime(dtpTo.Text))
                {
                    MessageBox.Show("Wrong Range of Dates");
                    search.AddRange(dataBase.Orders);
                    txbxSearchByOrder.Clear();
                    txbxSearchByClient.Clear();
                    dtpFrom.SelectedDate = null;
                    dtpTo.SelectedDate = null;
                }
                else
                {
                    getOrders = "\t\tOrders State from " + Convert.ToDateTime(dtpFrom.Text) + " to " + Convert.ToDateTime(dtpTo.Text) +
                        "\t\n\n" + $"{"OrderID",-20}{"Date",-30}{"Amount",-15}{"SellerID",-20}{"Seller",-20}{"ClientID",-15}{"Client"}\n";
                    foreach (Order order in dataBase.Orders)
                    {
                        if (order.Created >= Convert.ToDateTime(dtpFrom.Text) && 
                             order.Created <= Convert.ToDateTime(dtpTo.Text))
                        {
                            search.Add(order);
                            var seller = dataBase.Sellers.Where(i => i.SellerId == order.SellerId).FirstOrDefault();
                            var client = dataBase.Clients.Where(i => i.ClientId == order.ClientId).FirstOrDefault();
                            getOrders += $"{order.OrderId,-15}{order.Created,-30}{order.Amount,-18}{order.SellerId,-15}{seller.FirstName,-1}{seller.LastName,-22}" +
                                $"{order.ClientId,-8}{client.FirstName,-1}{client.LastName} \n";
                            ordersAmount += order.Amount;
                        }
                    }
                    getOrders += $"\nThe Amount of Orders is: " + ordersAmount;
                    historyGrid.ItemsSource = search.ToList();                   
                    dtpFrom.SelectedDate = null;
                    dtpTo.SelectedDate = null;
                }
            }
            else if (chkbxClient.IsChecked == true)
            {
                if (txbxSearchByClient.Text.Equals(""))
                {

                    MessageBox.Show("Enter ID Number of Client");
                    search.AddRange(dataBase.Orders);
                    txbxSearchByOrder.Clear();
                    txbxSearchByClient.Clear();
                    dtpFrom.SelectedDate = null;
                    dtpTo.SelectedDate = null;
                }
                else
                {
                   getAllClientPurchase = "\t\tClient Purchase State  " + DateTime.Now + "\t\n\n" +
                        $"{"OrderID",-20}{"Date",-30}{"Amount",-15}{"SellerID",-20}{"Seller",-20}{"ClientID",-15}{"Client"}\n";
                    foreach (Order order in dataBase.Orders)
                    {
                        if (order.ClientId.Equals(Convert.ToInt32(txbxSearchByClient.Text)))
                        {
                            search.Add(order);
                            var seller = dataBase.Sellers.Where(i => i.SellerId == order.SellerId).FirstOrDefault();
                            var client = dataBase.Clients.Where(i => i.ClientId == order.ClientId).FirstOrDefault();
                            getAllClientPurchase += $"{order.OrderId,-15}{order.Created,-30}{order.Amount,-18}{order.SellerId,-15}{seller.FirstName,-1}{seller.LastName,-22}" +
                                $"{order.ClientId,-8}{client.FirstName,-1}{client.LastName} \n";
                            clientAmount += order.Amount;
                        }
                    }
                    getAllClientPurchase += $"\nThe Amount of  Client Purchaces is: " + clientAmount;
                    historyGrid.ItemsSource = search.ToList();
                    dtpFrom.SelectedDate = null;
                    dtpTo.SelectedDate = null;
                }
            }
        }             
    }
}
