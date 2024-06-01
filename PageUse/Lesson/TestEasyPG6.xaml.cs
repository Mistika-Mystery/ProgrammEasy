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
    /// Логика взаимодействия для TestEasyPG6.xaml
    /// </summary>
    public partial class TestEasyPG6 : Page
    {
        private static DispatcherTimer _timer;
        private static DateTime _startTime;
        private static TestResult _testResult = new TestResult();
        public TestEasyPG6()
        {
            InitializeComponent();
            if (_timer == null)
            {
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += Timer_Tick;
                _startTime = DateTime.Now;
                _timer.Start();
            }
        }
        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите вернуться?\nНесохраненные данные могут будут утеряны",
 "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                _timer.Stop();
                TestResult._questionNumber = 0;
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
            SaveAnswer();
            if (TestResult._questionNumber == TestResult._countQuestion)
            {
                _timer.Stop();
                NavigationService.Navigate(new ResultPage(_testResult));
            }
            else
            {
                TestResult._questionNumber++;
               // NavigationService.Navigate(new TestEasyPG6()); // Переход к следующей странице
            }
        }
        private void SaveAnswer()
        {
            var selectedAnswer = GetSelectedAnswer();
            var correctAnswer = "a) Логическое значение true или false"; // Правильный ответ для текущего вопроса
            _testResult.QuestionResults.Add(new QuestionResult
            {
                Question = QuestionBody.Text, // Текущий вопрос
                SelectedAnswer = selectedAnswer, // Ответ пользователя
                IsCorrect = selectedAnswer == correctAnswer // Проверка правильности ответа
            });
        }

        private string GetSelectedAnswer()
        {
            if (AnswerRadioButton1.IsChecked == true) return "a) Логическое значение true или false";
            if (AnswerRadioButton2.IsChecked == true) return "b) Число с плавающей точкой";
            if (AnswerRadioButton3.IsChecked == true) return "c) Целое число";
            if (AnswerRadioButton4.IsChecked == true) return "d) Последовательность символов";
            return string.Empty;
        }
    }
}
