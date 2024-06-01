using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammEasy
{
    public class TestResult
    {
        public TimeSpan TimeSpent { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public List<QuestionResult> QuestionResults { get; set; } = new List<QuestionResult>();
        
        public static int _questionNumber;
        public static int _countQuestion;

    }

    public class QuestionResult
    {
        public string Question { get; set; }
        public string SelectedAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
