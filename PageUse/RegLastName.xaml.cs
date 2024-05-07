﻿using ProgrammEasy.WinUse;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProgrammEasy.PageUse
{
    /// <summary>
    /// Логика взаимодействия для RegLastName.xaml
    /// </summary>
    public partial class RegLastName : Page
    {
        Regex CountName = new Regex(@"^.{2,20}$");
        MatchCollection match;
        public RegLastName()
        {
            InitializeComponent();
            SaveNameTB.Text = RegFlag.NameFlage;
        }
       
        private void GoBackBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FormatTextBox(UserLastNameTB);

                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(UserLastNameTB.Text))
                    errors.AppendLine("Введите Фамилию!");
                match = CountName.Matches(UserLastNameTB.Text);
                if (match.Count == 0) errors.AppendLine("Фамилия должна содержать только буквы русского алфавита, длиной от 2-х, до 20-ти символов.");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                RegFlag.LastNameFlage = UserLastNameTB.Text;
                NavigationService.Navigate(new RegLog());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RecomLastNameBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RegFlag.LastNamebool == 1)
            {               
                OpenToolTipWindow();                
            }

        }
        private void OpenToolTipWindow()
        {
            var recLastName = new RecomendLastName();
            recLastName.Show();
            RegFlag.LastNamebool = 2;
        }

        private void UserLastNameTB_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            FormatTextBox(textBox);
        }

        private void FormatTextBox(TextBox textBox)
        {
            if (textBox != null)
            {
                string text = textBox.Text.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    // Удаляем все пробелы, включая в середине текста
                    text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", "");

                    // Удаляем все символы кроме букв кириллицы и дефиса
                    text = System.Text.RegularExpressions.Regex.Replace(text, @"[^а-яА-ЯёЁ-]", "");

                    // Удаляем два и более подряд идущих дефиса, оставляя только один, если за ним есть буква
                    text = System.Text.RegularExpressions.Regex.Replace(text, @"-{2,}", "-");
                    text = System.Text.RegularExpressions.Regex.Replace(text, @"-(?![а-яА-ЯёЁ])", "");

                    // Удаляем начальный дефис, если он есть
                    if (text.StartsWith("-"))
                    {
                        text = text.Substring(1);
                    }
                    // Если первая буква не заглавная, делаем ее заглавной
                    if (!string.IsNullOrEmpty(text))
                    {
                        text = char.ToUpper(text[0]) + text.Substring(1).ToLower();
                    }
                    // Делаем буквы после дефиса заглавными
                    text = System.Text.RegularExpressions.Regex.Replace(text, @"-(\p{L})", match => "-" + match.Groups[1].Value.ToUpper());

                    textBox.Text = text;
                }
            }
        }

    }
}
