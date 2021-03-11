using System;
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
            this.pMax = pmax;
            InitializeShape();
            polygon.Fill = InitBrush();
            Indexer++;
            polygon.Name = "Triangle";
        }

        private void InitializeShape()
        {
            this.polygon = new Polygon();
            System.Windows.Point Point1 = new System.Windows.Point(20, 10);
            Point Point2 = new System.Windows.Point(40, 40);
            Point Point3 = new System.Windows.Point(0, 40);
            PointCollection polygonPoints = new PointCollection();
            polygonPoints.Add(Point1);
            polygonPoints.Add(Point2);
            polygonPoints.Add(Point3);
            polygon.Points = polygonPoints;
        }

        public override void Draw(Canvas canvas, TreeViewItem childItem)
        {

            X = rd.Next(10, (int)pMax.X);
            Y = rd.Next(10, (int)pMax.Y);

            Canvas.SetLeft(polygon, X);
            Canvas.SetTop(polygon, Y);
            EntryAddIntoTreeList(canvas, childItem);
        }

        private void EntryAddIntoTreeList(Canvas canvas, TreeViewItem childItem)
        {
            canvas.Children.Add(polygon);
            TreeViewItem polygonTree = new TreeViewItem();
            polygonTree.Header = polygon.Name + " " + Indexer;
            childItem.Items.Add(polygonTree);
        }

        public override void Move(Point pMax)
        {
            throw new NotImplementedException();
        }

        public static explicit operator UIElement(TriangleFigure v)
        {
            return v.polygon;
        }
    }
}
