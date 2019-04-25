using BookStoreLIB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;


namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for SignUpDialog.xaml
    /// </summary>
    /// //yo
    public partial class SignUpDialog : Window
    {
        public SignUpDialog()
        {
            InitializeComponent();
        }
        ValidateInput val = new ValidateInput();

        private bool validateFullName(string s1)
        {
            if (val.validateUsername(s1) != true)
            {
                MessageBox.Show("Please enter a valid name");
                return false;

            }
            else
            {
                return true;
            }
        }
        private bool validateUserName(string s1)
        {
            if (val.validateUsername(s1) != true)
            {
                MessageBox.Show("The username must start with a letter");
                return false;

            }
            else
            {
                return true;
            }
        }
        private bool validatePass(string s1)
        {
            if (val.validatePass(s1) != true)
            {
                MessageBox.Show("Make sure the password contains 6 characters with letters and numbers");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool passwordConfirmation(string s1, string s2)
        {
            if (val.passwordConfirmation(s1,s2) != true)
            {
                MessageBox.Show("Make sure the password field and the confirm password field are the same");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void okButton_Click(object sender, RoutedEventArgs e)
        {

            DALUserInfo userdb = new DALUserInfo();

            

           
            if (validateUserName(signup_nameTextBox.Text) && validateFullName(fullnameTextBox.Text) && validatePass(signup_passwordTextBox.Password) && passwordConfirmation(signup_passwordTextBox.Password, signup_confirmpasswordTextBox.Password))
            {
                int register = userdb.Register(fullnameTextBox.Text, signup_nameTextBox.Text, signup_passwordTextBox.Password, 2);

                
                if (register == -1)
                {

                    MessageBox.Show("Succesfully Created an Account, Now login");
                    // this.DialogResult = false;


                    this.DialogResult = false;
                }
                else if(register == 1)
                {
                    MessageBox.Show("Username already exists"); 
                }
                


            }



        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

