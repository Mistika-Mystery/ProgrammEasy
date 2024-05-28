using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для addEdUser.xaml
    /// </summary>
    public partial class addEdUser : Window
    {
        private User _user = new User();

        public addEdUser(User user)
        {
            InitializeComponent();
            if (user != null)
            {
                _user = user;
            }
            DataContext = _user;
            CBGroup.ItemsSource = myEntities.GetContext().GroupUser.ToList();
            CBRole.ItemsSource = myEntities.GetContext().RoleUser.ToList();
            CBImg.ItemsSource = myEntities.GetContext().ImgFoto.ToList();


            if (_user.ImgFoto.ImgLev != null) ImgUs.Source = new ImageSourceConverter().ConvertFrom(_user.ImgFoto.ImgLev) as ImageSource;///pjijijj
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
