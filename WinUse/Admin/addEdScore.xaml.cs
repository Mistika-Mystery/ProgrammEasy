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
    /// Логика взаимодействия для addEdScore.xaml
    /// </summary>
    public partial class addEdScore : Window
    {
        private ScoreImage _score = new ScoreImage();
        Regex nazvania = new Regex(@"^[А-ЯЁ][а-яё\s]{2,50}$");
        MatchCollection match;
        private byte[] data = null;
        public addEdScore(ScoreImage score)
        {
            InitializeComponent();
            if (score != null)
            {
                _score = score;
            }
            DataContext = _score;
            lable.Content = _score.Name;
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
                if (string.IsNullOrWhiteSpace(_score.Name)) errors.AppendLine("Укажите название оценки!");
                match = nazvania.Matches(NameTB.Text);
                if (match.Count == 0) errors.AppendLine("Название должно содержать только русские буквы! Первая буква должна быть заглавной! Длина от 2 до 50 символов");
                var NameSc = myEntities.GetContext().ScoreImage.FirstOrDefault(x => x.Name == _score.Name);

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                if (_score.Id == 0)
                {
                    if (NameSc != null)
                    {
                        errors.AppendLine("Такая оценка уже существует!");
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
                            _score.Img = data;
                        }
                        myEntities.GetContext().ScoreImage.Add(_score);
                        myEntities.GetContext().SaveChanges();
                        MessageBox.Show("Оценка успешно добавлена!");
                        this.Close();
                    }
                }
                else
                {
                    if (NameSc != null && _score.Id != NameSc.Id)
                    {
                        errors.AppendLine("Такая оценка уже существует!");
                    }
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        _score.Name = lable.Content.ToString();
                        NameTB.Text = _score.Name;
                        return;
                    }
                    else
                    {
                        if (data != null)
                        {
                            _score.Img = data;
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
