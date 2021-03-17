// <copyright file="TriangleFigure.cs" company="IndieWare Ink.">
// Copyright (c) IndieWare Ink.. All rights reserved.
// </copyright>

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    internal class TriangleFigure : AbstractFigure
    {

        private static int Indexer = 0;
        public Polygon polygon { get; private set; }
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

            this.shapeNode = new TreeViewItem();
            this.shapeNode.Header = "Triangle" + " " + Indexer;
            this.X = this.rd.Next(10, (int)this.PMax.X);
            this.Y = this.rd.Next(10, (int)this.PMax.Y);
            ShapeForm = this.polygon;
        }

        private void InitializeShape()
        {
            this.polygon = new Polygon();
            System.Windows.Point Point1 = new System.Windows.Point(20, 10);
            Point Point2 = new System.Windows.Point(40, 40);
            Point Point3 = new System.Windows.Point(0, 40);
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
