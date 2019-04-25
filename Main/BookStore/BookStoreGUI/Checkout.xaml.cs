using BookStoreLIB;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for Checkout.xaml
    /// </summary>
    public partial class Checkout : UserControl
    {
        BookCart cart;
        UserData user;
        UserControl usc;
        int total;
        List<List<string>> books;
        public Checkout()
        {
            InitializeComponent();
        }
        public Checkout(UserData u, List<List<string>> b)
        {
            InitializeComponent();
            this.user = u;
            this.books = b;
            cart = new BookCart();
            UserControl usc = new Store(user);
            int a = cart.colSum(user.UserID);
            total = a;
            label_Copy.Content = "Total:$ " + a;

        }
        private bool checkExpire()
        {
            String expireText1 = textBox_Copy1.Text;
            String expireText2 = textBox_Copy2.Text;

            if (expireText1.Length != 4 || expireText2.Length != 4)
            {
                MessageBox.Show("Each Expire box must have 4 digits");
                return false;

            }
            else
            {
                if (expireText1.Any(x => char.IsLetter(x)) || expireText2.Any(x => char.IsLetter(x)))
                {
                    MessageBox.Show("Expire box must be numbers only");
                    return false;

                }
                else
                {
                    return true;
                }
            }
        }
        private bool checkName()
        {
            String nameText = textBox.Text;

            if (nameText.Length > 50)
            {
                MessageBox.Show("Exceeded name size");
                return false;
            }
            else
            {
                if (nameText.Any(x => char.IsLetter(x)))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Name number cannot have numbers");
                    return false;
                }
            }
        }
        private bool checkCard()
        {
            String cardText = textBox_Copy.Text;

            if (cardText.Length != 16)
            {
                MessageBox.Show("Card number must contain 16 digits");
                return false;
            }
            else
            {
                if (cardText.Any(x => char.IsLetter(x)))
                {
                    MessageBox.Show("Card number cannot have letters");
                    return false;
                }
                else
                {

                    return true;
                }
            }
        }
        private bool checkCVC()
        {

            String cvcText1 = textBox_Copy3.Text;
            String cvcText2 = textBox_Copy4.Text;
            String cvcText3 = textBox_Copy5.Text;


            if (cvcText1.Length > 1 || cvcText2.Length > 1 || cvcText3.Length > 1)
            {
                MessageBox.Show("Each CVC box must be less than 1");
                return false;

            }
            else
            {
                if (cvcText1.Any(x => !char.IsLetter(x)) && cvcText2.Any(x => !char.IsLetter(x)) && cvcText3.Any(x => !char.IsLetter(x)))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("CVC box must be numbers only");
                    return false;
                }
            }


        }
        private void button1_Click_1(object sender, RoutedEventArgs e)
        {


            bool cvc = checkCVC();
            if (cvc)
            {
                if (checkCard())
                {
                    if (checkName())
                    {
                        if (checkExpire())
                        {

                            cart.removeAll(user.UserID);
                            int a = cart.colSum(user.UserID);
                            for (int i = 0; i < books.Count; i++)
                            {

                                cart.cBook(user.UserID, books[i][0], Int32.Parse(books[i][1]));
                            }

                            UserControl usc = new Store(user);
                            user_Check.Children.Clear();
                            user_Check.Children.Add(usc);


                        }

                    }
                }
            }


        }



        private void button_Click(object sender, RoutedEventArgs e)
        {

            if(textBox_Copy6.Text == "")
            {
                MessageBox.Show("No coupon entered");
                label_Copy.Content = "Total:$ " + total;
            }
            else
            {
                double a = cart.coupon(textBox_Copy6.Text);

                if(a == 0)
                {
                    MessageBox.Show("Invalid coupon entered");
                    label_Copy.Content = "Total:$ " + total;
                }
                else
                {
                    double b = total * (a / 100);
                    double c = total - b;

                    label_Copy.Content = "Total:$ " + c;

                }
               
            }
        }

        private void btn_checkout_Copy_Click(object sender, RoutedEventArgs e)
        {
            
            //user_cart.Children.Clear();
            //user_cart.Children.Add(usc);
        }
    }
}
