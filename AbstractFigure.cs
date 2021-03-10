using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PolimorphismApp
{
   abstract internal class AbstractFigure
    {
        public int X { get; set; }
        public int Y { get; set; }

        private int _Dx = 4;
        private int _Dy = -4;

        public int Dx { get { return _Dx; }   set { _Dx = value; } }
        public int Dy { get { return _Dy; }   set { _Dy = value; } }
        public Point pMax { get; set; }
        protected Random rd = new Random();
      
        //public AbstractFigure(Point pMax)
        //{
        //    Random random = new Random();
        //    int minX = GetMin(pMax.X) % 3;
        //    int minY = GetMin(pMax.Y);
        //    this.x = random.Next(minX,(int)pMax.X);
        //    this.y = random.Next(minY , (int)pMax.Y);
           


        //}

        private int GetMin(double x)
        {
            if (x % 3 >= 0)
                return (int)x % 3;
            else
                return 0;
        }
        protected RadialGradientBrush InitBrush()
        {
            RadialGradientBrush brush = new RadialGradientBrush();
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 0.250));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 0.100));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 8));
            return brush;


        }

        public abstract void Draw(Canvas canvas);
        public abstract void Move(Point pMax);

        
    }
}