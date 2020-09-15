using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
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
        CashDesk cashDesk;
        DispatcherTimer tmrDelay = new DispatcherTimer();

        

        public SelfPurchase()
        {
            dataBase = new VeredContext();
            InitializeComponent();
            cart = new Cart(client);
            cashDesk = new CashDesk(dataBase.Sellers.FirstOrDefault());
            
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
            if (MessageBox.Show("Are You Sure Want to Delete This Product?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    ltbxCart.Items.Remove(product);
                    MessageBox.Show("Product has been delete from list", "Caution", MessageBoxButton.OK);
                   

                }
                catch (Exception)
                {

                }
            }
        }
 

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void To_Payment_Click(object sender, RoutedEventArgs e)
        {
                cashDesk.Enqueue(cart);
                var price = cashDesk.Dequeue();
                ltbxCart.Items.Clear();
                cart = new Cart(client);

                MessageBox.Show("Congratulations on your purchase!  Price: " + price, "Purchase succeed!", MessageBoxButton.OK);
          
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
            foreach (Product product in cart)
            {
                ltbxCart.Items.Add(product);
            }
                lblPrice.Content = cart.Price;
        

          
            

        }

    }
}
