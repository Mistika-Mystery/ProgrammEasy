using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для RegResult.xaml
    /// </summary>
    public partial class RegResult : Page
    {
        private User _user = new User();
        public RegResult()
        {
            InitializeComponent();
            TBName.Text = RegFlag.NameFlage;
            TBLastName.Text = RegFlag.LastNameFlage;
            TBLog.Text = RegFlag.LoginFlag;
            TBPass.Text = RegFlag.PasswordFlag;
        }

        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void RegBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _user.FirstName = TBName.Text;
                _user.LastName = TBLastName.Text;
                _user.Login = TBLog.Text;
                _user.Pass1 = TBPass.Text;
                _user.IdRole = 3;
                _user.IdGroup = 1;
                _user.DateOfReg = DateTime.Now;
                _user.FotoImg = 1;

                my01Entities.GetContext().User.Add( _user );
                my01Entities.GetContext().SaveChanges();

                MessageBox.Show("Отлично!");

                NavigationService.Navigate(new SuccessfulReg());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
