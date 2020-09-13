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
    /// Interaction logic for SelfPurchase.xaml
    /// </summary>
    public partial class SelfPurchase : Window
    {
        VeredContext dataBase; 
        Cart cart;
        Client client;
        System.Windows.Threading.DispatcherTimer tmrDelay = new System.Windows.Threading.DispatcherTimer();

        

        public SelfPurchase()
        {
            dataBase = new VeredContext();
            InitializeComponent();
            cart = new Cart(client);
        }

        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ToMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Product_from_Cart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Product_to_Cart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void To_Payment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ltbxCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txbxBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
               
                   if (txbxBarcode.Text.Trim().Length == 1)
                   {

                    tmrDelay.Start();
                    tmrDelay.Tick += new EventHandler(tmrDelay_Tick);
                   }
               

            }

            catch
            {

            }
        }
            void tmrDelay_Tick(object sender, EventArgs e)
            {
                try
                {
                    tmrDelay.Stop();
                    string strCurrentString = txbxBarcode.Text.Trim().ToString();
                    if (strCurrentString != "")
                    {
                    if (long.Parse(txbxBarcode.Text) > 999999999999)
                    {
                        long barcode = Convert.ToInt64(txbxBarcode.Text);

                        Product product = dataBase.Products.Where(i => i.Barcode == barcode).FirstOrDefault();

                        cart.Add(product);
                        ltbxCart.Items.Add(product);
                        UpdateLists();
                    }
                    else
                    {
                        MessageBox.Show("Enter Barcode of Product.", "Caution", MessageBoxButton.OK);
                    }
                    txbxBarcode.Text = "";
                    }
                    txbxBarcode.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


       

        private void UpdateLists()
        {
            ltbxCart.Items.Clear();
            ltbxCart.Items.Add(cart.GetAll().ToArray());
           
        }
    }
}
