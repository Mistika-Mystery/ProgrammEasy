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
    /// Логика взаимодействия для BlockDiagramPG13.xaml
    /// </summary>
    public partial class BlockDiagramPG13 : Page
    {
        private Results results = new Results();
        public BlockDiagramPG13()
        {
            InitializeComponent();
            DataContext = results;
        }

        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BlockDiagramPG12());
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RegFlag.IdRol != 4)
                {
                    results.IdUser = RegFlag.IdUser;
                    results.IdLesson = RegFlag.LessonId;
                    results.Date = DateTime.Now;
                    results.ScoreImg = 1005;

                    myEntities.GetContext().Results.Add(results);
                    myEntities.GetContext().SaveChanges();
                }

                NavigationService.Navigate(new choiceLess());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
    
}
