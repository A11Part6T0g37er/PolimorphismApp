// <copyright file="TriangleFigure.cs" company="IndieWare Ink.">
// Copyright (c) IndieWare Ink.. All rights reserved.
// </copyright>

using RandomizerNetFramework;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace PolimorphismApp
{
    [Serializable]
    public class TriangleFigure : AbstractFigure
    {

        private static int Indexer = 0;
        [NonSerialized]
        [XmlIgnore]
        public Polygon polygon;
        public TriangleFigure() { }

        public TriangleFigure(Point pmax)
        {
            this.PMax = pmax;
            this.InitializeShape();
            this.polygon.Stroke = this.InitBrush();
            this.polygon.Stroke.Freeze();
            this.polygon.Fill = this.InitBrush();
            this.polygon.Fill.Opacity = 0.0;
            this.polygon.Fill.Freeze();

            Indexer++;

            this.shapeNode = "Triangle" + " " + Indexer;
            this.X = Randomizer.GetRandom((int)PMax.X);
            this.Y = Randomizer.GetRandom((int)PMax.Y);
            shapeForm = ShapeForm.Triangle;
        }

        private void InitializeShape()
        {
            this.polygon = new Polygon();
            Point Point1 = new Point(20, 10);
            Point Point2 = new Point(40, 40);
            Point Point3 = new Point(0, 40);
            PointCollection polygonPoints = new PointCollection
            {
                Point1,
                Point2,
                Point3,
            };
            this.polygon.Points = polygonPoints;
        }

        public override void Draw(Canvas canvas)
        {
            if (!canvas.Children.Contains(this.polygon))
            {


                Canvas.SetLeft(this.polygon, this.X);
                Canvas.SetTop(this.polygon, this.Y);
                canvas.Children.Add(this.polygon);
            }
        }

        public override void Move(Point pMax)
        {
            this.BounceTheBorder(pMax);

            Canvas.SetLeft(this.polygon, this.X);
            Canvas.SetTop(this.polygon, this.Y);
        }

        public static explicit operator UIElement(TriangleFigure v)
        {
            return v.polygon;
        }
    }
}
