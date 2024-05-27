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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProgrammEasy.WinUse.Admin
{
    /// <summary>
    /// Логика взаимодействия для addEdGroup.xaml
    /// </summary>
    public partial class addEdGroup : Window
    {
        private GroupUser _group = new GroupUser();
        Regex nazvania = new Regex(@"^[a-zA-Zа-яА-Я0-9][\s\S]{1,49}$");
        MatchCollection match;
        private byte[] data = null;
        public addEdGroup(GroupUser group)
        {
            InitializeComponent();
            if (group != null)
            {
                _group = group;

            }
            DataContext = _group;
            lable.Content = _group.Name;
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
                if (string.IsNullOrWhiteSpace(_group.Name)) errors.AppendLine("Укажите название группы!");
                match = nazvania.Matches(NameTB.Text);
                if (match.Count == 0) errors.AppendLine("Название должно начинаться с цифр или букв! Длина от 2 до 50 символов.");
                var NameGr = myEntities.GetContext().GroupUser.FirstOrDefault(x => x.Name == _group.Name);

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                if (_group.Id == 0)
                {
                    if (NameGr != null)
                    {
                        errors.AppendLine("Такая группа уже существует!");
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
                            _group.Img = data;
                        }
                        myEntities.GetContext().GroupUser.Add(_group);
                        myEntities.GetContext().SaveChanges();
                        MessageBox.Show("Группа успешно добавлена!");
                        this.Close();
                    }
                }
                else
                {
                    if (NameGr != null && _group.Id != NameGr.Id)
                    {
                        errors.AppendLine("Такая группа уже существует!");
                    }
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        _group.Name = lable.Content.ToString();
                        NameTB.Text = _group.Name;
                        return;
                    }
                    else
                    {
                        if (data != null)
                        {
                            _group.Img = data;
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
    }
}
