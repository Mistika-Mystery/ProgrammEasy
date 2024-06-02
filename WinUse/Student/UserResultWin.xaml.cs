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
    /// Логика взаимодействия для UserResultWin.xaml
    /// </summary>
    public partial class UserResultWin : Window
    {
        private const double ExpanderHeightChange = 200;

        public UserResultWin(Results results)
        {
            InitializeComponent();
            DataContext = results;
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OpisanTB.Text))
            {
                this.Height += ExpanderHeightChange;
                DescriptionRow.Height = new GridLength(ExpanderHeightChange);
            }
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OpisanTB.Text))
            {
                this.Height -= ExpanderHeightChange;
                DescriptionRow.Height = new GridLength(1, GridUnitType.Star);
            }
        }
    }
}
