using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    internal class CircleFigure : AbstractFigure
    {
        private Canvas internalCanvas;

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
           

            if (X <= 0 || X >= pMax.X)
                Dx = -Dx;
            if (Y <= 0 || X >= pMax.Y)
                Dy = -Dy;

            X += Dx;
            Y += Dy;
           

        }

        public override void Draw(Canvas canvas)
        {
            internalCanvas = canvas;
            X = rd.Next(10, (int)pMax.X);
            Y = rd.Next(10, (int)pMax.Y);
            Canvas.SetLeft(ellipse, X);
            Canvas.SetTop(ellipse, Y);
            canvas.Children.Add(ellipse);
            Thread.Sleep(1000);


            //int x = 0;
            //do
            //{
            //    Move(canvas);
            //    Thread.Sleep(500);
            //    x++;
            //}
            //while (x<5);
            //}
        }
        public void Move( Canvas canvas2)
        {
            int x = 0;
            do
            {
                Move2(canvas2);
                Thread.Sleep(500);
                x++;
            }
            while (x < 5);
        
            if (X <= 0 || X >= pMax.X)
                Dx = -Dx;
            if (Y <= 0 || X >= pMax.Y)
                Dy = -Dy;

            X += Dx;
            Y += Dy;

        }

        private void Move2(Canvas canvas2)
        {
            canvas2.Children.Remove(ellipse);
            Canvas.SetLeft(ellipse, X);
            Canvas.SetTop(ellipse, Y);
            canvas2.Children.Add(ellipse);
        }
    }
}