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
    /// Логика взаимодействия для RegResult.xaml
    /// </summary>
    public partial class RegResult : Page
    {
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
                NavigationService.Navigate(new SuccessfulReg());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
