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

namespace ProgrammEasy.PageUse.Lesson
{
    /// <summary>
    /// Логика взаимодействия для TypeDatePG10.xaml
    /// </summary>
    public partial class TypeDatePG10 : Page
    {
        public TypeDatePG10()
        {
            InitializeComponent();
        }

        private void CheckBT_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBT.Content.ToString() == "Проверить")
            {
                bool allCorrect = true;

                if (Check1.Text == "true" || Check1.Text == "true;")
                {                   
                    chOk1.Visibility = Visibility.Visible;
                }
                else
                {
                     chNo1.Visibility = Visibility.Visible;
                    allCorrect = false;
                }

                if (Check2.Text == "true" || Check2.Text == "true;")
                {
                    chOk2.Visibility = Visibility.Visible;
                }
                else
                {
                    chNo2.Visibility = Visibility.Visible;
                    allCorrect = false;
                }

                if (Check3.Text == "false" || Check3.Text == "false;")
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

        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TypeDatePG9());
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TypeDatePG11());
        }
    }
    
}
