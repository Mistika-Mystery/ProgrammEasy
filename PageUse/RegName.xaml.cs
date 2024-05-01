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
    /// Логика взаимодействия для RegName.xaml
    /// </summary>
    public partial class RegName : Page
    {
        public RegName()
        {
            InitializeComponent();
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
