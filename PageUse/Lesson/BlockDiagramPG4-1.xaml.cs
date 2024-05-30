using System;
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
        private Point[] initialPositions; ///////

        public BlockDiagramPG4_1()
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
            NavigationService.Navigate(new BlockDiagramPG5());

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
            // Если текущая трансформация еще не установлена, создаем новую
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

            // Сохраняем текущие позиции изображения в Tag
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
            // Логика проверки
            bool isCorrect = IsInTarget(img01, Target1) &&
                             IsInTarget(img02, Target2) &&
                             IsInTarget(img03, Target3) &&
                             IsInTarget(img04, Target4) &&
                             IsInTarget(img05, Target5);
            return isCorrect;

            
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

        private void CheckBT_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBT.Content.ToString() == "Проверить")
            {

                if (CheckIfAllTargetsCorrect())
                {
                    // Пользователь расставил картинки верно
                    CheckBT.Content = "Молодец!";
                    NextBT.IsEnabled = true;
                }
                else
                {
                    // Пользователь расставил картинки неверно
                    //ResetImages();
                    CheckBT.Content = "Попробовать еще раз";
                }
            }
            else if (CheckBT.Content.ToString() == "Попробовать еще раз")
            {
                
                ResetImages();
            }

        }
        private void ResetImages()
        {
            Console.WriteLine("Resetting images...");
            Console.WriteLine($"Initial position of img01: {img01.Tag}");
            Console.WriteLine($"Initial position of img02: {img02.Tag}");
            Console.WriteLine($"Initial position of img03: {img03.Tag}");
            Console.WriteLine($"Initial position of img04: {img04.Tag}");
            Console.WriteLine($"Initial position of img05: {img05.Tag}");
            Console.WriteLine($"Initial position of img06: {img06.Tag}");
            Console.WriteLine($"Initial position of img07: {img07.Tag}");
            Console.WriteLine($"Initial position of img08: {img08.Tag}");

            // Возвращаем изображения на исходные позиции в разметке грида
            SetImagePosition(img01, (Point)img01.Tag);
            SetImagePosition(img02, (Point)img02.Tag);
            SetImagePosition(img03, (Point)img03.Tag);
            SetImagePosition(img04, (Point)img04.Tag);
            SetImagePosition(img05, (Point)img05.Tag);
            SetImagePosition(img06, (Point)img06.Tag);
            SetImagePosition(img07, (Point)img07.Tag);
            SetImagePosition(img08, (Point)img08.Tag);

            // Возвращаем контент кнопки на "Проверить"
            CheckBT.Content = "Проверить";
        }


        private void SetImagePosition(Image image, Point position)
        {
            // Устанавливаем позиции в сетке
            Grid.SetColumn(image, (int)position.X);
            Grid.SetRow(image, (int)position.Y);

            // Сбрасываем трансформацию изображения
            if (image.RenderTransform is TranslateTransform transform)
            {
                transform.X = 0;
                transform.Y = 0;
            }
        }
    }
}
