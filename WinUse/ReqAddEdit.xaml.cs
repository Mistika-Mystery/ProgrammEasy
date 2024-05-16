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
    /// Логика взаимодействия для ReqAddEdit.xaml
    /// </summary>
    public partial class ReqAddEdit : Window
    {
        private Requests _req = new Requests();
        public ReqAddEdit(Requests rowRequests)
        {
            InitializeComponent();
            if (rowRequests != null)
            {
                _req = rowRequests;
            }
            DataContext = _req;
            CBStatus.ItemsSource = my01Entities.GetContext().Status.ToList();
            if (_req.Foto != null) ImgOshib.Source = new ImageSourceConverter().ConvertFrom(_req.Foto) as ImageSource;
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
