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

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addbookwin = new AddBookWindow();
            addbookwin.Owner = this;
            addbookwin.Show();
            this.Hide();
        }

        private void removebutton_Click(object sender, RoutedEventArgs e)
        {
            RemoveBookWindow rembookWin = new RemoveBookWindow();
            rembookWin.Owner = this;
            rembookWin.Show();
            this.Hide();
        }

        private void updatebutton_Click(object sender, RoutedEventArgs e)
        {
            InventoryWindow iw = new InventoryWindow();
            iw.Owner = this;
            iw.Show();
            this.Hide();
        }

        private void returnbutton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow logWin = new LoginWindow();
            logWin.Owner = this;
            logWin.Show();
            this.Hide();
        }

    }

}
