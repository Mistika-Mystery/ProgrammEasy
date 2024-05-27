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

namespace ProgrammEasy.PageUse
{
    /// <summary>
    /// Логика взаимодействия для RegLog.xaml
    /// </summary>
    public partial class RegLog : Page
    {
        
        Regex CountLog = new Regex(@"^.{3,20}$");
        MatchCollection match;
        public RegLog()
        {
            InitializeComponent();
            SaveNameTB.Text = RegFlag.NameFlage;
        }

        private void LogInfoBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RegFlag.Informbool == 1)
            {
                OpenInformTipWindow();
            }
        }
        private void OpenInformTipWindow()
        {
            var infoLog = new InfoLogin();
            infoLog.Show();
            RegFlag.Informbool = 2;
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FormatTextBox(UserlogTB);

                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(UserlogTB.Text))
                    errors.AppendLine("Введите Логин!");
                match = CountLog.Matches(UserlogTB.Text);
                if (match.Count == 0) errors.AppendLine("Логин должн содержать только буквы английского алфавита и цифры, длиной от 3-х, до 20-ти символов.");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                    var Log = myEntities.GetContext().User.FirstOrDefault(x => x.Login == UserlogTB.Text);
                    if (Log != null)
                    {
                        errors.AppendLine("Такой логин уже существует, придумай другой!");
                    }
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        return;
                    }
              
                    RegFlag.LoginFlag = UserlogTB.Text;
                NavigationService.Navigate(new RegPassword());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GoMainWinBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void QuestionLogBT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RegFlag.Logbool == 1)
            {
                OpenToolTipWindow();
            }
        }
        private void OpenToolTipWindow()
        {
            var questLog = new RecommendLog();
            questLog.Show();
            RegFlag.Logbool = 2;
        }

        private void UserlogTB_MouseLeave(object sender, MouseEventArgs e)
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

                    // Удаляем все символы кроме букв латиницы, цифр и дефиса
                    text = System.Text.RegularExpressions.Regex.Replace(text, @"[^a-zA-Z0-9-]", "");

                    // Удаляем два и более подряд идущих дефиса, оставляя только один, если за ним есть буква или цифра
                    text = System.Text.RegularExpressions.Regex.Replace(text, @"-{2,}", "-");
                    text = System.Text.RegularExpressions.Regex.Replace(text, @"-(?![a-zA-Z0-9])", "");

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
