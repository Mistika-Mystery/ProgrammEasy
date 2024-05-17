using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProgrammEasy.WinUse
{
    /// <summary>
    /// Логика взаимодействия для ReqNew.xaml
    /// </summary>
    public partial class ReqNew : Window
    {
        private Requests _req = new Requests();
        private byte[] data = null;
        public ReqNew()
        {
            InitializeComponent();
            DataContext = _req;
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите вернуться?\nНесохраненные данные могут быть утеряны",
"Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void SelectImageBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog fileOpen = new OpenFileDialog();
                fileOpen.Multiselect = false;
                fileOpen.Filter = "Image | *.png; *.jpg; *.jpeg";
                if (fileOpen.ShowDialog() == true)
                {
                    data = System.IO.File.ReadAllBytes(fileOpen.FileName);

                    ImageSerice.Source = new ImageSourceConverter().ConvertFrom(data) as ImageSource;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(_req.Description))
                    errors.AppendLine("Опишите проблему!");
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                _req.Date = DateTime.Now;
                _req.IdStatus = 1;
                _req.IdUser = RegFlag.IdUser;

                _req.Foto = data;

                my01Entities.GetContext().Requests.Add(_req);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message);}
        }
    }
}
