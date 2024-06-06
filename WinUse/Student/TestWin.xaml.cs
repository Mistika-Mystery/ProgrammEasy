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
using System.Windows.Threading;

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
            _testResult = new TestResult { TotalQuestions = 20 }; 
            myFrameTest.Navigate(new PageUse.Lesson.TestEasyPG(_testResult, 1)); 
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите вернуться?\nНесохраненные данные могут быть утеряны", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {

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
    }
    
}
