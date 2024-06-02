using ProgrammEasy.WinUse;
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

namespace ProgrammEasy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogInBT_Click(object sender, RoutedEventArgs e)
        {
            var logInWin = new LogWin();
            logInWin.Show();
            this.Close();
        }

        private void RegInBt_Click(object sender, RoutedEventArgs e)
        {
            var registrWin = new Registration();
            registrWin.Show();
            this.Close();
        }

        private void GuestInBt_Click(object sender, RoutedEventArgs e)
        {
           if (MessageBox.Show("Если Вы заходите как гость, Вы можете только изучать уроки и проходить тесты. Ваши результаты не будут сохранены. Продолжить?", "ВНИМАНИЕ", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    RegFlag.IdRol = 4;
                    RegFlag.IdUser = 1015;
                    RegFlag.UserLogin = "Гость";
                    var userWin = new UserGlav();
                    userWin.Show();
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
