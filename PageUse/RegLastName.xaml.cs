using ProgrammEasy.WinUse;
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

namespace ProgrammEasy.PageUse
{
    /// <summary>
    /// Логика взаимодействия для RegLastName.xaml
    /// </summary>
    public partial class RegLastName : Page
    {
        public RegLastName()
        {
            InitializeComponent();
        }

        private void GoBackBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            RegFlag.LastNameFlage = UserLastNameTB.Text;
            NavigationService.Navigate(new RegLog());
        }

        private void RecomLastNameBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var recLastName = new RecomendLastName();
            recLastName.Show();
        }
    }
}
