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
    /// Логика взаимодействия для RegLog.xaml
    /// </summary>
    public partial class RegLog : Page
    {
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
            RegFlag.LoginFlag = UserlogTB.Text;
            NavigationService.Navigate(new RegPassword());
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
            if (textBox != null)
            {
                string text = textBox.Text.Trim(); // Удаляем пробелы в начале и в конце текста
                if (!string.IsNullOrEmpty(text))
                {
                    // Если первая буква не заглавная, делаем ее заглавной
                    if (char.IsLower(text[0]))
                    {
                        text = char.ToUpper(text[0]) + text.Substring(1);
                    }

                    // Заменяем множественные пробелы одним пробелом
                    text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", " ");

                    textBox.Text = text; // Устанавливаем измененный текст обратно в TextBox
                }
            }
        }
    }
}
