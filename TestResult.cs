using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammEasy
{
    public class TestResult
    {
        public int TotalQuestions  { get; set; }
    public List<QuestionResult> QuestionResults { get; set; } = new List<QuestionResult>();

        public int CorrectAnswers => QuestionResults.Count(q => q.IsCorrect);
        public int IncorrectAnswers => QuestionResults.Count(q => !q.IsCorrect);
        public TimeSpan TotalTimeSpent { get; set; }
    }

    public class QuestionResult
    {
        public string Question { get; set; }
        public string SelectedAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
