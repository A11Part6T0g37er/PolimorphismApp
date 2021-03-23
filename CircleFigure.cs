// <copyright file="CircleFigure.cs" company="IndieWare Ink.">
// Copyright (c) IndieWare Ink.. All rights reserved.
// </copyright>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Serialization;
using RandomizerNetFramework;

namespace PolimorphismApp
{
    [Serializable]
    public class CircleFigure : AbstractFigure
    {
        public CircleFigure() { }
        public CircleFigure(Point pMax)
        {
            this.PMax = pMax;
            this.ellipse = new Ellipse() { Height = 40, Width = 40 };
            this.ellipse.Stroke = this.InitBrush();
            this.ellipse.StrokeThickness = 2;

            this.ellipse.Fill = this.InitBrush();
            this.ellipse.Fill.Opacity = 0.0;
            this.ellipse.Fill.Freeze();
            this.ellipse.Stroke.Freeze();
            Indexer++;

            this.shapeNode =  "Circle" + " " + Indexer;
                //this.X = this.rd.Next(0, (int)this.PMax.X);
            this.X = Randomizer.GetRandom((int)PMax.X);
                this.Y = Randomizer.GetRandom((int)PMax.Y);
            shapeForm = ShapeForm.Ellipse;
        }

        private static int Indexer = 0;

        [NonSerialized]
        [XmlIgnore]
        public Ellipse ellipse;

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
                Canvas.SetLeft(this.ellipse, this.X);
                Canvas.SetTop(this.ellipse, this.Y);
                canvas.Children.Add(this.ellipse);

            }

        }

    }
}