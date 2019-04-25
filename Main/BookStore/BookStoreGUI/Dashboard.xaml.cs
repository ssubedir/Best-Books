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

    public partial class Dashboard : UserControl
    {
        DataSet dsBookHistory;
        BookCatalog bookHistory;
        UserData user;
        BookCart bookCart;

        public Dashboard()
        {
            InitializeComponent();
        }

        public Dashboard(UserData u)
        {
            InitializeComponent();
            this.user = u;
            bookHistory = new BookCatalog();
            bookCart = new BookCart();

            dsBookHistory = bookHistory.load_History(user.UserID);
            this.DataContext = dsBookHistory.Tables["History"];
            cart_val.Content = "" + bookCart.cartItems(user.UserID);
            total_books.Content = "" + bookCart.totalBooks(user.UserID);
            total_money.Content = "$" + bookCart.totalPrice(user.UserID);
        }
    }
}
