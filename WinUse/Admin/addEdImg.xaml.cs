using Microsoft.Win32;
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
            try
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(_imgFoto.Name)) errors.AppendLine("Укажите название роли!");
                match = nazvania.Matches(NameTB.Text);
                if (match.Count == 0) errors.AppendLine("Название должно содержать только русские буквы! Первая буква должна быть заглавной! Длина от 2 до 50 символов");
                var NameImg = myEntities.GetContext().ImgFoto.FirstOrDefault(x => x.Name == _imgFoto.Name);

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                if (_imgFoto.Id == 0)
                {
                    if (NameImg != null)
                    {
                        errors.AppendLine("Такая роль уже существует!");
                    }
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        return;
                    }
                    else
                    {
                        if (data != null)
                        {
                            _imgFoto.ImgLev = data;
                        }
                        myEntities.GetContext().ImgFoto.Add(_imgFoto);
                        myEntities.GetContext().SaveChanges();
                        MessageBox.Show("Роль успешно добавлена!");
                        this.Close();
                    }
                }
                else
                {
                    if (NameImg != null && _imgFoto.Id != NameImg.Id)
                    {
                        errors.AppendLine("Такая роль уже существует!");
                    }
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        _imgFoto.Name = lable.Content.ToString();
                        NameTB.Text = _imgFoto.Name;
                        return;
                    }
                    else
                    {
                        if (data != null)
                        {
                            _imgFoto.ImgLev = data;
                        }
                        myEntities.GetContext().SaveChanges();
                        MessageBox.Show("Изменения сохранены!");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
