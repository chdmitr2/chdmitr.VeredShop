using System;
using System.Collections.Generic;
using System.IO.Packaging;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VeredShopBL.VeredShopModel;

namespace VeredShopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Menu : Window
    {
        VeredContext dataBase;
        Client client1;
        Seller seller1;
        Storekeeper storekeeper1;
        public Menu()
        {


            InitializeComponent();
            dataBase = new VeredContext();

        }

        public Menu (Client client)
        {

            InitializeComponent();
            dataBase = new VeredContext();
            client1 = client;
        }

        public Menu(Seller seller)
        {

            InitializeComponent();
            dataBase = new VeredContext();
            seller1 = seller;
        }
        public Menu(Storekeeper storekeeper)
        {

            InitializeComponent();
            dataBase = new VeredContext();
            storekeeper1 = storekeeper;
        }


        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void OnMain_Click(object sender, RoutedEventArgs e)
        {
             MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

      

        private void buyThroughPos_Click(object sender, RoutedEventArgs e)
        {
            var buyThroughPos = new buyThroughPos(seller1);
            buyThroughPos.Show();
            this.Close();
        }
      
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Storage_Click(object sender, RoutedEventArgs e)
        {
            var storageProduct = new Storage();
            storageProduct.Show();
            this.Close();
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            var shopUsers = new User();
            shopUsers.Show();
            this.Close();
        }

        private void Self_Purchase_Click(object sender, RoutedEventArgs e)
        {
            var selfPurchase = new SelfPurchase(client1);
            selfPurchase.Show();
            this.Close();
        }
    }
}
