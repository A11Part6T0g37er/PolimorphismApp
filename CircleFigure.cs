﻿using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    internal class CircleFigure : AbstractFigure
    {
        private Canvas internalCanvas;
        private static int Indexer = 0;

        //private Point pMax;
        public Ellipse ellipse { get; private set; }
        public CircleFigure(Point pMax)
        {
            this.pMax = pMax;
            ellipse = new Ellipse() { Height = 40, Width = 40 };
            ellipse.Fill = InitBrush();
            Indexer++;

            ellipse.Name = "Circle";
        }



        public override void Move(Point pMax)
        {


            if (X <= 0 || X >= pMax.X)
                Dx = -Dx;
            if (Y <= 0 || X >= pMax.Y)
                Dy = -Dy;

            X += Dx;
            Y += Dy;


        }

        public override void Draw(Canvas canvas, TreeViewItem childTree)
        {
            internalCanvas = canvas;
            X = rd.Next(0, (int)pMax.X);
            Y = rd.Next(0, (int)pMax.Y);
            Canvas.SetLeft(ellipse, X);
            Canvas.SetTop(ellipse, Y);
            canvas.Children.Add(ellipse);

            TreeViewItem ChildItem = new TreeViewItem();
            ChildItem.Header = ellipse.Name + " " + Indexer;
            childTree.Items.Add(ChildItem);

        }
        public void Move(Canvas canvas2)
        {
           
                Move2(canvas2);
                
            

           

        }

        private void Move2(Canvas canvas2)
        {
            if (X <= 0 || X >= pMax.X)
                Dx = -Dx;
            if (Y <= 0 || X >= pMax.Y)
                Dy = -Dy;

            X += Dx;
            Y -= Dy;

          var  left = Canvas.GetLeft(ellipse);
           var right = Canvas.GetLeft(ellipse);
           var top = Canvas.GetTop(ellipse);
          var  bottom = Canvas.GetTop(ellipse);
            //canvas2.Children.Remove(ellipse);
            Canvas.SetLeft(ellipse, X );
            Canvas.SetTop(ellipse, Y);
            //canvas2.Children.Add(ellipse);
        }
    }
}