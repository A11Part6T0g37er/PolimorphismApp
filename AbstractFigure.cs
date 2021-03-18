// <copyright file="AbstractFigure.cs" company="IndieWareCompany">
// Copyright (c) IndieWareCompany. All rights reserved.
// </copyright>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace PolimorphismApp
{
    [Serializable]
    [XmlInclude(typeof(RectangleFigure))]
    [XmlInclude(typeof(CircleFigure))]
    [XmlInclude(typeof(TriangleFigure))]
    public class AbstractFigure
    {
        /// <summary>
        /// Simple wrapper for objects derived from Shape;
        /// </summary>
        public int X { get; set; }

        public int Y { get; set; }

        public int Dx { get; set; } = 4;

        public int Dy { get; set; } = -4;

        public Point PMax { get; set; }

        public string shapeNode;

        public ShapeForm shapeForm;/*{ get; set; }*/
        public virtual ShapeForm GetShape()
        {
            return shapeForm;
        }
        public virtual void Draw(Canvas canvas) { }

        public virtual void Move(Point pMax) { }

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

        internal void Serialize(ElementPropertyBag epb)
        {
            throw new NotImplementedException();
        }
    }

    public enum ShapeForm
    {
        Rectangle, Triangle, Ellipse
    }
}