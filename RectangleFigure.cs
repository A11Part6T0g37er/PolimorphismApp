// <copyright file="RectangleFigure.cs" company="IndieWare Ink.">
// Copyright (c) IndieWare Ink.. All rights reserved.
// </copyright>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    [Serializable]
    public class RectangleFigure : AbstractFigure
    {

        [NonSerialized]
        public Rectangle rect /*= new Rectangle() { Height = 40, Width = 40 }*/;
        
        public static int Indexer = 0;

        public RectangleFigure() { }
        public RectangleFigure(Point pMax)
        {
            this.rect = new Rectangle() { Height = 40, Width = 40 };
            this.rect.Fill = this.InitBrush();
            this.rect.Fill.Opacity = 0.0;
            this.rect.Fill.Freeze();
            this.rect.Stroke = this.InitBrush();
            this.rect.StrokeThickness = 2;
            this.rect.Stroke.Freeze();
            this.PMax = pMax;
            Indexer++;



            this.X = this.rd.Next(10, (int)this.PMax.X);
            this.Y = this.rd.Next(10, (int)this.PMax.Y);
            this.shapeNode =  "Square" + " " + Indexer;
            shapeForm = ShapeForm.Rectangle;
        }

        public override void Move(Point pMax)
        {
            this.BounceTheBorder(pMax);

            Canvas.SetLeft(this.rect, this.X);
            Canvas.SetTop(this.rect, this.Y);
        }

        public static explicit operator UIElement(RectangleFigure v)
        {
            return v.rect;
        }

        public override void Draw(Canvas canvasFigures)
        {


            if (!canvasFigures.Children.Contains(this.rect))
            {
                Canvas.SetLeft(this.rect, this.X);
                Canvas.SetTop(this.rect, this.Y);
                canvasFigures.Children.Add(this.rect);
            }

        }
    }
}