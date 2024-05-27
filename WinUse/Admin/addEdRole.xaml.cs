using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для addEdRole.xaml
    /// </summary>
    public partial class addEdRole : Window
    {
        private RoleUser _role = new RoleUser();
        Regex nazvania = new Regex(@"^[А-ЯЁ][а-яё\s]{2,50}$");
        MatchCollection match;
        private byte[] data = null;
        public addEdRole(RoleUser role)
        {
            InitializeComponent();
            if (role != null)
            {
                _role = role;
            }
            DataContext = _role;
            lable.Content = _role.Name;
            lable.Visibility = Visibility.Hidden;
            if (_role.Img != null) ImageSerice.Source = new ImageSourceConverter().ConvertFrom(_role.Img) as ImageSource;
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
                if (string.IsNullOrWhiteSpace(_role.Name)) errors.AppendLine("Укажите название роли!");
                match = nazvania.Matches(NameTB.Text);
                if (match.Count == 0) errors.AppendLine("Название должно содержать только русские буквы! Первая буква должна быть заглавной! Длина от 2 до 50 символов");
                var NameRl = myEntities.GetContext().Status.FirstOrDefault(x => x.Name == _role.Name);

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                if (_role.Id == 0)
                {
                    if (NameRl != null)
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
                        _role.Img = data;
                        myEntities.GetContext().RoleUser.Add(_role);
                        myEntities.GetContext().SaveChanges();
                        MessageBox.Show("Роль успешно добавлена!");
                        this.Close();
                    }
                }
                else
                {
                    if (NameRl != null && _role.Id != NameRl.Id)
                    {
                        errors.AppendLine("Такая роль уже существует!");
                    }
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        _role.Name = lable.Content.ToString();
                        NameTB.Text = _role.Name;
                        return;
                    }
                    else
                    {
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
