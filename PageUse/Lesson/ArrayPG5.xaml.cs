using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace ProgrammEasy.PageUse.Lesson
{
    /// <summary>
    /// Логика взаимодействия для ArrayPG5.xaml
    /// </summary>
    public partial class ArrayPG5 : Page
    {
        Regex regex = new Regex("^[0-9]$");
        MatchCollection match;
        public ArrayPG5()
        {            
            InitializeComponent();
        }

        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBT_Click(object sender, RoutedEventArgs e)
        {
            
            StringBuilder errors = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Check1.Text))
            {
                match = regex.Matches(Check1.Text);
                if (match.Count == 0) errors.AppendLine("Можно вводить только цыфры.");
            }
            if (!string.IsNullOrWhiteSpace(Check2.Text))
            {
                match = regex.Matches(Check2.Text);
                if (match.Count == 0) errors.AppendLine("Можно вводить только цыфры.");
            }
            if (!string.IsNullOrWhiteSpace(Check3.Text))
            {
                match = regex.Matches(Check3.Text);
                if (match.Count == 0) errors.AppendLine("Можно вводить только цыфры.");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (CheckBT.Content.ToString() == "Проверить")
            {
                bool allCorrect = true;

                if (Check1.Text == "0")
                {
                    chOk1.Visibility = Visibility.Visible;
                }
                else
                {
                    chNo1.Visibility = Visibility.Visible;
                    allCorrect = false;
                }

                if (Check2.Text == "2")
                {
                    chOk2.Visibility = Visibility.Visible;
                }
                else
                {
                    chNo2.Visibility = Visibility.Visible;
                    allCorrect = false;
                }

                if (Check3.Text == "4")
                {
                    chOk3.Visibility = Visibility.Visible;
                }
                else
                {
                    chNo3.Visibility = Visibility.Visible;
                    allCorrect = false;
                }

                if (allCorrect)
                {
                    NextBT.IsEnabled = true;
                    CheckBT.Content = "Молодец!";
                }
                else
                {
                    CheckBT.Content = "Попробовать еще раз";
                }
            }
            else
            {
                chOk1.Visibility = Visibility.Hidden;
                chNo1.Visibility = Visibility.Hidden;

                chOk2.Visibility = Visibility.Hidden;
                chNo2.Visibility = Visibility.Hidden;

                chOk3.Visibility = Visibility.Hidden;
                chNo3.Visibility = Visibility.Hidden;

                Check1.Text = "";
                Check2.Text = "";
                Check3.Text = "";

                CheckBT.Content = "Проверить";
            }
        }
    }
}
