// <copyright file="MainWindow.xaml.cs" company="IndieWare Ink.">
// Copyright (c) IndieWare Ink.. All rights reserved.
// </copyright>

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            // used by all shapes
            this.pMax = new Point(this.canvasFigures.ActualWidth - 30, this.canvasFigures.ActualHeight - 30);

            timer.Tick += this.Timer_Tick;
            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.pMax = new Point(this.canvasFigures.ActualWidth - 30, this.canvasFigures.ActualHeight - 30);

            foreach (var figure in this.figuresList)
            {
                if (figure != null)
                {
                    figure.Draw(this.canvasFigures);
                    figure.Move(this.pMax);
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
            // Dialog window openFile
            //OpenFileDialog Fd = new OpenFileDialog() { DefaultExt = "xaml", InitialDirectory = @"C:\Users\dct\source\repos\PolimorphismApp\bin\Debug" };
            //Fd.ShowDialog();
            //string LoadedFileName = Fd.FileName;

         /*   FileStream Fs = new FileStream(@LoadedFileName, FileMode.Open);
            Canvas newCanvas = new Canvas();

            newCanvas = System.Windows.Markup.XamlReader.Load(Fs) as Canvas;

            Fs.Close();

            // make canvas and tree clean from any objects it`s currently has in order to restore previous state
            figuresList.Clear();
            canvasFigures.Children.Clear();


            CirclesTree.Items.Clear();
            RectTree.Items.Clear();
            RectTree.Items.Clear();

            #region RepopulateFromCanvas
            for (int i = 0; i < newCanvas.Children.Count; i++)
            {
                if (newCanvas.Children[i] is Rectangle)
                {

                    RectangleFigure rf = new RectangleFigure(pMax) { X = i + 1 * 30, Y = i + 1 * 50 }; *//*{ X = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).X, Y = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).Y }*//*

                    figuresList.Add(rf);
                }
                if (newCanvas.Children[i] is Ellipse)
                {
                    CircleFigure cf = new CircleFigure(pMax) { X = i + 1 * 40, Y = i + 1 * 40 };

                    figuresList.Add(cf); *//*{ X = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).X, Y = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).Y }*//*
                }
                if (newCanvas.Children[i] is Polygon)
                {
                    TriangleFigure tf = new TriangleFigure(pMax) { X = i + 1 * 70, Y = i + 1 * 80 };

                    figuresList.Add(tf); *//*{ X = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).X, Y = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).Y }*//*
                }
            } 
            #endregion
            #region TreeViewPopulate
            foreach (var item in figuresList)
            {
                if (item is RectangleFigure)
                    RectTree.Items.Add(item.shapeNode);
                if (item is CircleFigure)
                    CirclesTree.Items.Add(item.shapeNode);
                if (item is TriangleFigure)
                    TrianglesTree.Items.Add(item.shapeNode);
            }
            #endregion
*/

            DeserialiseBinarry(  path: "Figures.bin");
            //string path = @"C:\Users\dct\source\repos\PolimorphismApp\bin\Debug\Figa.xaml";
            //string defaultNamespace = "PolimorphismApp";
            //string folderName = "Debug";
            //string fileName = "Figa.xaml";
            //string path1 = String.Format("{0}.bin.{1}.{2}", defaultNamespace, folderName, fileName);

            //object result =   bINNARYDeSerialization(listnewAbstract, path);

            //UIElementCollection s = new UIElementCollection(canvasFigures, canvasFigures);
            //s.Add((UIElement)XamlReader.Parse(result.ToString()));

            //StringReader stringReader = new StringReader("Test string");
            //XmlReader xmlReader = XmlReader.Create(stringReader);
            //var AAA = XamlReader.Load(xmlReader);
            //MessageBox.Show(s.ToString());

        }

        private void DeserialiseBinarry(string  path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream streamwriter = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            //RectangleFigure result = new RectangleFigure(pMax);
            List<AbstractFigure> intermadiate = null;
            if (File.Exists(path))
            using (streamwriter)
            {
              intermadiate =   bf.Deserialize(streamwriter) as List<AbstractFigure>;
            }
            intermadiate.ForEach(x => { 
             RectangleFigure fr = new RectangleFigure(pMax) { X = x.X, Y = x.Y ,Dx = x.Dx,Dy = x.Dy};
            figuresList.Add(fr);
            
            }  );
           
           /* result.X = intermadiate.X;
            result.Y = intermadiate.Y;*/
            //MessageBox.Show(result.X.ToString());
        }

        // serialization testcase
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            string path = "Figures.bin";
            RectangleFigure rf = (RectangleFigure)(AbstractFigure)(new RectangleFigure(pMax));

            WorkingCanvasSave("Figa.xaml");
            //SerializeToXml<RectangleFigure>(rf, path);
             
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream streamwriter = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

            using (streamwriter)
            {
                formatter.Serialize(streamwriter, figuresList);
            }
        }

        private void WorkingCanvasSave(string path)
        {

            FileStream streamwriter = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            
            using (streamwriter)
            {
                XamlWriter.Save(canvasFigures, streamwriter);
                 
            }

        }
        public static void SerializeToXml<T>(T anyobject, string xmlFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(anyobject.GetType());

            using (StreamWriter writer = new StreamWriter(xmlFilePath))
            {
                xmlSerializer.Serialize(writer, anyobject);
            }
        }

    }
}
