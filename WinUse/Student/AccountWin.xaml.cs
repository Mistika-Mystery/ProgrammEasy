using ProgrammEasy.WinUse.Admin;
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

namespace ProgrammEasy.WinUse.Student
{
    /// <summary>
    /// Логика взаимодействия для AccountWin.xaml
    /// </summary>
    public partial class AccountWin : Window
    {

        public AccountWin()
        {
            InitializeComponent();
            myFrameAccount.Navigate(new PageUse.AccountResult());
            LogName.Text = RegFlag.UserLogin;
            if (RegFlag.IdRol == 1 || RegFlag.IdRol == 2)
            {
                PanelAdminBT.IsEnabled = true;
            }

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var exWin = new UserGlav();
            exWin.Show();
            this.Close();
        }

        private void PanelAdminBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AdminGlavWin Panel = new AdminGlavWin();
            Panel.Show();
            this.Close();
        }
    }
}
