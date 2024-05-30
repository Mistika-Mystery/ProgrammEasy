using ProgrammEasy.PageUse.Lesson;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProgrammEasy.WinUse
{
    /// <summary>
    /// Логика взаимодействия для MainWinUse.xaml
    /// </summary>
    public partial class MainWinUse : Window
    {
        private bool isDragging = false;
        private Point clickPosition;
        private TextBlock draggedTextBlock;

        public MainWinUse()
        {
            InitializeComponent();
        }

        private void BakcBT_Click(object sender, RoutedEventArgs e)
        {
            // Navigation logic for Back button
        }

        private void NextBT_Click(object sender, RoutedEventArgs e)
        {
            // Navigation logic for Next button
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            draggedTextBlock = sender as TextBlock;
            clickPosition = e.GetPosition(MyCanvas);
            draggedTextBlock.CaptureMouse();
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            if (draggedTextBlock != null)
            {
                draggedTextBlock.ReleaseMouseCapture();
                CheckIfAllTargetsCorrect();
            }
        }

        private void TextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && draggedTextBlock != null)
            {
                Point currentPosition = e.GetPosition(MyCanvas);
                double offsetX = currentPosition.X - clickPosition.X;
                double offsetY = currentPosition.Y - clickPosition.Y;

                double newLeft = Canvas.GetLeft(draggedTextBlock) + offsetX;
                double newTop = Canvas.GetTop(draggedTextBlock) + offsetY;

                Canvas.SetLeft(draggedTextBlock, newLeft);
                Canvas.SetTop(draggedTextBlock, newTop);

                clickPosition = currentPosition;
            }
        }

        private void CheckIfAllTargetsCorrect()
        {
            // Logic to check if the correct TextBlocks are in the correct targets
            bool isCorrect = (IsElementWithinTarget(TextBlock1, Target1) &&
                              IsElementWithinTarget(TextBlock2, Target2) &&
                              IsElementWithinTarget(TextBlock3, Target3));

            NextBT.IsEnabled = isCorrect;
        }

        private bool IsElementWithinTarget(UIElement element, UIElement target)
        {
            double elementLeft = Canvas.GetLeft(element);
            double elementTop = Canvas.GetTop(element);

            double targetLeft = Canvas.GetLeft(target);
            double targetTop = Canvas.GetTop(target);

            double targetRight = targetLeft + target.RenderSize.Width;
            double targetBottom = targetTop + target.RenderSize.Height;

            return (elementLeft >= targetLeft && elementLeft <= targetRight &&
                    elementTop >= targetTop && elementTop <= targetBottom);
        }
    }
}
