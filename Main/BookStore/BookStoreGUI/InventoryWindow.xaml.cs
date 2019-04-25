using BookStoreLIB;
using System;
using System.Data;
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
using System.Diagnostics;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for InventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window
    {
        DataSet dsBookInv;
        AdminControls ac;

        public InventoryWindow()
        {
            InitializeComponent();
            ac = new AdminControls();
            dsBookInv = ac.loadInv2();
            this.DataContext = dsBookInv.Tables["Inventory"];
        }

        private void quantityButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selectedRow;
                bool result = false;
                string status = this.statusTextBox.Text;
                string quantityTB = this.quantityTextBox.Text;

                if (addRadioButton.IsChecked == true)
                {
                    selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];

                    string isbn = selectedRow.Row.ItemArray[0].ToString();
                    //string quantity = selectedRow.Row.ItemArray[4].ToString();

                    //int quantityToInt1 = Int32.Parse(quantity);
                    int quantityToInt2 = Int32.Parse(quantityTB);

                    //int newQuantity = quantityToInt1 + quantityToInt2;

                    result = ac.UpdateInventory(isbn, quantityToInt2);

                }
                else if (removeRadioButton.IsChecked == true)
                {
                    selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];

                    string isbn = selectedRow.Row.ItemArray[0].ToString();
                    string quantity = selectedRow.Row.ItemArray[4].ToString();

                    int quantityToInt1 = Int32.Parse(quantity);
                    int quantityToInt2 = Int32.Parse(quantityTB);

                    statusTextBox.Text = "TBQuantity: " + quantityToInt2 + "DBQuantity: " + quantityToInt1;

                    int newQuantity = quantityToInt1 - quantityToInt2;

                    statusTextBox.Text = "New Quantity: " + newQuantity;

                    if (newQuantity < 0) {
                        newQuantity = 0;
                    }

                    result = ac.UpdateInventory(isbn, quantityToInt2);
                }
                else
                {
                    statusTextBox.Text = "Please Select a Radio Button";
                }


                //message to status bar
                if (result)
                {
                    statusTextBox.Text = "Book Quantity has been Updated";
                }
                else
                {
                    statusTextBox.Text = "Book Quantity has not been Updated";
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Hide();
        }
    
    }
}
