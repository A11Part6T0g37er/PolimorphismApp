using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    internal class RectangleFigure : AbstractFigure
    {
       
       
        public Rectangle rect;
        public static int Index = 0;

        public RectangleFigure(Point pMax)
        {
            this.rect = new Rectangle() { Height = 40, Width = 40 };
            this.rect.Fill = InitBrush();
            this.PMax = pMax;
            Index++;

            rect.Name = "Square";
        }

      

       
       

        public override void Move(Point pMax)
        {
            X++;
            Y++;
            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, Y);
        }
        public static explicit operator UIElement(RectangleFigure v)
        {
            return v.rect;
        }

       

        public override void Draw(Canvas canvasFigures, TreeViewItem rectTree)
        {
            X = rd.Next(10, (int)PMax.X);
            Y = rd.Next(10, (int)PMax.Y);

            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, Y);
            canvasFigures.Children.Add(rect);

            TreeViewItem Child1Item = new TreeViewItem();
            Child1Item.Header = rect.Name +" "  + Index ;
            rectTree.Items.Add(Child1Item);

        }
    }
}