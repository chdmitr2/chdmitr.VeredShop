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

namespace VeredShopUI
{
    /// <summary>
    /// Interaction logic for SelfPurchase.xaml
    /// </summary>
    public partial class SelfPurchase : Window
    {
        public SelfPurchase()
        {
            InitializeComponent();
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

        }
    }
}
