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


            polygon.Stroke = this.InitBrush();
            polygon.Stroke.Freeze();

            Indexer++;
            polygon.Name = "Triangle";
            shapeNode = new TreeViewItem();
            shapeNode.Header = polygon.Name + " " + Indexer;
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
            polygon.Points = polygonPoints;
        }

        public override void Draw(Canvas canvas)
        {
            if (!canvas.Children.Contains(polygon))
            {

                X = rd.Next(10, (int)PMax.X);
                Y = rd.Next(10, (int)PMax.Y);

                Canvas.SetLeft(polygon, X);
                Canvas.SetTop(polygon, Y);
                canvas.Children.Add(polygon);
            }
        }



        public override void Move(Point pMax)
        {
            BounceTheBorder(pMax);

            Canvas.SetLeft(polygon, X);
            Canvas.SetTop(polygon, Y);
        }

        public static explicit operator UIElement(TriangleFigure v)
        {
            return v.polygon;
        }
    }
}
