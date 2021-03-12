// <copyright file="RectangleFigure.cs" company="IndieWare Ink.">
// Copyright (c) IndieWare Ink.. All rights reserved.
// </copyright>

using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    internal class RectangleFigure : AbstractFigure
    {

        public Rectangle rect { get; private set; }
        public static int Indexer = 0;

        public RectangleFigure(Point pMax)
        {
            this.rect = new Rectangle() { Height = 40, Width = 40 };

            this.rect.Stroke = this.InitBrush();
            this.rect.StrokeThickness = 2;
            this.rect.Stroke.Freeze();
            this.PMax = pMax;
            Indexer++;

            this.rect.Name = "Square";

            this.shapeNode = new TreeViewItem();
            this.shapeNode.Header = this.rect.Name + " " + Indexer;
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
                this.X = this.rd.Next(10, (int)this.PMax.X);
                this.Y = this.rd.Next(10, (int)this.PMax.Y);
                Canvas.SetLeft(this.rect, this.X);
                Canvas.SetTop(this.rect, this.Y);
                canvasFigures.Children.Add(this.rect);
            }

        }
    }
}