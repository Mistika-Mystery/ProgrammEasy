﻿using ProgrammEasy.WinUse.Student;
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
using System.Windows.Threading;

namespace ProgrammEasy.PageUse.Lesson
{
    /// <summary>
    /// Логика взаимодействия для TestEasyPG10.xaml
    /// </summary>
    public partial class TestEasyPG10 : Page
    {
        private DispatcherTimer _timer;
        private DateTime _startTime;
        private TestResult _testResult;
        private int _questionNumber;
        public TestEasyPG10(TestResult testResult, int questionNumber)
        {
            InitializeComponent();
            _testResult = testResult;
            _questionNumber = questionNumber;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _startTime = DateTime.Now;
            _timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            var elapsedTime = DateTime.Now - _startTime;
        }
        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(GetSelectedAnswer()))
            {
                MessageBox.Show("Пожалуйста, выберите вариант ответа.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SaveAnswer();
            _testResult.TotalTimeSpent += DateTime.Now - _startTime;
            if (_questionNumber == _testResult.TotalQuestions)
            {
                _timer.Stop();
                NavigationService.Navigate(new ResultPage(_testResult));
            }
            else
            {
                NavigationService.Navigate(new TestEasyPG11(_testResult, _questionNumber + 1)); // Переход к следующей странице
            }
        }

        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите вернуться?\nНесохраненные данные могут быть утеряны", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                _timer.Stop();
                _testResult.QuestionResults.Clear();
                try
                {
                    var logIn = new UserGlav();
                    logIn.Show();

                    Window window = Window.GetWindow(this);
                    if (window != null)
                    {
                        window.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void SaveAnswer()
        {
            var selectedAnswer = GetSelectedAnswer();
            var correctAnswer = "c) true"; // Правильный ответ для текущего вопроса
            _testResult.QuestionResults.Add(new QuestionResult
            {
                Question = QuestionBody.Text, // Текущий вопрос
                SelectedAnswer = selectedAnswer, // Ответ пользователя
                IsCorrect = selectedAnswer == correctAnswer // Проверка правильности ответа
            });
        }

        private string GetSelectedAnswer()
        {
            if (AnswerRadioButton1.IsChecked == true) return "a) 42";
            if (AnswerRadioButton2.IsChecked == true) return "b) 3.14";
            if (AnswerRadioButton3.IsChecked == true) return "c) true";
            if (AnswerRadioButton4.IsChecked == true) return "d) “строка”";
            return string.Empty;
        }
    }
}
