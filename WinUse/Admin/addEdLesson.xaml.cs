using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
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
    /// Логика взаимодействия для addEdLesson.xaml
    /// </summary>
    public partial class addEdLesson : Window
    {
        private Lessons _lesson = new Lessons();
        Regex nazvania = new Regex(@"^[А-ЯЁ0-9][а-яё0-9\-\s]{2,49}$");
        MatchCollection match;
        private byte[] data = null;
        public addEdLesson(Lessons lesson)
        {
            InitializeComponent();
            if (lesson != null)
            {
                _lesson = lesson;
            }
            DataContext = _lesson;
            lable.Content = _lesson.Name;
            lable.Visibility = Visibility.Hidden;
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
                if (string.IsNullOrWhiteSpace(_lesson.Name)) errors.AppendLine("Укажите название урока!");
                match = nazvania.Matches(NameTB.Text);
                if (match.Count == 0) errors.AppendLine("Название должно содержать только русские буквы! Первая буква должна быть заглавной! Длина от 2 до 50 символов");
                if (string.IsNullOrWhiteSpace(_lesson.Description))
                    errors.AppendLine("Опишите урок.");
                var NameRl = myEntities.GetContext().Lessons.FirstOrDefault(x => x.Name == _lesson.Name);

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                if (_lesson.Id == 0)
                {
                    if (NameRl != null)
                    {
                        errors.AppendLine("Такой урок уже существует!");
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
                            _lesson.Img = data;
                        }
                        myEntities.GetContext().Lessons.Add(_lesson);
                        myEntities.GetContext().SaveChanges();
                        MessageBox.Show("Урок успешно добавлен!");
                        this.Close();
                    }
                }
                else
                {
                    if (NameRl != null && _lesson.Id != NameRl.Id)
                    {
                        errors.AppendLine("Такой урок уже существует!");
                    }
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        _lesson.Name = lable.Content.ToString();
                        NameTB.Text = _lesson.Name;
                        return;
                    }
                    else
                    {
                        if (data != null)
                        {
                            _lesson.Img = data;
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
