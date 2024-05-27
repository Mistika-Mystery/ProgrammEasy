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
    /// Логика взаимодействия для addEdStatus.xaml
    /// </summary>
    public partial class addEdStatus : Window
    {
        private Status _status = new Status();
        Regex nazvania = new Regex(@"^[А-ЯЁ][а-яё\s]{2,50}$");
        MatchCollection match;
   
        public addEdStatus(Status status)
        {
            InitializeComponent();
            if (status != null)
            {
                _status = status;
            }
            DataContext = _status;
            lable.Content = _status.Name;
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
                if (string.IsNullOrWhiteSpace(_status.Name)) errors.AppendLine("Укажите название статуса!");
                match = nazvania.Matches(NameTB.Text);
                if (match.Count == 0) errors.AppendLine("Название должно содержать только русские буквы! Первая буква должна быть заглавной! Длина от 2 до 50 символов");
                var NameSt = myEntities.GetContext().Status.FirstOrDefault(x => x.Name == _status.Name);

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                if (_status.Id == 0)
                {
                    if (NameSt != null)
                    {
                        errors.AppendLine("Такой статус уже существует!");
                    }
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        return;
                    }
                    else
                    {
                        myEntities.GetContext().Status.Add(_status);
                        myEntities.GetContext().SaveChanges();
                        MessageBox.Show("Статус успешно добавлен!");
                        this.Close();
                    }
                }
                else
                {
                    if (NameSt != null && _status.Id != NameSt.Id)
                    {
                        errors.AppendLine("Такой статус уже существует!");
                    }
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        _status.Name = lable.Content.ToString();
                        NameTB.Text = _status.Name;
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

    }
}
