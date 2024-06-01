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
    /// Логика взаимодействия для ArrayPG18.xaml
    /// </summary>
    public partial class ArrayPG18 : Page
    {
        private bool isDragging = false;
        private Point clickPosition;
        private TranslateTransform currentTransform;
        public ArrayPG18()
        {
            InitializeComponent();
            SaveInitialPositions();
        }

        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ArrayPG17());
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ArrayPG19());
        }
        private void SaveInitialPositions()
        {
            Text01.Tag = new Point(Grid.GetColumn(Text01), Grid.GetRow(Text01));
            Text02.Tag = new Point(Grid.GetColumn(Text02), Grid.GetRow(Text02));
            Text03.Tag = new Point(Grid.GetColumn(Text03), Grid.GetRow(Text03));
            Text04.Tag = new Point(Grid.GetColumn(Text04), Grid.GetRow(Text04));

        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            Image image = sender as Image; /////
            
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
            bool isCorrect = IsInTarget(Text01, Target3) &&
                             IsInTarget(Text02, Target4) &&
                             IsInTarget(Text03, Target1) &&
                             IsInTarget(Text04, Target2);
            return isCorrect;
        }
        private bool Ch01()
        {
            bool isCorrect = IsInTarget(Text01, Target3);
            return isCorrect;
        }
        private bool Ch02()
        {
            bool isCorrect = IsInTarget(Text02, Target4);
            return isCorrect;
        }
        private bool Ch03()
        {
            bool isCorrect = IsInTarget(Text03, Target1);
            return isCorrect;
        }
        private bool Ch04()
        {
            bool isCorrect = IsInTarget(Text04, Target2);
            return isCorrect;
        }
        private bool IsInTarget(Image text, Border target)
        {
            var imgBounds = text.TransformToAncestor(this).TransformBounds(new Rect(0, 0, text.ActualWidth, text.ActualHeight));
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
                    chOk1.Visibility = Visibility.Visible;
                    chOk2.Visibility = Visibility.Visible;
                    chOk3.Visibility = Visibility.Visible;
                    chOk4.Visibility = Visibility.Visible;

                }
                else
                {
                    if (Ch01())
                    {
                        chOk1.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        chNo1.Visibility = Visibility.Visible;
                    }
                    if (Ch02())
                    {
                        chOk2.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        chNo2.Visibility = Visibility.Visible;
                    }
                    if (Ch03())
                    {
                        chOk3.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        chNo3.Visibility = Visibility.Visible;
                    }
                    if (Ch04())
                    {
                        chOk4.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        chNo4.Visibility = Visibility.Visible;
                    }

                    CheckBT.Content = "Попробовать еще раз";
                }
            }
            else if (CheckBT.Content.ToString() == "Попробовать еще раз")
            {
                ResetImages();
                chNo1.Visibility = Visibility.Hidden;
                chNo2.Visibility = Visibility.Hidden;
                chNo3.Visibility = Visibility.Hidden;
                chOk1.Visibility = Visibility.Hidden;
                chOk2.Visibility = Visibility.Hidden;
                chOk3.Visibility = Visibility.Hidden;
                chNo4.Visibility = Visibility.Hidden;
                chOk4.Visibility = Visibility.Hidden;
            }

        }
        private void ResetImages()
        {
            // Возвращаем изображения на исходные позиции в разметке грида
            SetImagePosition(Text01, (Point)Text01.Tag);
            SetImagePosition(Text02, (Point)Text02.Tag);
            SetImagePosition(Text03, (Point)Text03.Tag);
            SetImagePosition(Text04, (Point)Text04.Tag);


            // Возвращаем контент кнопки на "Проверить"
            CheckBT.Content = "Проверить";
        }


        private void SetImagePosition(Image text, Point position)
        {
            // Устанавливаем позиции в сетке
            Grid.SetColumn(text, (int)position.X);
            Grid.SetRow(text, (int)position.Y);

            // Сбрасываем трансформацию изображения
            if (text.RenderTransform is TranslateTransform transform)
            {
                transform.X = 0;
                transform.Y = 0;
            }
        }
    }
}
