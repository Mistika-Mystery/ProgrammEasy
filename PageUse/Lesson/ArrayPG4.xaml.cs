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
    /// Логика взаимодействия для ArrayPG4.xaml
    /// </summary>
    public partial class ArrayPG4 : Page
    {
        public ArrayPG4()
        {
            InitializeComponent();
        }

        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ArrayPG3());
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ArrayPG5());
        }
    }
}
