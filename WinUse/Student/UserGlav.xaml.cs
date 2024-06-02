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
    /// Логика взаимодействия для UserGlav.xaml
    /// </summary>
    public partial class UserGlav : Window
    {
        public UserGlav()
        {
            InitializeComponent();
            NameUser.Text = RegFlag.UserLogin;
            if (RegFlag.IdRol ==4)
            {
                ImgOk.Visibility = Visibility.Collapsed;
                ImgNo.Visibility = Visibility.Visible;
                ResultSP.Visibility = Visibility.Collapsed;
                NoResultSP.Visibility = Visibility.Visible;
                InfoUsSP.IsEnabled = false;
            }
        }

        private void ResultSP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var Acc = new AccountWin();
            Acc.Show();
            this.Close();
        }

        private void LessonSP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var less = new LessonWin();
            less.Show();
            this.Close();
        }

        private void TestSP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var test = new TestWin();
            test.Show();
            this.Close();
        }

        private void InfoUsSP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ExitUsSP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var glavWin = new MainWindow();
            glavWin.Show();
            this.Close();
        }
    }
}
