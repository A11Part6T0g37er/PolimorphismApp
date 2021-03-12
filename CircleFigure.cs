﻿namespace PolimorphismApp
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Shapes;

    internal class CircleFigure : AbstractFigure
    {
        public CircleFigure(Point pMax)
        {
            this.PMax = pMax;
            this.ellipse = new Ellipse() { Height = 40, Width = 40 };
            this.ellipse.Fill = InitBrush();
            Indexer++;

            this.ellipse.Name = "Circle";

            shapeNode = new TreeViewItem();
                shapeNode.Header = this.ellipse.Name + " " + Indexer;
        }

        private Canvas internalCanvas;
        private static int Indexer = 0;

        // private Point pMax;
        public Ellipse ellipse { get; private set; }




        public override void Move(Point pMax)
        {


            BounceTheBorder(pMax);
            Canvas.SetLeft(ellipse, X);
            Canvas.SetTop(ellipse, Y);

        }

        public override void Draw(Canvas canvas )
        {

            if (!canvas.Children.Contains(ellipse))
            {

                X = rd.Next(0, (int)PMax.X);
                Y = rd.Next(0, (int)PMax.Y);
                Canvas.SetLeft(this.ellipse, X);
                Canvas.SetTop(this.ellipse, Y);
                canvas.Children.Add(this.ellipse);

            }

        }

        
    }
}