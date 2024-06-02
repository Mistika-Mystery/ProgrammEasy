using ProgrammEasy.WinUse.Student;
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
    /// Логика взаимодействия для TestEasyPG4.xaml
    /// </summary>
    public partial class TestEasyPG4 : Page
    {
        private static DispatcherTimer _timer;
        private static DateTime _startTime;
        private static TestResult _testResult = new TestResult();
        public TestEasyPG4()
        {
            InitializeComponent();
        }
        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите вернуться?\nНесохраненные данные могут будут утеряны",
 "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                _timer.Stop();
                //TestResult._questionNumber = 0;
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
        private void Timer_Tick(object sender, EventArgs e)
        {
            var elapsedTime = DateTime.Now - _startTime;
            // Optionally update UI with elapsedTime
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            //SaveAnswer();
            //if (TestResult._questionNumber == _testResult.TotalQuestions)
            //{
            //    _timer.Stop();
            //    NavigationService.Navigate(new ResultPage(_testResult));
            //}
            //else
            //{
            //    TestResult._questionNumber++;
            //    NavigationService.Navigate(new TestEasyPG5()); 
            //}
        }
        private void SaveAnswer()
        {
            var selectedAnswer = GetSelectedAnswer();
            var correctAnswer = "c) Последовательность символов"; // Правильный ответ для текущего вопроса
            _testResult.QuestionResults.Add(new QuestionResult
            {
                Question = QuestionBody.Text, // Текущий вопрос
                SelectedAnswer = selectedAnswer, // Ответ пользователя
                IsCorrect = selectedAnswer == correctAnswer // Проверка правильности ответа
            });
        }

        private string GetSelectedAnswer()
        {
            if (AnswerRadioButton1.IsChecked == true) return "a) Число с плавающей точкой";
            if (AnswerRadioButton2.IsChecked == true) return "b) Логическое значение";
            if (AnswerRadioButton3.IsChecked == true) return "c) Последовательность символов";
            if (AnswerRadioButton4.IsChecked == true) return "d) Одномерный массив";
            return string.Empty;
        }
    }
}

