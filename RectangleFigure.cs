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
           
            this.rect.Stroke = InitBrush();
            this.rect.StrokeThickness = 2;
            this.rect.Stroke.Freeze();
            this.PMax = pMax;
            Indexer++;

            rect.Name = "Square";

            this.shapeNode = new TreeViewItem();
            shapeNode.Header = rect.Name + " " + Indexer;
        }






        public override void Move(Point pMax)
        {
            BounceTheBorder(pMax);

            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, Y);
        }

        public static explicit operator UIElement(RectangleFigure v)
        {
            return v.rect;
        }

        public override void Draw(Canvas canvasFigures)
        {

            if (!canvasFigures.Children.Contains(rect))
            {
                X = rd.Next(10, (int)PMax.X);
                Y = rd.Next(10, (int)PMax.Y);
                Canvas.SetLeft(rect, X);
                Canvas.SetTop(rect, Y);
                canvasFigures.Children.Add(rect);
            }

        }
    }
}