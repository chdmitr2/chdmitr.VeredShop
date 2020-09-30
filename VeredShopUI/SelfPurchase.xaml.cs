#region USING
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using VeredShopBL.VeredShopModel;
using System.Windows.Media;
#endregion


namespace VeredShopUI
{
   
    public partial class SelfPurchase : Window
    {
        #region Defining Objets
        VeredContext dataBase; 
        Cart cart;
        Client client;
        CashDesk cashDesk;
        DispatcherTimer tmrDelay = new DispatcherTimer();
        #endregion

        #region Initializing Self Purchase Window (Constructors)

      
        public SelfPurchase(Client client)
        {
            dataBase = new VeredContext();
            InitializeComponent();
            cart = new Cart(client);
            cashDesk = new CashDesk(client);
            
                clientLabel.Content = $"Hello, {client.FirstName}";
                clientLabel.FontSize = 16;
                clientLabel.FontFamily = new FontFamily("SegoePrint");
                clientLabel.FontWeight = FontWeights.Bold;
           
            
        }
        #endregion

        #region Allows A Window To Be Dragged By  A Mouse
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region Back To Main Menu
        private void ToMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
        #endregion

        #region Remove Product From Cart
        private void Remove_Product_from_Cart_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are You Sure Want to Delete This Product?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {                  
                    if(ltbxCart.SelectedItem != null)
                    {
                        foreach (Product product in cart)
                        {
                           if(ltbxCart.SelectedItem == product)
                            {
                              cart.Remove(product);
                                break;
                            }
                           
                        }
                        ltbxCart.Items.Remove(ltbxCart.SelectedItem);
                        UpdateLists();
                        
                      
                    }
                    
                    MessageBox.Show("Product has been delete from list", "Caution", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region Exit From Application
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Payment
        private void To_Payment_Click(object sender, RoutedEventArgs e)
        {
                var price = cashDesk.SelfPurchase(cart);
                MessageBox.Show("Congratulations on your purchase!  Price: " + price + " Check email to see your bill ", "Purchase succeed! ", MessageBoxButton.OK);              
              
               if (MessageBox.Show("Do you want to view the bill which send in mail?", "Bill has been created",MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
               { 
                    try
                    {                   
                      System.Diagnostics.Process.Start("Bill.pdf");
                    }
                    catch (Exception ex)
                    {
                    MessageBox.Show(ex.Message);
                    }
               }
            
           
                ltbxCart.Items.Clear();
                cart = new Cart(client);

        }
        #endregion

        #region ListBox Cart
        private void ltbxCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        #endregion

        #region Add Products To Cart
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

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        #endregion

        #region Update Cart List
        private void UpdateLists()
        {
            try
            {
                ltbxCart.Items.Clear();
                foreach (Product product in cart)
                {
                    ltbxCart.FontSize = 16;
                    ltbxCart.FontFamily = new FontFamily("SegoePrint");
                    ltbxCart.FontWeight = FontWeights.Bold;
                    ltbxCart.Items.Add(product);
                }
                lblPrice.FontSize = 20;
                lblPrice.FontFamily = new FontFamily("SegoePrint");
                lblPrice.FontWeight = FontWeights.Bold;
                lblPrice.Content = cart.Price;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

    }
}
