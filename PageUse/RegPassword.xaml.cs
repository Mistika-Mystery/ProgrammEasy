using ProgrammEasy.WinUse;
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

namespace ProgrammEasy.PageUse
{
    /// <summary>
    /// Логика взаимодействия для RegPassword.xaml
    /// </summary>
    public partial class RegPassword : Page
    {
        public RegPassword()
        {
            InitializeComponent();
            SaveLogTB.Text = RegFlag.LoginFlag;
        }

        private void GoMainWinBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void QuestionPassBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RegFlag.Passbool == 1)
            {
                OpenToolTipWindow();
            }
        }
        private void OpenToolTipWindow()
        {
            var questPass = new RecomendPassword();
            questPass.Show();
            RegFlag.Passbool = 2;
        }

        private void PassInfoBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RegFlag.Passwordbool == 1)
            {
                OpenInformTipWindow();
            }
        }
        private void OpenInformTipWindow()
        {
            var infoPass = new InfoPassword();
            infoPass.Show();
            RegFlag.Passwordbool = 2;
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            FormatTextBox(UserPassTB);
            RegFlag.PasswordFlag = UserPassTB.Text;
            NavigationService.Navigate(new RegResult());
        }
        private void UserPassTB_MouseLeave(object sender, MouseEventArgs e)
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

                    // Удаляем все символы кроме букв латиницы и цифр
                    text = System.Text.RegularExpressions.Regex.Replace(text, @"[^a-zA-Z0-9]", "");

                    textBox.Text = text;
                }
            }
        }
    }
}
