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
    /// Логика взаимодействия для RecomendName.xaml
    /// </summary>
    public partial class RecomendName : Window
    {
        public RecomendName()
        {
            InitializeComponent();
        }

        private void CloseBT_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            RegFlag.Namebool = 1;
        }
    }
}
