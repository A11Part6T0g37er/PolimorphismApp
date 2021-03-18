// <copyright file="AbstractFigure.cs" company="IndieWareCompany">
// Copyright (c) IndieWareCompany. All rights reserved.
// </copyright>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    [Serializable]
    public abstract class AbstractFigure
    {
        /// <summary>
        /// Simple wrapper for objects derived from Shape;
        /// </summary>
        public int X { get; set; }

        public int Y { get; set; }

        public int Dx { get; set; } = 4;

        public int Dy { get; set; } = -4;

        public Point PMax { get; set; }

       [NonSerialized] public TreeViewItem shapeNode;

        [NonSerialized] public Shape ShapeForm;/*{ get; set; }*/
        public virtual Shape GetShape()
        {
            return ShapeForm;
        }
        public abstract void Draw(Canvas canvas);

        public abstract void Move(Point pMax);

        protected Random rd = new Random();

        protected RadialGradientBrush InitBrush()
        {
            RadialGradientBrush brush = new RadialGradientBrush();
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7695"), 0.250));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7695"), 0.100));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7695"), 8));

            return brush;
        }
        protected void BounceTheBorder(Point pMax)
        {
            if (this.X <= 0 || this.X >= pMax.X)
            {
                this.Dx = -this.Dx;
            }

            if (this.Y <= 0 || this.Y >= pMax.Y)
            {
                this.Dy = -this.Dy;
            }

            this.X += this.Dx;
            this.Y += this.Dy;

        }

    }
}