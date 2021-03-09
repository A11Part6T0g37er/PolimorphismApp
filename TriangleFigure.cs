using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    internal class TriangleFigure : AbstractFigure
    {
        Point pMax;
        public Polygon polygon { get; private set;}
    public TriangleFigure() { }
        public TriangleFigure(Point pmax)
    {
            this.pMax = pmax;
        this.polygon = new Polygon();
        System.Windows.Point Point1 = new System.Windows.Point(30, 10);
        Point Point2 = new System.Windows.Point(40, 40);
        Point Point3 = new System.Windows.Point(20, 40);
        PointCollection polygonPoints = new PointCollection();
        polygonPoints.Add(Point1);
        polygonPoints.Add(Point2);
        polygonPoints.Add(Point3);
        polygon.Points = polygonPoints;
        polygon.Fill = InitBrush();
    }
    public override void Draw()
    {
            Canvas.SetLeft(polygon,rd.Next(10, (int)pMax.X));
            Canvas.SetTop(polygon, rd.Next(10, (int)pMax.Y));


            
        }

    public override void Move(Point pMax)
    {
        throw new NotImplementedException();
    }

        public static explicit operator UIElement(TriangleFigure v)
        {
            return v.polygon ;
        }
    }
}
