#region USING
using System;
using System.Collections.Generic;
using System.Data;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VeredShopBL.VeredShopModel;
# endregion


namespace VeredShopUI
{
    public partial class Storage : Window
        
    {
        #region Defining Object and Variables
        VeredContext dataBase;
        Storekeeper storekeeper1;
        string getAllProducts = "\t\tProducts State  " + DateTime.Now + "\t\n\n" + $"{"Barcode",-30}{"Price",-20}{"OnShelf",-20}{"InStorage",-20}{"Product",-20}\n"
         #endregion

        #region Constructors
        public Storage()
        {
            dataBase = new VeredContext();
            InitializeComponent();
            showData();                       
        }

        public Storage(Storekeeper storekeeper)
        {
            dataBase = new VeredContext();
            InitializeComponent();
            storekeeper1 = storekeeper;
            showData();                  
        }
        #endregion

        #region Built Table of storage in Application
        void storageGrid_AutoGenerateColumns(object sender,DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            if (headername == "ProductId" || headername == "Sells" || headername == "Storekeeper")
                e.Cancel = true;
        }
        #endregion
         
        #region Color Row by Quantity of Products
        void storageGrid_LoadingRowEvent(object sender, DataGridRowEventArgs e)
        {

            var product = e.Row.DataContext as Product;

            if (product != null && Convert.ToInt32(product.CountOnShelf.ToString()) < 20)
            {
              e.Row.Background = new SolidColorBrush(Colors.Red);

            }
            else if (product != null && Convert.ToInt32(product.CountInStorage.ToString()) < 50)
            {
              e.Row.Background = new SolidColorBrush(Colors.Yellow);
            }
            else
            {
              e.Row.Background = new SolidColorBrush(Colors.Green);
            }
        }
        #endregion

        #region Show Table of Storage in Application
        private void showData()
        {
            storageGrid.ItemsSource = dataBase.Products.ToList();            
        }
        #endregion

        #region Allows A Window To Be Dragged By  A Mouse
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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

        #region Add Product To Storage
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            long barcode = Convert.ToInt64(txbxBarcode.Text);
            int count = Convert.ToInt32(txbxCountInStorage.Text);
            var product1 = dataBase.Products.Where(i => i.Barcode == barcode).FirstOrDefault();
            if (txbxBarcode.Text == "")
            {
                MessageBox.Show("Barcode Should be Filled!", "Warning!", MessageBoxButton.OK);
                txbxBarcode.Focus();
            }
            else if(product1 != null)
               {
                product1.CountInStorage += count;
                dataBase.SaveChanges();
                showData();
                MessageBox.Show("Added quantity to existing product");
                txbxStorekeeperID.Clear();
                txbxName.Clear();
                txbxPrice.Clear();
                txbxCountInStorage.Clear();
                txbxBarcode.Clear();
                txbxCountOnShelf.Clear();
            }
            else
            {
                var product = new Product()
                {
                    StorekeeperId = Convert.ToInt32(txbxStorekeeperID.Text),
                    Name = txbxName.Text,
                    Price = Convert.ToDecimal(txbxPrice.Text),
                    Barcode = Convert.ToInt64(txbxBarcode.Text),
                    CountInStorage = Convert.ToInt32(txbxCountInStorage.Text),
                };
                    dataBase.Products.Add(product);
                    dataBase.SaveChanges();
                    showData();
                    MessageBox.Show("New Product Is Added");
                    txbxStorekeeperID.Clear();
                    txbxName.Clear();
                    txbxPrice.Clear();
                    txbxCountInStorage.Clear();
                    txbxBarcode.Clear();
                    txbxCountOnShelf.Clear();
            }
        }
        #endregion

        #region Update Product In Storage
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                long barcode = Convert.ToInt64(txbxBarcode.Text);
                var update = dataBase.Products.Where(i => i.Barcode == barcode).FirstOrDefault();             
                int oldDataOnShelf = update.CountOnShelf;
                int newDataOnShelf = Convert.ToInt32(txbxCountOnShelf.Text);
                update.Name = txbxName.Text;
                update.Price = Convert.ToDecimal(txbxPrice.Text);
                if(oldDataOnShelf != newDataOnShelf)
                {                   
                    int subFromStorage = newDataOnShelf - oldDataOnShelf;
                    update.CountOnShelf = Convert.ToInt32(txbxCountOnShelf.Text);
                    update.CountInStorage -= subFromStorage;
                }
                else
                {
                    update.CountInStorage = Convert.ToInt32(txbxCountInStorage.Text);
                    update.CountOnShelf = Convert.ToInt32(txbxCountOnShelf.Text);
                }               
                dataBase.SaveChanges();
                showData();
                MessageBox.Show("Data is Updated");
                txbxStorekeeperID.Clear();
                txbxName.Clear();
                txbxPrice.Clear();
                txbxCountInStorage.Clear();
                txbxBarcode.Clear();
                txbxCountOnShelf.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region Delete Product From Storage
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are You Sure Want to Delete This Product?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    long barcode = Convert.ToInt64(txbxBarcode.Text);
                    var delete = dataBase.Products.Where(i => i.Barcode == barcode).FirstOrDefault();
                    dataBase.Products.Remove(delete);
                    dataBase.SaveChanges();
                    MessageBox.Show("Product has been delete.", "Caution", MessageBoxButton.OK);
                    showData();
                    txbxStorekeeperID.Clear();
                    txbxName.Clear();
                    txbxPrice.Clear();
                    txbxCountInStorage.Clear();
                    txbxBarcode.Clear();
                    txbxCountOnShelf.Clear();

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

        #region Out Data From Row To Textboxes
        private void storageGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {          
            var Data = storageGrid.SelectedItem;
            if (Data != null)
            {
                var StorekeeperId = (storageGrid.SelectedCells[5].Column.GetCellContent(Data) as TextBlock).Text;
                txbxStorekeeperID.Text = StorekeeperId;
                var Name = (storageGrid.SelectedCells[0].Column.GetCellContent(Data) as TextBlock).Text;
                txbxName.Text = Name;
                var Barcode = (storageGrid.SelectedCells[1].Column.GetCellContent(Data) as TextBlock).Text;
                txbxBarcode.Text = Barcode;
                var Price = (storageGrid.SelectedCells[2].Column.GetCellContent(Data) as TextBlock).Text;
                txbxPrice.Text = Price;
                var CountInStorage = (storageGrid.SelectedCells[4].Column.GetCellContent(Data) as TextBlock).Text;
                txbxCountInStorage.Text = CountInStorage;
                var CountOnShelf = (storageGrid.SelectedCells[3].Column.GetCellContent(Data) as TextBlock).Text;
                txbxCountOnShelf.Text = CountOnShelf;
            }
            else 
            {
                txbxStorekeeperID.Clear();
                txbxName.Clear();
                txbxPrice.Clear();
                txbxCountInStorage.Clear();
                txbxBarcode.Clear();
                txbxCountOnShelf.Clear();
            }

        }
        #endregion

        #region Search Product in Storage

        List<Product> search = new List<Product>();
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            search.Clear();
            if(txbxBarcode.Text.Equals(""))
            {
                MessageBox.Show("Search only with Barcode");
                search.AddRange(dataBase.Products);
                txbxStorekeeperID.Clear();
                txbxName.Clear();
                txbxPrice.Clear();
                txbxCountInStorage.Clear();
                txbxBarcode.Clear();
                txbxCountOnShelf.Clear();
            }
            else
            {
                foreach(Product product in dataBase.Products)
                {
                    if(product.Barcode.Equals(Convert.ToInt64(txbxBarcode.Text)))
                    {
                        search.Add(product);
                    }
                }
            }
            
            storageGrid.ItemsSource = search.ToList();
            storageGrid.RowBackground = new SolidColorBrush(Colors.Yellow);
            txbxBarcode.Clear();

        }
        #endregion

        #region Prints Product Report Form
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Product product in dataBase.Products)
                {
                    getAllProducts += $"{product.Barcode,-25}{product.Price,-22}{product.CountOnShelf,-22}{product.CountInStorage,-22}{product.Name} \n";
                }
                using (PdfDocument document = new PdfDocument())
                {

                    PdfPage page = document.Pages.Add();

                    PdfGraphics graphics = page.Graphics;

                    PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

                    graphics.DrawString(getAllProducts, font, PdfBrushes.Black, new PointF(0, 0));

                    document.Save("Products State.pdf");
                }
               
                 System.Diagnostics.Process.Start("Products State.pdf");                             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
