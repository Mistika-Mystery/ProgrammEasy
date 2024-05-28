using ProgrammEasy.PageUse.PageAdmin;
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
            CBStatus.ItemsSource = myEntities.GetContext().Status.ToList();
            if (_req.Foto != null) ImgOshib.Source = new ImageSourceConverter().ConvertFrom(_req.Foto) as ImageSource;
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись изменена!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите вернуться?\nНесохраненные данные могут быть утеряны",
             "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
