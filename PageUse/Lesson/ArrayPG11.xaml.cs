﻿using System;
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
    /// Логика взаимодействия для ArrayPG11.xaml
    /// </summary>
    public partial class ArrayPG11 : Page
    {
        public ArrayPG11()
        {
            InitializeComponent();
        }

        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ArrayPG10());
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ArrayPG12());
        }

        private void CheckBT_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBT.Content.ToString() == "Проверить")
            {
                bool allCorrect = true;
                int correctAnswers = 0;
                int notCorrectAnswers = 0;

                if (Check1.IsChecked == true)
                {
                    chNo1.Visibility = Visibility.Visible;
                    allCorrect = false;
                    notCorrectAnswers++;
                }

                if (Check2.IsChecked == true)
                {
                    chNo2.Visibility = Visibility.Visible;
                    allCorrect = false;
                    notCorrectAnswers++;
                }

                if (Check3.IsChecked == true)
                {
                    chOk3.Visibility = Visibility.Visible;
                    correctAnswers++;
                }
                else
                {
                    allCorrect = false;
                }

                if (correctAnswers == 0 && notCorrectAnswers == 0)
                {
                    HelpLB.Content = "Выберите варианты ответа";
                    CheckBT.Content = "Попробовать еще раз";
                }
                else if (correctAnswers == 1 && allCorrect && notCorrectAnswers == 0)
                {
                    NextBT.IsEnabled = true;
                    CheckBT.Content = "Молодец!";
                    HelpLB.Content = "";
                }
                else if (correctAnswers > 0 && notCorrectAnswers > 0)
                {
                    CheckBT.Content = "Попробовать еще раз";
                    HelpLB.Content = "Выберите только один вариант";
                    chNo1.Visibility = Visibility.Hidden;
                    chNo2.Visibility = Visibility.Hidden;
                    chNo3.Visibility = Visibility.Hidden;
                    chOk1.Visibility = Visibility.Hidden;
                    chOk2.Visibility = Visibility.Hidden;
                    chOk3.Visibility = Visibility.Hidden;
                }
                else
                {
                    CheckBT.Content = "Попробовать еще раз";
                    HelpLB.Content = "";
                }
            }
            else if (CheckBT.Content.ToString() == "Молодец!")
            { }
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
                HelpLB.Content = "";
            }
        }
    }
    
}
