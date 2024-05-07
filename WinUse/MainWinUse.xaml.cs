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
    /// Логика взаимодействия для MainWinUse.xaml
    /// </summary>
    public partial class MainWinUse : Window
    {
        public MainWinUse()
        {
            InitializeComponent();
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (popup.IsOpen)
                popup.IsOpen = false;
            else
                popup.IsOpen = true;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            var glavwin = new MainWindow();
            glavwin.Show();
            this.Close();
        }
    }
}
