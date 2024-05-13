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

namespace ProgrammEasy.WinUse.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminMain.xaml
    /// </summary>
    public partial class AdminMain : Window
    {
        public AdminMain()
        {
            InitializeComponent();
            AdminFrame.Navigate(new PageUse.PageAdmin.GlavAdmin());
        }

        private void MenuBT_Click(object sender, RoutedEventArgs e)
        {
            if (popup.IsOpen)
                popup.IsOpen = false;
            else
                popup.IsOpen = true;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.GoBack();
            popup.IsOpen = false;
        }

        private void AdminFrame_ContentRendered(object sender, EventArgs e)
        {
            if (AdminFrame.CanGoBack)
            {
                MenuBT.Visibility = Visibility.Visible;
            }
            else
            {
                MenuBT.Visibility = Visibility.Collapsed;
            }
        }
    }
}
