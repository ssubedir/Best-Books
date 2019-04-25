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
    /// Interaction logic for Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        DataSet dsBookItem;
        BookCatalog bookItem;
        string isbn;
        UserData user;
        BookCart bookCart;

        public Item()
        {
            InitializeComponent();

        }
        public Item(UserData u, string i)
        {
            InitializeComponent();
            this.user = u;
            this.isbn = i;
            bookItem = new BookCatalog();
            bookCart = new BookCart();

            dsBookItem = bookItem.load_Item(isbn);
            this.DataContext = dsBookItem.Tables["Item"];
            cart_val.Text = "" + bookCart.cartItems(user.UserID);


        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            //UserControl usc = new Store();
            //item_item.Children.Clear();
            //item_item.Children.Add(usc);
        }

        private void PackIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UserControl usc = new Store(user);
            item_Shop.Children.Clear();
            item_Shop.Children.Add(usc);

        }

        private void addToCart_Click(object sender, RoutedEventArgs e)
        {



            try
            {
                string isbn = isbnTextBox.Text;
                short quantity = short.Parse(item_q.Text);
                bookCart.AddBook(user.UserID, isbn, quantity);
                cart_val.Text = "" + bookCart.cartItems(user.UserID);
                MessageBox.Show("You Have added " + book_name.Text +" to your Cart.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid input");
            }





        }


    }
}
