using ProgrammEasy.WinUse.Admin;
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

namespace ProgrammEasy.PageUse
{
    /// <summary>
    /// Логика взаимодействия для AccountResult.xaml
    /// </summary>
    public partial class AccountResult : Page
    {
        private User user;
        public AccountResult()
        {

            InitializeComponent();
            user = myEntities.GetContext().User.FirstOrDefault(x => x.Id == RegFlag.IdUser);
            DataContext = user;

            var AllLesson = myEntities.GetContext().Lessons.Where(l => l.Results.Any(ul => ul.IdUser == user.Id)).ToList();
            AllLesson.Insert(0, new Lessons
            {
                Name = "Все уроки"
            });
            CBLessonResult.ItemsSource = AllLesson;

            var AllScore = myEntities.GetContext().ScoreImage.Where(l => l.Results.Any(ul => ul.IdUser == user.Id)).ToList();
            AllScore.Insert(0, new ScoreImage
            {
                Name = "Все оценки"
            });
            CBScoreResult.ItemsSource = AllScore;


        }

        private void EditBt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var rowRes = (sender as DataGridRow).DataContext as Results;
            UserResultWin adRes = new UserResultWin(rowRes);
            adRes.Show();
        }

        private void MessBt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpRes();
        }
        private void UpRes()
        {
            SeactWaterResult.Visibility = Visibility.Collapsed;
            SeactWaterResult.Text = "";
            TBoxSearchResult.Visibility = Visibility.Visible;
            sortBoxResult.SelectedIndex = 0;
            CBLessonResult.SelectedIndex = 0;
            CBScoreResult.SelectedIndex = 0;

            myResultDG.ItemsSource = myEntities.GetContext().Results.Where(x => x.User.Id == RegFlag.IdUser).ToList();
        }
        private void TBoxSearchResult_GotFocus(object sender, RoutedEventArgs e)
        {
            TBoxSearchResult.Visibility = Visibility.Collapsed;
            SeactWaterResult.Visibility = Visibility.Visible;
            SeactWaterResult.Focus();
        }

        private void SeactWaterResult_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SeactWaterResult.Text))
            {
                SeactWaterResult.Visibility = Visibility.Collapsed;
                TBoxSearchResult.Visibility = Visibility.Visible;
            }
        }

        private void SeactWaterResult_TextChanged(object sender, TextChangedEventArgs e)
        {
            Seach_FilterResult(SeactWaterResult.Text);
        }

        private void sortBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_FilterResult(SeactWaterResult.Text);
        }


        private void CBLessonResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_FilterResult(SeactWaterResult.Text);
        }

        private void BtnReloadResult_Click(object sender, RoutedEventArgs e)
        {
            UpRes();
        }

        private void CBScoreResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_FilterResult(SeactWaterResult.Text);
        }
        private void Seach_FilterResult(string search = "")
        {
            var ResultSerch = myEntities.GetContext().Results.Where(x => x.User.Id == RegFlag.IdUser).ToList();

            if (!string.IsNullOrEmpty(search) || !string.IsNullOrWhiteSpace(search))
            {
                ResultSerch = ResultSerch.Where(s => s.Lessons.Description.ToLower().Contains(search.ToLower())
                || (s.Lessons.Name ?? "").ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBoxResult.SelectedIndex)
            {
                case 1:
                    ResultSerch = ResultSerch.OrderByDescending(s => s.Date).ToList();
                    break;
                case 2:
                    ResultSerch = ResultSerch.OrderBy(s => s.Date).ToList();
                    break;
                default:
                    break;
            }

            if (CBLessonResult == null)
            {
                return;
            }
            if (CBLessonResult.SelectedIndex > 0)
            {
                ResultSerch = ResultSerch.Where(p => p.Lessons == CBLessonResult.SelectedValue).ToList();
            }
            if (CBScoreResult.SelectedIndex > 0)
            {
                ResultSerch = ResultSerch.Where(p => p.ScoreImage == CBScoreResult.SelectedValue).ToList();
            }
            if (ResultSerch.Count > 0)
            {
                labCoun.Content = ("Найдено: ") + ResultSerch.Count;
            }
            else if (ResultSerch.Count == 0)
            {
                labCoun.Content = ("Не найдено");
            }

            myResultDG.ItemsSource = ResultSerch;
        }
    }
}

