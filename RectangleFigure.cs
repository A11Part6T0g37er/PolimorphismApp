using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    internal class RectangleFigure : AbstractFigure
    {
        private RoutedEventArgs e;
        private Point pMax;
        public Rectangle rect;
       

        public RectangleFigure(Point pMax)
        {
            this.rect = new Rectangle() { Height = 40, Width = 40 };
            this.rect.Fill = InitBrush();
            this.pMax = pMax;
        }

        //public RectangleFigure(RoutedEventArgs e) => this.e = e;

       
       

        public override void Move(Point pMax)
        {
            throw new NotImplementedException();
        }
        public static explicit operator UIElement(RectangleFigure v)
        {
            return v.rect;
        }

        public override void Draw(Canvas canvasFigures)
        {
            Canvas.SetLeft(rect, rd.Next(10, (int)pMax.X));
            Canvas.SetTop(rect, rd.Next(10, (int)pMax.Y));
            _ = canvasFigures.Children.Add(rect);
        }
    }
}