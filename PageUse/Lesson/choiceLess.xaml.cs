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

namespace ProgrammEasy.PageUse.Lesson
{
    /// <summary>
    /// Логика взаимодействия для choiceLess.xaml
    /// </summary>
    public partial class choiceLess : Page
    {
        public choiceLess()
        {
            InitializeComponent();
        }

        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var logIn = new UserGlav();
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

        private void DataTypeSP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BlockSP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void arrySP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
