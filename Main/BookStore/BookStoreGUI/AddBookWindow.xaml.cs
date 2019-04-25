using System;
using System.Collections.Generic;
using System.Data;
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
using BookStoreLIB;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        AdminControls control;
        DataSet dsBookInv;
        public AddBookWindow()
        {
            InitializeComponent();
            control = new AdminControls();
            dsBookInv = control.loadInv();
            this.DataContext = dsBookInv.Tables["Inventory"];
        }
    
    private void AddButton_Click(object sender,RoutedEventArgs e)
        {
            int numvalue;
            string message = "";
            string isbn = this.ISBNbox.Text;
            string catid = this.CategoryIDbox.Text;
            string title = this.Titlebox.Text;
            string author = this.Authorbox.Text;
            string price = this.Pricebox.Text;
            string supplier = this.SupplierIDbox.Text;
            string publisher = this.Publisherbox.Text;
            string edition = this.Editionbox.Text;
            string year = this.Yearbox.Text;

            if(Int32.TryParse(catid, out numvalue) && Int32.TryParse(supplier, out numvalue) && Int32.TryParse(year, out numvalue) && Int32.TryParse(edition, out numvalue) && Int32.TryParse(price, out numvalue))
            {
                MessageBoxButton button = MessageBoxButton.OK;

                MessageBox.Show("Success", "Success", button);
                control.AddBook(isbn, catid, title, author, price, supplier, publisher, edition, year);
                return;
            }
            else
            {
                MessageBoxButton button = MessageBoxButton.OK;

                MessageBox.Show("One of the fields is a wrong format", "Fail", button);
                return;
            }
      
            
               /* if (!Int32.TryParse(catid, out numvalue))
                {
                    message = message + "Category ID must be an  integer number.";
                }
                if (!Int32.TryParse(supplier, out numvalue))
                {
                    message = message + " SupplierID must be an  integer number.";
                }
                if (!Int32.TryParse(year, out numvalue))
                {
                    message = message + " Year must be an  integer number.";
                }
                if (!Int32.TryParse(edition, out numvalue))
                {
                    message = message + " Edition must be an  integer number.";
                }
                if (!Int32.TryParse(price, out numvalue))
                {
                    message = message + " Price must be an  integer number.";
                }
                */
            

            
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Hide();
        }
    }
  
}
