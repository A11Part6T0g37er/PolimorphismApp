// <copyright file="AbstractFigure.cs" company="IndieWareCompany">
// Copyright (c) IndieWareCompany. All rights reserved.
// </copyright>

namespace PolimorphismApp
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    internal abstract class AbstractFigure
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Dx { get; set; } = 4;

        public int Dy { get; set; } = -4;

        public Point PMax { get; set; }

        public TreeViewItem shapeNode;

        public abstract void Draw(Canvas canvas, TreeViewItem childTree);

        public abstract void Move(Point pMax);

        protected Random rd = new Random();

        protected RadialGradientBrush InitBrush()
        {
            RadialGradientBrush brush = new RadialGradientBrush();
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 0.250));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 0.100));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 8));
            return brush;
        }
        protected void BounceTheBorder(Point pMax)
        {
            if (X <= 0 || X >= pMax.X)
            {
                Dx = -Dx;
            }

            if (Y <= 0 || Y >= pMax.Y)
            {
                Dy = -Dy;
            }

            X += Dx;
            Y += Dy;

        }
        private int GetMin(double x)
        {
            if (x % 3 >= 0)
            {
                return (int)x % 3;
            }
            else
            {
                return 0;
            }
        }
    }
}