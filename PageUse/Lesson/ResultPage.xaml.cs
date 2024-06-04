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
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace ProgrammEasy.PageUse.Lesson
{
    public partial class ResultPage : Page
    {
        private Results results = new Results();
        private TestResult _testResult;
        private User user;

        public ResultPage(TestResult testResult)
        {
            InitializeComponent();
            _testResult = testResult;
            user = myEntities.GetContext().User.FirstOrDefault(x => x.Id == RegFlag.IdUser);
            DisplayResults();
            DescriptionGet();
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
            try
            {
                var logIn = new UserGlav();
                logIn.Show();
                _testResult.QuestionResults.Clear();

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
        private void DescriptionGet()
        {
            try
            {
                if (_testResult.CorrectAnswers >= 19)
                {
                    results.ScoreImg = 1;
                    Img05.Visibility = Visibility.Visible;
                }
                else if (_testResult.CorrectAnswers >= 15)
                {
                    results.ScoreImg = 2;
                    Img04.Visibility = Visibility.Visible;
                }
                else if (_testResult.CorrectAnswers >= 10)
                {
                    results.ScoreImg = 3;
                    Img03.Visibility = Visibility.Visible;
                }
                else if (_testResult.CorrectAnswers >= 5)
                {
                    results.ScoreImg = 4;
                    Img02.Visibility = Visibility.Visible;
                }
                else
                {
                    results.ScoreImg = 5;
                    Img01.Visibility = Visibility.Visible;

                }
                if (RegFlag.IdRol != 4)
                {
                    if (CheckRepeatLevel() && _testResult.CorrectAnswers >= 19 && user.FotoImg == 4)
                    {
                        user.FotoImg = 5;
                        myEntities.GetContext().SaveChanges();
                        MessageBox.Show("Поздравляем! \n Ваш достигли 2 уровня!");

                    }
                    results.IdUser = RegFlag.IdUser;
                    results.IdLesson = RegFlag.LessonId;
                    results.Date = DateTime.Now;
                    results.Description = ResultsSummary.Text;

                    myEntities.GetContext().Results.Add(results);
                    myEntities.GetContext().SaveChanges();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool CheckRepeatLevel()
        {
            try
            {
                var existingResult = myEntities.GetContext().Results.FirstOrDefault(r => r.IdUser == RegFlag.IdUser && r.IdLesson == RegFlag.LessonId);

                if ((existingResult != null && existingResult.ScoreImg != 1) || existingResult == null)
                {
                    return true; 
                }

                return false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
