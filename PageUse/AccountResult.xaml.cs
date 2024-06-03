using ProgrammEasy.WinUse.Admin;
using ProgrammEasy.WinUse.Student;
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
    /// Логика взаимодействия для AccountResult.xaml
    /// </summary>
    public partial class AccountResult : Page
    {
        private User user;
        public AccountResult()
        {

            InitializeComponent();
            //user = myEntities.GetContext().User.FirstOrDefault(x => x.Id == RegFlag.IdUser);
            //DataContext = user;

            //myResultDG.ItemsSource= myEntities.GetContext().Results.Where( x => x.User.Id == RegFlag.IdUser).ToList();
        }

        private void EditBt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var rowRes = (sender as DataGridRow).DataContext as Results;
            UserResultWin adRes = new UserResultWin(rowRes);
            adRes.Show();
        }

        private void MessBt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
