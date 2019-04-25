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
    /// Interaction logic for RemoveBookWindow.xaml
    /// </summary>
    public partial class RemoveBookWindow : Window
    {
        DataSet dsBookInv;
        AdminControls ac;

        public RemoveBookWindow()
        {
            InitializeComponent();
            ac = new AdminControls();
            dsBookInv = ac.loadInv();
            this.DataContext = dsBookInv.Tables["Inventory"];


        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selectedRow;
                bool result = false;
                string status = this.statusTextBox.Text;

                if (isbnRadioButton.IsChecked == true)
                {
                    selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];

                    string isbn = selectedRow.Row.ItemArray[0].ToString();

                    statusTextBox.Text = isbn;

                    result = ac.RemoveBookbyISBN(isbn);

                }
                else if (titleRadioButton.IsChecked == true)
                {
                    selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];

                    string title = selectedRow.Row.ItemArray[1].ToString();

                    statusTextBox.Text = title;

                    result = ac.RemoveBookbyTitle(title);
                }
                else
                {
                    statusTextBox.Text = "Please Select a Radio Button";
                }


                if (result)
                {
                    statusTextBox.Text = "Book has been Deleted";
                }
                else
                {
                    statusTextBox.Text = "Book has not been Deleted";
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
