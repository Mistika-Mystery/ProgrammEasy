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

namespace ProgrammEasy.WinUse.Admin
{
    /// <summary>
    /// Логика взаимодействия для addEdResult.xaml
    /// </summary>
    public partial class addEdResult : Window
    {
        private Results _results = new Results();
        public addEdResult(Results results)
        {
            InitializeComponent();
            DataContext = results;
            if (results.ScoreImage != null) ImgOshib.Source = new ImageSourceConverter().ConvertFrom(results.ScoreImage.Img) as ImageSource;
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
