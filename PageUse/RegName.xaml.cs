//using Microsoft.CodeAnalysis.Differencing;
using ProgrammEasy.WinUse;
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
using System.Xml.Linq;

namespace ProgrammEasy.PageUse
{
    /// <summary>
    /// Логика взаимодействия для RegName.xaml
    /// </summary>
    public partial class RegName : Page
    {
        Regex CountName = new Regex(@"^.{2,20}$");
        MatchCollection match;
        public RegName()
        {
            InitializeComponent();
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FormatTextBox(UserNameTB);

                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(UserNameTB.Text))
                    errors.AppendLine("Введите Имя!");
                match = CountName.Matches(UserNameTB.Text);
                if (match.Count == 0) errors.AppendLine("Имя должно содержать только буквы русского алфавита, длиной от 2-х, до 20-ти символов.");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                RegFlag.NameFlage = UserNameTB.Text;
                NavigationService.Navigate(new PageUse.RegLastName());
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void GoMainWinBT_Click(object sender, RoutedEventArgs e)
        {
            var mainWind = new MainWindow();
            mainWind.Show();
            CloseWindow();
        }

        private void CloseWindow()
        {
            Window window = Window.GetWindow(this);
            window.Close();
        }

        private void NameInfoBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RegFlag.Namebool == 1)
            {

                OpenToolTipWindow();

            }
        }
        private void OpenToolTipWindow()
        {
            var recName = new RecomendName();
            recName.Show();
            RegFlag.Namebool = 2;
        }
        private void UserNameTB_MouseLeave(object sender, MouseEventArgs e)
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

                    text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", "");

                    text = System.Text.RegularExpressions.Regex.Replace(text, @"[^а-яА-ЯёЁ-]", "");

                    text = System.Text.RegularExpressions.Regex.Replace(text, @"-{2,}", "-");
                    text = System.Text.RegularExpressions.Regex.Replace(text, @"-(?![а-яА-ЯёЁ])", "");

                    if (text.StartsWith("-"))
                    {
                        text = text.Substring(1);
                    }

                    if (!string.IsNullOrEmpty(text))
                    {
                        text = char.ToUpper(text[0]) + text.Substring(1).ToLower();
                    }

                    text = System.Text.RegularExpressions.Regex.Replace(text, @"-(\p{L})", match => "-" + match.Groups[1].Value.ToUpper());

                    textBox.Text = text;
                }
            }
        }
    }
}
