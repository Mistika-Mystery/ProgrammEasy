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
    /// Логика взаимодействия для BlockDiagramPG11.xaml
    /// </summary>
    public partial class BlockDiagramPG11 : Page
    {
        private bool isDragging = false;
        private Point clickPosition;
        private TranslateTransform currentTransform;
        public BlockDiagramPG11()
        {
            InitializeComponent();
            SaveInitialPositions();
        }

        private void SaveInitialPositions()
        {
            img01.Tag = new Point(Grid.GetColumn(img01), Grid.GetRow(img01));
            img02.Tag = new Point(Grid.GetColumn(img02), Grid.GetRow(img02));
            img03.Tag = new Point(Grid.GetColumn(img03), Grid.GetRow(img03));
            img04.Tag = new Point(Grid.GetColumn(img04), Grid.GetRow(img04));
            img05.Tag = new Point(Grid.GetColumn(img05), Grid.GetRow(img05));
            img06.Tag = new Point(Grid.GetColumn(img06), Grid.GetRow(img06));
            img07.Tag = new Point(Grid.GetColumn(img07), Grid.GetRow(img07));
            img08.Tag = new Point(Grid.GetColumn(img08), Grid.GetRow(img08));
        }

        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BlockDiagramPG10());

        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BlockDiagramPG12());
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            Image image = sender as Image;
            clickPosition = e.GetPosition(this);

            if (image.RenderTransform == Transform.Identity)
            {
                currentTransform = new TranslateTransform();
                image.RenderTransform = currentTransform;
            }
            else
            {
                currentTransform = (TranslateTransform)image.RenderTransform;
            }



            image.CaptureMouse();
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Image image = sender as Image;
            image.ReleaseMouseCapture();

            Point currentPosition = new Point(Grid.GetColumn(image), Grid.GetRow(image));
            image.Tag = currentPosition;
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Image image = sender as Image;
                Point currentPosition = e.GetPosition(this);
                double offsetX = currentPosition.X - clickPosition.X;
                double offsetY = currentPosition.Y - clickPosition.Y;

                currentTransform.X += offsetX;
                currentTransform.Y += offsetY;

                clickPosition = currentPosition;
            }
        }

        private bool CheckIfAllTargetsCorrect()
        {
            bool isCorrect = IsInTarget(img01, Target1) &&
                             IsInTarget(img06, Target2) &&
                             IsInTarget(img04, Target3) &&
                             IsInTarget(img02, Target4) &&
                             IsInTarget(img05, Target5);
            return isCorrect;


        }

        private bool IsInTarget(Image img, Border target)
        {
            var imgBounds = img.TransformToAncestor(this).TransformBounds(new Rect(0, 0, img.ActualWidth, img.ActualHeight));
            var targetBounds = target.TransformToAncestor(this).TransformBounds(new Rect(0, 0, target.ActualWidth, target.ActualHeight));

            const double margin = 20; 
            var extendedTargetBounds = new Rect(targetBounds.X - margin, targetBounds.Y - margin,
                                                targetBounds.Width + 2 * margin, targetBounds.Height + 2 * margin);

            return imgBounds.IntersectsWith(extendedTargetBounds);
        }

        private void CheckBT_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBT.Content.ToString() == "Проверить")
            {

                if (CheckIfAllTargetsCorrect())
                {
                    CheckBT.Content = "Молодец!";
                    NextBT.IsEnabled = true;
                    ImgOk.Visibility = Visibility.Visible;
                }
                else
                {

                    CheckBT.Content = "Попробовать еще раз";
                    ImgNo.Visibility = Visibility.Visible;
                }
            }
            else if (CheckBT.Content.ToString() == "Попробовать еще раз")
            {
                ResetImages();
                ImgOk.Visibility = Visibility.Hidden;
                ImgNo.Visibility = Visibility.Hidden;
            }

        }
        private void ResetImages()
        {

            SetImagePosition(img01, (Point)img01.Tag);
            SetImagePosition(img02, (Point)img02.Tag);
            SetImagePosition(img03, (Point)img03.Tag);
            SetImagePosition(img04, (Point)img04.Tag);
            SetImagePosition(img05, (Point)img05.Tag);
            SetImagePosition(img06, (Point)img06.Tag);
            SetImagePosition(img07, (Point)img07.Tag);
            SetImagePosition(img08, (Point)img08.Tag);

            CheckBT.Content = "Проверить";
        }


        private void SetImagePosition(Image image, Point position)
        {
            Grid.SetColumn(image, (int)position.X);
            Grid.SetRow(image, (int)position.Y);

            if (image.RenderTransform is TranslateTransform transform)
            {
                transform.X = 0;
                transform.Y = 0;
            }
        }
    }
}
