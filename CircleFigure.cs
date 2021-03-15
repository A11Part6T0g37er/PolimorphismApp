// <copyright file="CircleFigure.cs" company="IndieWare Ink.">
// Copyright (c) IndieWare Ink.. All rights reserved.
// </copyright>

using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    internal class CircleFigure : AbstractFigure
    {
        public CircleFigure(Point pMax)
        {
            this.PMax = pMax;
            this.ellipse = new Ellipse() { Height = 40, Width = 40 };
            this.ellipse.Stroke = this.InitBrush();
            this.ellipse.StrokeThickness = 2;
            this.ellipse.Name = "Circle";
            this.ellipse.Fill = this.InitBrush();
            this.ellipse.Fill.Opacity = 0.0;
            this.ellipse.Fill.Freeze();
            this.ellipse.Stroke.Freeze();
            Indexer++;

            this.shapeNode = new TreeViewItem
            {
                Header = this.ellipse.Name + " " + Indexer,
            };
            ShapeForm = this.ellipse;
        }

        private static int Indexer = 0;

        // private Point pMax;
        public Ellipse ellipse { get; private set; }

        public override void Move(Point pMax)
        {

            this.BounceTheBorder(pMax);
            Canvas.SetLeft(this.ellipse, this.X);
            Canvas.SetTop(this.ellipse, this.Y);

        }

        public override void Draw(Canvas canvas)
        {

            if (!canvas.Children.Contains(this.ellipse))
            {

                this.X = this.rd.Next(0, (int)this.PMax.X);
                this.Y = this.rd.Next(0, (int)this.PMax.Y);
                Canvas.SetLeft(this.ellipse, this.X);
                Canvas.SetTop(this.ellipse, this.Y);
                canvas.Children.Add(this.ellipse);

            }

        }

    }
}