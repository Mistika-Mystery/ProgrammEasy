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
using System.Windows.Shapes;

namespace ProgrammEasy.WinUse.Student
{
    /// <summary>
    /// Логика взаимодействия для TestWin.xaml
    /// </summary>
    public partial class TestWin : Window
    {
        private TestResult _testResult;
        public TestWin()
        {
            InitializeComponent();
            _testResult = new TestResult { TotalQuestions = 20 }; // Установите общее количество вопросов
            myFrameTest.Navigate(new PageUse.Lesson.TestEasyPG(_testResult, 1)); // Начало с первого вопроса (0)
        }
    }
    
}
