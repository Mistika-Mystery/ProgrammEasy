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
    /// Логика взаимодействия для RecommendLog.xaml
    /// </summary>
    public partial class RecommendLog : Window
    {
        public RecommendLog()
        {
            InitializeComponent();
        }

        private void CloseBT_Click(object sender, RoutedEventArgs e)
        {
            RegFlag.Logbool = 1;
            this.Close();
        }
    }
}
