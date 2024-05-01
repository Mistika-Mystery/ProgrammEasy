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
    /// Логика взаимодействия для RegName.xaml
    /// </summary>
    public partial class RegName : Page
    {
        public RegName()
        {
            InitializeComponent();
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            RegFlag.NameFlage = UserNameTB.Text;
            NavigationService.Navigate(new PageUse.RegLastName());
        }

        private void GoMainWinBT_Click(object sender, RoutedEventArgs e)
        {
            var mainWind = new MainWindow();
            mainWind.Show();
            CloseWindow();
        }

        private void CloseWindow()
        {
            Window window = Window.GetWindow(this);
            window.Close();
        }

        private void NameInfoBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var recName = new RecomendName();
            recName.Show();
        }
    }
}
