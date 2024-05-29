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
    /// Логика взаимодействия для TypeDatePG6.xaml
    /// </summary>
    public partial class TypeDatePG6 : Page
    {
        public TypeDatePG6()
        {
            InitializeComponent();
        }

        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TypeDatePG5());
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TypeDatePG7());

        }

        private void CheckBT_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBT.Content.ToString() == "Проверить")
            {
                bool allCorrect = true;

                if (Check1.IsChecked == true)
                {
                    chOk1.Visibility = Visibility.Visible;
                }
                else
                {
                   
                    allCorrect = false;
                }

                if (Check2.IsChecked == true)
                {
                    chOk2.Visibility = Visibility.Visible;                    
                    
                }
                else
                {
                    
                    allCorrect = false;
                }

                if (Check3.IsChecked == true)
                {                    
                    chNo3.Visibility = Visibility.Visible;
                    allCorrect = false;
                }
                else
                {
                    

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
                chNo1.Visibility = Visibility.Hidden;
                chNo2.Visibility = Visibility.Hidden;
                chNo3.Visibility = Visibility.Hidden;
                chOk1.Visibility = Visibility.Hidden;
                chOk2.Visibility = Visibility.Hidden;
                chOk3.Visibility = Visibility.Hidden;

                CheckBT.Content = "Проверить";
                Check1.IsChecked = false;
                Check2.IsChecked = false;
                Check3.IsChecked = false;
            }
        }
    }
    
}
