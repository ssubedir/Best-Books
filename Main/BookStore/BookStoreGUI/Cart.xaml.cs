using BookStoreLIB;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : UserControl
    {
        DataSet dsBookCar;
        BookCart bookCart;
        UserData user;

        public Cart()
        {
            InitializeComponent();
            bookCart = new BookCart();
            dsBookCar = bookCart.loadCart(user.UserID);
            this.DataContext = dsBookCar.Tables["Cart"];
        }
        public Cart(UserData u)
        {
            InitializeComponent();
            this.user = u;
            bookCart = new BookCart();
            dsBookCar = bookCart.loadCart(user.UserID);
            this.DataContext = dsBookCar.Tables["Cart"];
            cart_val.Text = "" + bookCart.cartItems(user.UserID);
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selectedRow;
                selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
                bookCart.RemoveBook(user.UserID, selectedRow.Row.ItemArray[0].ToString());
                dsBookCar = bookCart.loadCart(user.UserID);
                this.DataContext = dsBookCar.Tables["Cart"];
                cart_val.Text = "" + bookCart.cartItems(user.UserID);


            }
            catch (Exception ex)
            {

            }

        }
        private void chechoutButton_Click(object sender, RoutedEventArgs e)
        {
            List<List<string>> books = new List<List<string>>();

            if (ProductsDataGrid.Items.Count >= 1)
            {
                try
                {


                    for (int x = 0; x < ProductsDataGrid.Items.Count; x++)
                    {

                        List<string> b = new List<string>();
                        DataRowView selectedRow;
                        selectedRow = (DataRowView)this.ProductsDataGrid.Items[x];
                        b.Add(selectedRow.Row.ItemArray[0].ToString());
                        b.Add(selectedRow.Row.ItemArray[2].ToString());
                        books.Add(b);

                    }


                    UserControl usc = new Checkout(user, books);
                    user_cart.Children.Clear();
                    user_cart.Children.Add(usc);

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                MessageBox.Show("You must have atleast one item in your cart to checkout.");
            }




        }


    }
}
