﻿using System;
using System.Collections;
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
    public partial class Storage : Window
        
    {
        VeredContext dataBase;
        Storekeeper storekeeper1;

        
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


        void storageGrid_AutoGenerateColumns(object sender,DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            if (headername == "ProductId" || headername == "Sells" || headername == "Storekeeper")
                e.Cancel = true;
        }

        void storageGrid_LoadingRowEvent(object sender, DataGridRowEventArgs e)
        {


            //      DataGridRow row = e.Row;
            //  DataRowView rView = row.Item as DataRowView;
            //  foreach (rView in storageGrid.ItemsSource)
            //  { 
            //  string text = rView.Row.ItemArray[4].ToString();
            //  int count = Convert.ToInt32(text);
            // if (count < 5)
            //  {
            //     e.Row.Background = Brushes.Red;
            //  }
            //  else
            //  {
            //      e.Row.Background = Brushes.Green;
            // }
            ///  }

              if (Convert.ToInt32(((DataRowView)e.Row.DataContext).Row.ItemArray[4].ToString()) < 5)
              {
             e.Row.Background = new SolidColorBrush(Colors.Red);
             }
             else 
             {
               e.Row.Background = new SolidColorBrush(Colors.Green);
              }


        }

        
        private void showData()
        {
            storageGrid.ItemsSource = dataBase.Products.ToList();
            
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are You Sure Want to Delete This Product?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    long barcode = Convert.ToInt32(txbxBarcode.Text);
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
                var StorekeeperId = (storageGrid.SelectedCells[0].Column.GetCellContent(Data) as TextBlock).Text;
                txbxStorekeeperID.Text = StorekeeperId;
                var Name = (storageGrid.SelectedCells[1].Column.GetCellContent(Data) as TextBlock).Text;
                txbxName.Text = Name;
                var Barcode = (storageGrid.SelectedCells[2].Column.GetCellContent(Data) as TextBlock).Text;
                txbxBarcode.Text = Barcode;
                var Price = (storageGrid.SelectedCells[3].Column.GetCellContent(Data) as TextBlock).Text;
                txbxPrice.Text = Price;
                var CountInStorage = (storageGrid.SelectedCells[5].Column.GetCellContent(Data) as TextBlock).Text;
                txbxCountInStorage.Text = CountInStorage;
                var CountOnShelf = (storageGrid.SelectedCells[4].Column.GetCellContent(Data) as TextBlock).Text;
                txbxCountOnShelf.Text = CountOnShelf;
            }
            else 
            {
                txbxStorekeeperID.Text = "0";
                txbxName.Text = "";
                txbxBarcode.Text = "";
                txbxPrice.Text = "0.00";
                txbxCountInStorage.Text = "0"; 
                txbxCountOnShelf.Text = "0";
            }

        }
    }
}
