using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    internal class CircleFigure : AbstractFigure
    {
        //private Point pMax;
        public Ellipse ellipse { get; private set; }
        public CircleFigure(Point pMax)
        {
            this.pMax = pMax;
            ellipse = new Ellipse() { Height = 40, Width = 40 };
            ellipse.Fill = InitBrush();
        }

       

        public override void Move(Point pMax)
        {
            //throw new System.NotImplementedException();

            if (X <= 0 || X >= pMax.X)
                Dx = -Dx;
            if (Y <= 0 || X >= pMax.Y)
                Dy = -Dy;

            X += Dx;
            Y += Dy;
            Canvas.SetLeft(ellipse, X);
            Canvas.SetTop(ellipse, Y);
        }

        public override void Draw(Canvas canvas)
        {
            X = rd.Next(10, (int)pMax.X);
            Y = rd.Next(10, (int)pMax.Y);
            Canvas.SetLeft(ellipse, X);
            Canvas.SetTop(ellipse, Y);
            canvas.Children.Add(ellipse);

            //Point pMax = new Point(canvas.ActualWidth - 20, canvas.ActualHeight - 20);
            Move(pMax);
        }
    }
}