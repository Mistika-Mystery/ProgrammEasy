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
    /// Логика взаимодействия для ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {
        public ResultPage(TestResult testResult)
        {
            InitializeComponent();
            DisplayResults(testResult);
        }
        private void DisplayResults(TestResult testResult)
        {
            ResultText.Text = $"Время, потраченное на тест: {testResult.TimeSpent}\n" +
                              $"Всего вопросов: {testResult.TotalQuestions}\n" +
                              $"Правильных ответов: {testResult.CorrectAnswers}\n\n" +
                              "Неправильные ответы:\n";

            foreach (var result in testResult.QuestionResults)
            {
                if (!result.IsCorrect)
                {
                    ResultText.Text += $"Вопрос: {result.Question}\nВаш ответ: {result.SelectedAnswer}\n\n";
                }
            }
            TestResult._questionNumber = 0;
        }
    }
}
