using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProgrammEasy.WinUse.Admin
{
    /// <summary>
    /// Логика взаимодействия для addEdImg.xaml
    /// </summary>
    public partial class addEdImg : Window
    {
        private ImgFoto _imgFoto = new ImgFoto();
        Regex nazvania = new Regex(@"^[А-ЯЁ][а-яё\s]{2,50}$");
        MatchCollection match;
        private byte[] data = null;
        public addEdImg(ImgFoto imgFoto)
        {
            InitializeComponent();
            if (imgFoto != null)
            {
                _imgFoto = imgFoto;
            }
            DataContext = _imgFoto;
            lable.Content = _imgFoto.Name;
            lable.Visibility = Visibility.Hidden;
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите вернуться?\nНесохраненные данные могут быть утеряны",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectImageBTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
