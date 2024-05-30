using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProgrammEasy.PageUse.Lesson
{
    public partial class BlockDiagramPG4_1 : Page
    {
        private bool isDragging = false;
        private Point clickPosition;
        private TranslateTransform currentTransform;

        public BlockDiagramPG4_1()
        {
            InitializeComponent();
        }

        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            // Навигация назад
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            // Навигация дальше
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            Image image = sender as Image;
            clickPosition = e.GetPosition(this);
            currentTransform = (TranslateTransform)image.RenderTransform;
            image.CaptureMouse();
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Image image = sender as Image;
            image.ReleaseMouseCapture();
            CheckIfAllTargetsCorrect();
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

        private void CheckIfAllTargetsCorrect()
        {
            // Логика проверки
            bool isCorrect = IsInTarget(img01, Target1) &&
                             IsInTarget(img02, Target2) &&
                             IsInTarget(img03, Target3) &&
                             IsInTarget(img05, Target4) &&
                             IsInTarget(img05, Target5);

            NextBT.IsEnabled = isCorrect;
        }

        private bool IsInTarget(Image img, Border target)
        {
            var imgBounds = img.TransformToAncestor(this).TransformBounds(new Rect(0, 0, img.ActualWidth, img.ActualHeight));
            var targetBounds = target.TransformToAncestor(this).TransformBounds(new Rect(0, 0, target.ActualWidth, target.ActualHeight));

            // Увеличение границ целевой области
            const double margin = 20; // например, на 20 пикселей с каждой стороны
            var extendedTargetBounds = new Rect(targetBounds.X - margin, targetBounds.Y - margin,
                                                targetBounds.Width + 2 * margin, targetBounds.Height + 2 * margin);

            return imgBounds.IntersectsWith(extendedTargetBounds);
        }
    }
}
