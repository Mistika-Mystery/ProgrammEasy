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
    /// Логика взаимодействия для RegPassword.xaml
    /// </summary>
    public partial class RegPassword : Page
    {
        public RegPassword()
        {
            InitializeComponent();
            SaveLogTB.Text = RegFlag.LoginFlag;
        }

        private void GoMainWinBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void QuestionPassBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PassInfoBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserPassTB_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            RegFlag.PasswordFlag = UserPassTB.Text;
            NavigationService.Navigate(new RegResult());
        }
    }
}
