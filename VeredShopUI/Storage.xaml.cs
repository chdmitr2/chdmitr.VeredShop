using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
    /// Interaction logic for Storage.xaml
    /// </summary>
    public partial class Storage : Window
        
    {
        VeredContext dataBase;
        public Storage()
        {
            dataBase = new VeredContext();
            InitializeComponent();
            showData();            
            
        }
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void OnMain_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (txbxName.Text == "")
            {
                MessageBox.Show("Name Should be Filled!", "Warning!", MessageBoxButton.OK);
                txbxName.Focus();
            }
            
            else
            {


                var product = new Product()
                {
                    Name = txbxName.Text,
                    Price = Convert.ToDecimal(txbxPrice.Text),
                    Barcode = Convert.ToInt64(txbxBarcode.Text),
                    CountInStorage = Convert.ToInt32(txbxCountInStorage.Text),
                };
                    dataBase.Products.Add(product);
                    dataBase.SaveChanges();
                    showData();
                    MessageBox.Show("New Product Is Added");
            }
        }

        private void showData()
        {         
            storageGrid.ItemsSource = dataBase.Products.ToList();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(txbxProductID.Text);
            var update = dataBase.Products.Where(i => i.ProductId == id).FirstOrDefault();
            update.Name = txbxName.Text;
            update.Price = Convert.ToDecimal(txbxPrice.Text);
            update.CountInStorage = Convert.ToInt32(txbxCountInStorage.Text);
            dataBase.SaveChanges();
            showData();
            MessageBox.Show("Data is Updated");

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are You Sure Want to Delete This Product?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(txbxProductID.Text);
                    var delete = dataBase.Products.Where(i => i.ProductId == id).FirstOrDefault();
                    dataBase.Products.Remove(delete);
                    dataBase.SaveChanges();
                    MessageBox.Show("Product has been delete.", "Caution", MessageBoxButton.OK);
                    showData();

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

        

        private void Show_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PriceBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void storageGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Data = storageGrid.SelectedItem;
            if (Data != null)
            {
                var Id = (storageGrid.SelectedCells[0].Column.GetCellContent(Data) as TextBlock).Text;
                txbxProductID.Text = Id;
                var Name = (storageGrid.SelectedCells[1].Column.GetCellContent(Data) as TextBlock).Text;
                txbxName.Text = Name;
                var Barcode = (storageGrid.SelectedCells[2].Column.GetCellContent(Data) as TextBlock).Text;
                txbxName.Text = Barcode;
                var Price = (storageGrid.SelectedCells[3].Column.GetCellContent(Data) as TextBlock).Text;
                txbxPrice.Text = Price;
                var CountInStorage = (storageGrid.SelectedCells[4].Column.GetCellContent(Data) as TextBlock).Text;
                txbxCountInStorage.Text = CountInStorage;
                var CountOnShelf = (storageGrid.SelectedCells[5].Column.GetCellContent(Data) as TextBlock).Text;
                txbxCountOnShelf.Text = CountOnShelf;
            }
            else {
                txbxProductID.Text = "0";
                txbxName.Text = "";
                txbxBarcode.Text = "";
                txbxPrice.Text = "0.00";
                txbxCountInStorage.Text = "0"; 
                txbxCountOnShelf.Text = "0";
            }

        }
    }
}
