// <copyright file="MainWindow.xaml.cs" company="IndieWare Ink.">
// Copyright (c) IndieWare Ink.. All rights reserved.
// </copyright>

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Serialization;

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


        private void CanvasArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IInputElement clickedElement = Mouse.DirectlyOver;







            if (clickedElement is Rectangle)
            {
                var obj = clickedElement as Rectangle;
                int i = canvasFigures.Children.IndexOf(obj);

                Rectangle some = (Rectangle)canvasFigures.Children[i];


                int X = (int)obj.TransformToAncestor(canvasFigures).Transform(new Point(0, 0)).X;
                int Y = (int)obj.TransformToAncestor(canvasFigures).Transform(new Point(0, 0)).Y;
                var toDel = figuresList.Find(x => x.X == X && x.Y == Y);



                if (StopClicked)
                    figuresList.Remove(toDel);
                if (!StopClicked)
                {
                    if (!figuresList.Contains(toDel))
                    {
                        RectangleFigure rf = new RectangleFigure(pMax) { Y = Y, X = X };
                        canvasFigures.Children.Remove(obj);
                        figuresList.Add(rf);
                    }
                }

            }

            if (clickedElement is Ellipse)
            {
                var obj = clickedElement as Ellipse;
                int i = canvasFigures.Children.IndexOf(obj);

                Ellipse some = (Ellipse)canvasFigures.Children[i];


                int X = (int)obj.TransformToAncestor(canvasFigures).Transform(new Point(0, 0)).X;
                int Y = (int)obj.TransformToAncestor(canvasFigures).Transform(new Point(0, 0)).Y;
                var toDel = figuresList.Find(x => x.X == X && x.Y == Y);



                if (StopClicked)
                    figuresList.Remove(toDel);
                if (!StopClicked)
                {
                    if (!figuresList.Contains(toDel))
                    {
                        CircleFigure rf = new CircleFigure(pMax) { Y = Y, X = X };
                        canvasFigures.Children.Remove(obj);
                        figuresList.Add(rf);
                    }
                }

            }

            if (clickedElement is Polygon)
            {
                var obj = clickedElement as Polygon;
                int i = canvasFigures.Children.IndexOf(obj);

                Polygon some = (Polygon)canvasFigures.Children[i];


                int X = (int)obj.TransformToAncestor(canvasFigures).Transform(new Point(0, 0)).X;
                int Y = (int)obj.TransformToAncestor(canvasFigures).Transform(new Point(0, 0)).Y;
                var toDel = figuresList.Find(x => x.X == X && x.Y == Y);



                if (StopClicked)
                    figuresList.Remove(toDel);
                if (!StopClicked)
                {
                    if (!figuresList.Contains(toDel))
                    {
                        TriangleFigure rf = new TriangleFigure(pMax) { Y = Y, X = X };
                        canvasFigures.Children.Remove(obj);
                        figuresList.Add(rf);
                    }

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

        // TODO:
        // сделайте меню file в которое добавьте 2 подменю save open
        // при саве сохраняйте все добавленые фигуры в файл в формате .bin
        // используйте стандартную сериализацию с помощью атрибута serializable.
        // потом добавьте формат xml и формат json .
        // с форматом xml могут быть проблемы потому что не все объекты нормально сериализуются.
        // разберитесь как решить эту проблему.
        // по опену можно прочитать любой из форматов .bin .json .xml и восстановить все фигуры

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Fd = new OpenFileDialog();
            Fd.ShowDialog();
            string LoadedFileName = Fd.FileName;

            string path = "Figa.binnary";

            List<AbstractFigure> listnewAbstract = new List<AbstractFigure>();


            
           
            //bINNARYDeSerialization(listnewAbstract);

            canvasFigures.Children.Clear();

            figuresList.Clear();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            object result = null;
            using (fs)
            {

             result =   bf.Deserialize(fs);
            }
            UIElementCollection s = new UIElementCollection(canvasFigures, canvasFigures);
            s.Add((UIElement)XamlReader.Parse(result.ToString()) );

            MessageBox.Show(s.ToString());

        }

        private static void bINNARYDeSerialization(List<AbstractFigure> listnewAbstract)
        {
            
            BinaryFormatter bf = new BinaryFormatter();

            FileStream fsin = new FileStream("Figures.bin", FileMode.Open, FileAccess.Read, FileShare.None);
            try
            {
                using (fsin)
                {
                    MessageBox.Show(fsin.ToString());
                    MessageBox.Show(bf.Deserialize(fsin).ToString());
                    //listnewAbstract.Add((AbstractFigure)bf.Deserialize(fsin));

                    MessageBox.Show("Object Deserialized");


                }
            }
            catch
            {
                MessageBox.Show("An error has occured");
            }

           

        }

        // serialization testcase
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {

            WorkingCanvasSave("Figa.binnary");

            RectangleFigure rf = new RectangleFigure(pMax);
            Rectangle rect = new Rectangle() { Height = 40, Width = 40 };
            rect.Fill = rf.rect.Fill;
            
            string path = "Figures.bin";

            BinaryFormatter bf = new BinaryFormatter();

            FileStream fsout = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
                using (fsout)
                {
                    bf.Serialize(fsout, rect);
                    MessageBox.Show("Object Serialized");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error has occured " + ex.Message);
            }
            //WorkingCanvasSave(path);

        }

        private void WorkingCanvasSave(string path)
        {
            string mystrXAML = XamlWriter.Save(canvasFigures.Children);
           
            FileStream streamwriter = new FileStream(path, FileMode.Create , FileAccess.Write);

            BinaryFormatter bf = new BinaryFormatter();
           using(streamwriter)
            bf.Serialize(streamwriter,mystrXAML);

           
            
        }

        private static void XMLSerialization(RectangleFigure rf)
        {
            XmlSerializer formatter = new XmlSerializer(rf.GetType());

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("Figures.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, rf);

                MessageBox.Show("Объект сериализован");
            }
        }
    }
}
