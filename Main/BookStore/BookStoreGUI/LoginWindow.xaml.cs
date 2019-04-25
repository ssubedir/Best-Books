using System;
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
using BookStoreLIB;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

            UserData userData = new UserData();

            //get input from textboxs and saves them into string vars
            string username = this.nameTextBox.Text;
            string password = this.passwordTextBox.Password;


            if (userData.LogIn(username, password))
            {

                int isAdmin = userData.checkAdmin(username); //check if user is an admin

                //MessageBox.Show(isAdmin + "");

                if (isAdmin == 1) //if user is an admin, go to admin page
                {
                    AdminPage adminPage = new AdminPage();
                    adminPage.Show();
                    this.Close();
                }
                else // if user is not an admin, go to main window
                {
                    MainWindow mainWin = new MainWindow(userData);
                    mainWin.Owner = this;
                    mainWin.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Username or password is incorrect, try again!");
            }

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void signupButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpDialog signup = new SignUpDialog();
            signup.Owner = this;
            signup.ShowDialog();
        }

        private void adminButton_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
            this.Close();
        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow();
            mainWin.Owner = this;
            mainWin.Show();
            this.Hide();
            
        }
    }
}
