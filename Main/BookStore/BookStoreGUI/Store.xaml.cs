using BookStoreLIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for Store.xaml
    /// </summary>
    public partial class Store : UserControl
    {
        UserData user;
        DataSet dsBookCat;
        BookCatalog bookCatalog;
        BookCart bookCart;

        public Store(UserData u)
        {
            InitializeComponent();
            this.user = u;
            bookCatalog = new BookCatalog();
            bookCart = new BookCart();

            dsBookCat = bookCatalog.GetBookInfo();
            this.DataContext = dsBookCat.Tables["Category"];
            cart_val.Text = "" + bookCart.cartItems(user.UserID);



        }
        private void searchTxb_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (searchTxb.Text == "")
            {
                dsBookCat = bookCatalog.GetBookInfo();
                this.DataContext = dsBookCat.Tables["Category"];

            }
            else
            {
                dsBookCat = bookCatalog.SearchBook(searchTxb.Text);
                this.DataContext = dsBookCat.Tables["Category"];

            }


        }


        void viewBookEvent(object sender, MouseButtonEventArgs e)
        {

            DataRowView selectedRow = null;
            try
            {
                selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
            }

            catch (Exception ex)
            {

            }

            if (selectedRow != null)
            {
                UserControl usc = new Item(user, selectedRow.Row.ItemArray[0].ToString());
                catalog_Shop.Children.Clear();
                catalog_Shop.Children.Add(usc);

            }

        }


    }
}
