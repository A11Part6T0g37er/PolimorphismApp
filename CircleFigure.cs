using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    internal class CircleFigure : AbstractFigure
    {
        private Point pMax;
        public Ellipse ellipse { get; private set; }
        public CircleFigure(Point pMax)
        {
            this.pMax = pMax;
            ellipse = new Ellipse() { Height = 40, Width = 40 };
            ellipse.Fill = InitBrush();
        }

       

        public override void Move(Point pMax)
        {
            throw new System.NotImplementedException();
        }

        public override void Draw(Canvas canvas)
        {
            Canvas.SetLeft(ellipse, rd.Next(10, (int)pMax.X));
            Canvas.SetTop(ellipse, rd.Next(10, (int)pMax.Y));
            canvas.Children.Add(ellipse);
        }
    }
}