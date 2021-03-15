// <copyright file="MainWindow.xaml.cs" company="IndieWare Ink.">
// Copyright (c) IndieWare Ink.. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PolimorphismApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<AbstractFigure> figuresList = new List<AbstractFigure>();
        public Point pMax;
        DispatcherTimer timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(30),
        };

        public bool StopClicked { get; private set; } = false;

        public MainWindow()
        {
            this.InitializeComponent();

            
            timer.Tick += this.Timer_Tick;
            timer.Start();

            // used by all shapes
            this.pMax = new Point(this.canvasFigures.ActualWidth - 30, this.canvasFigures.ActualHeight - 30);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.pMax = new Point(this.canvasFigures.ActualWidth - 30, this.canvasFigures.ActualHeight - 30);

            foreach (var figure in this.figuresList)
            {
                if (figure != null)
                {
                    figure.Move(this.pMax);
                    figure.Draw(this.canvasFigures);
                }
            }

        }

        // just to be sure it still completes basic testcase
        private void CanvasArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IInputElement clickedElement = Mouse.DirectlyOver;


           




            if (clickedElement is Rectangle)
            {
                var obj = clickedElement as Rectangle;
            int i =    canvasFigures.Children.IndexOf(obj);
               
              Rectangle some = (Rectangle)canvasFigures.Children[i];
                

          int X =    (int)  obj.TransformToAncestor(canvasFigures).Transform(new Point(0, 0)).X;
                int Y = (int)obj.TransformToAncestor(canvasFigures).Transform(new Point(0, 0)).Y;
                var toDel =  figuresList.Find(x => x.X == X && x.Y == Y);

                

                if (StopClicked)
                    figuresList.Remove(toDel);
                if (!StopClicked)
                {
RectangleFigure rf = new RectangleFigure(pMax) { Y = Y , X= X };
                    canvasFigures.Children.Remove(obj);
                    figuresList.Add(rf);
                }
               
            }
           
            
        }

        private void CreateRectangleShape(object sender, RoutedEventArgs e)
        {

            RectangleFigure rectangle = new RectangleFigure(this.pMax);

            this.RectTree.Items.Add(rectangle.shapeNode);

            this.figuresList.Add(rectangle);

        }

        private void CreateTriangleShape(object sender, RoutedEventArgs e)
        {

            TriangleFigure triangleFigure = new TriangleFigure(this.pMax);

            this.TrianglesTree.Items.Add(triangleFigure.shapeNode);

            this.figuresList.Add(triangleFigure);
        }

        private void CreateCircleShape(object sender, RoutedEventArgs e)
        {

            CircleFigure circle = new CircleFigure(this.pMax);

            this.CirclesTree.Items.Add(circle.shapeNode);
            this.figuresList.Add(circle);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!StopClicked)
            {
                StopClicked = true;

            }
            else
            {
                StopClicked = false;
            }
        }
    }
}
