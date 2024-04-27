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

namespace ProgrammEasy.WinUse
{
    /// <summary>
    /// Логика взаимодействия для LogWin.xaml
    /// </summary>
    public partial class LogWin : Window
    {
        public LogWin()
        {
            InitializeComponent();
        }

        private void LogInBT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            var backWin = new MainWindow();
            backWin.Show();
            this.Close();
        }

        private void EyaOfIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PassTB.Text = PassPB.Password;
            PassTB.Visibility = Visibility.Visible;
            PassPB.Visibility = Visibility.Collapsed;
            EyaOfIMG.Visibility = Visibility.Collapsed;
            EyaOnIMG.Visibility = Visibility.Visible;
        }

        private void EyaOnIMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PassPB.Password = PassTB.Text; 
            PassTB.Visibility = Visibility.Collapsed; 
            PassPB.Visibility = Visibility.Visible;
            EyaOnIMG.Visibility = Visibility.Collapsed;
            EyaOfIMG.Visibility = Visibility.Visible;

        }
    }
}
