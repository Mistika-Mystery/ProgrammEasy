using ProgrammEasy.WinUse;
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
    /// Логика взаимодействия для SuccessfulReg.xaml
    /// </summary>
    public partial class SuccessfulReg : Page
    {
        public SuccessfulReg()
        {
            InitializeComponent();
        }

        private void AvtorizBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegFlag.ClearData();

                var logIn = new LogWin();
                logIn.Show();

                Window window = Window.GetWindow(this);
                if (window != null)
                {
                    window.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AvtorizInfoBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RegFlag.Avtorizbool == 1)
            {
                OpenInformTipWindow();
            }
        }
        private void OpenInformTipWindow()
        {
            var infoAvtoriz = new InfoAvtoriz();
            infoAvtoriz.Show();
            RegFlag.Avtorizbool = 2;
        }
    }
}
