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
using System.Windows.Shapes;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    /// 

    public partial class CartWindow : Window
    {
        DataSet dsBookCar;
        BookCart bookCart;
        UserData user;

        public CartWindow()
        {
            InitializeComponent();
            bookCart = new BookCart();
            dsBookCar = bookCart.loadCart(user.UserID);
            this.DataContext = dsBookCar.Tables["Cart"];

        }

        public CartWindow(UserData u)
        {
            InitializeComponent();
            this.user = u;
            bookCart = new BookCart();
            dsBookCar = bookCart.loadCart(user.UserID);
            this.DataContext = dsBookCar.Tables["Cart"];

        }

        private void catalogButton_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
            

        }

        private void exitButton_Click(object sender, RoutedEventArgs e) { this.Close(); }
        private void Window_Loaded(object sender, RoutedEventArgs e) { }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void selectCell(object sender, RoutedEventArgs e)
        {
           

        }



        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selectedRow;
                selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
                bookCart.RemoveBook(user.UserID,selectedRow.Row.ItemArray[0].ToString());
                dsBookCar = bookCart.loadCart(user.UserID);
                this.DataContext = dsBookCar.Tables["Cart"];

            }
            catch(Exception ex)
            {

            }

        }
        private void chechoutButton_Click(object sender, RoutedEventArgs e)
        {

            CheckoutWindow ds = new CheckoutWindow(user);
            ds.Owner = this;
            ds.Show();
            this.Hide();


        }

        // Minimize windows State

        private void WindowState_Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Normal  windows State
        public void WindowState_Normal(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        // Close Form
        public void WindowState_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
