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
    public partial class ResultPage : Page
    {
        private TestResult _testResult;

        public ResultPage(TestResult testResult)
        {
            InitializeComponent();
            _testResult = testResult;
            DisplayResults();
        }

        private void DisplayResults()
        {
            StringBuilder results = new StringBuilder();
            results.AppendLine($"Общее время: {_testResult.TotalTimeSpent}");
            results.AppendLine($"Всего вопросов: {_testResult.TotalQuestions}");
            results.AppendLine($"Правильных ответов: {_testResult.CorrectAnswers}");
            results.AppendLine($"Неправильных ответов: {_testResult.IncorrectAnswers}");

            foreach (var questionResult in _testResult.QuestionResults)
            {
                if (!questionResult.IsCorrect)
                {
                    results.AppendLine($"--------------");
                    results.AppendLine($"Вопрос: {questionResult.Question}");
                    results.AppendLine($"Ваш ответ: {questionResult.SelectedAnswer}");
                }
            }

            ResultsSummary.Text = results.ToString();
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
