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
using System.Text.RegularExpressions;
using BookStoreLIB;
using System.Data.SqlClient;

namespace BookStoreGUI
{
    /// Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {

        DataSet dsBookCat;
        UserData userData;
        BookCatalog bookCatalog;
        BookCart bookCart;
        UserData user;
        public MainWindow()
        {
            InitializeComponent();
            //bookCatalog = new BookCatalog();
            //dsBookCat = bookCatalog.GetBookInfo();
            //this.DataContext = dsBookCat.Tables["Category"];
            //userData = new UserData();
            //bookCart = new BookCart();
        }

        public MainWindow(UserData u)
        {
            InitializeComponent();
            this.user = u;
            UserControl usc = new Store(user);

            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(usc);
            //bookCatalog = new BookCatalog();
            //dsBookCat = bookCatalog.GetBookInfo();
            //this.DataContext = dsBookCat.Tables["Category"];
            //userData = new UserData();
            //bookCart = new BookCart();
            //welcomeMessage();
        }
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

            //var dlg = new LoginDialog();
            //dlg.Owner = this;
            //dlg.ShowDialog();
            //// Process data entered by user if dialog box is accepted
            //if (dlg.DialogResult == true)
            //{
            //    if (userData.LogIn(dlg.nameTextBox.Text, dlg.passwordTextBox.Password) == true)
            //        this.statusTextBlock.Text = "You are logged in as User #" + userData.UserID;
            //    else
            //        MessageBox.Show("You could not be verified. Please try again.");
            //}
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //OrderItemDialog orderItemDialog = new OrderItemDialog();
                //DataRowView selectedRow;
                //selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
                //orderItemDialog.isbnTextBox.Text = selectedRow.Row.ItemArray[0].ToString();
                //orderItemDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();
                //orderItemDialog.priceTextBox.Text = selectedRow.Row.ItemArray[4].ToString();
                //orderItemDialog.Owner = this;
                //orderItemDialog.ShowDialog();

                //if (orderItemDialog.DialogResult == true)
                //{
                //    string isbn = orderItemDialog.isbnTextBox.Text;
                //    double unitPrice = double.Parse(orderItemDialog.priceTextBox.Text);
                //    short quantity = short.Parse(orderItemDialog.quantityTextBox.Text);
                //    bookCart.AddBook(user.UserID, isbn, quantity);
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select A Book.");
            }
            finally
            {
                // do
            }
        }

        private void chechoutButton_Click(object sender, RoutedEventArgs e)
        {

            //CartWindow cw = new CartWindow(user);
            //cw.Owner = this;
            //cw.Show();
            //this.Hide();

        }

        private void searchTxb_TextChanged(object sender, TextChangedEventArgs e)
        {

            //if(searchTxb.Text == "")
            //{
            //    dsBookCat = bookCatalog.GetBookInfo();
            //    this.DataContext = dsBookCat.Tables["Category"];

            //}
            //else
            //{
            //    dsBookCat = bookCatalog.SearchBook(searchTxb.Text);
            //    this.DataContext = dsBookCat.Tables["Category"];

            //}


        }
        private void welcomeMessage()
        {
            //statusTextBlock.Text = "Welcome back " + user.LoginName +" " +user.UserID+ "!";
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            int index = ListViewMenu.SelectedIndex;
            UserControl usc = null;
            GridPrincipal.Children.Clear();
            switch (index)
            {
                case 0:
                    usc = new Store(user);
                    GridPrincipal.Children.Add(usc);
                    break;
                case 1:
                    usc = new Cart(user);
                    GridPrincipal.Children.Add(usc);
                    break;
                case 2:
                    usc = new Dashboard(user);
                    GridPrincipal.Children.Add(usc);
                    break;
                case 3:
                    this.Owner.Show();
                    this.Close();
                    break;
                case 4:
                    this.Owner.Close();
                    this.Close();
                    break;

                default:
                    break;
            }
        }

    }
}
