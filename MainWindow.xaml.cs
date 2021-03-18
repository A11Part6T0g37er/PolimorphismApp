// <copyright file="MainWindow.xaml.cs" company="IndieWare Ink.">
// Copyright (c) IndieWare Ink.. All rights reserved.
// </copyright>

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
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
            InitializeComponent();
            // used by all shapes
            pMax = new Point(canvasFigures.ActualWidth - 30, canvasFigures.ActualHeight - 30);

            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            pMax = new Point(canvasFigures.ActualWidth - 30, canvasFigures.ActualHeight - 30);

            foreach (var figure in figuresList)
            {
                if (figure != null)
                {
                    figure.Draw(canvasFigures);
                    figure.Move(pMax);
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

            RectangleFigure rectangle = new RectangleFigure(pMax);

            RectTree.Items.Add(rectangle.shapeNode);

            figuresList.Add(rectangle);

        }

        private void CreateTriangleShape(object sender, RoutedEventArgs e)
        {

            TriangleFigure triangleFigure = new TriangleFigure(pMax);

            TrianglesTree.Items.Add(triangleFigure.shapeNode);

            figuresList.Add(triangleFigure);
        }

        private void CreateCircleShape(object sender, RoutedEventArgs e)
        {

            CircleFigure circle = new CircleFigure(pMax);

            CirclesTree.Items.Add(circle.shapeNode);
            figuresList.Add(circle);

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
            OpenFileDialog Fd = new OpenFileDialog() { DefaultExt = "xaml", InitialDirectory = @"C:\Users\dct\source\repos\PolimorphismApp\bin\Debug" };
            Fd.ShowDialog();
            string LoadedFileName = Fd.FileName ?? String.Empty;

            Canvas newCanvas = new Canvas();

            if (!LoadedFileName.Equals(String.Empty))
            {
            FileStream Fs = new FileStream(@LoadedFileName, FileMode.Open);

            newCanvas = System.Windows.Markup.XamlReader.Load(Fs) as Canvas;

            Fs.Close();

            }
            
            // make canvas and tree clean from any objects it`s currently has in order to restore previous state
            figuresList.Clear();
            canvasFigures.Children.Clear();


            CirclesTree.Items.Clear();
            RectTree.Items.Clear();
            TrianglesTree.Items.Clear();
            

            // almost completely workable canvas objects transfer by XAML generated file. Unknown issue with coordinates of shapes.
            // whole code underneath is valid and executable as mentioned
            #region Ancestor of accomplished binarry attemp

            #region RepopulateFromCanvas
            for (int i = 0; i < newCanvas.Children.Count; i++)
            {
                if (newCanvas.Children[i] is Rectangle)
                {

                    RectangleFigure rf = new RectangleFigure(pMax) { X = i + 1 * 30, Y = i + 1 * 50 };
                    /* { X = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).X, Y = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).Y }*/

                    figuresList.Add(rf);
                }
                if (newCanvas.Children[i] is Ellipse)
                {
                    CircleFigure cf = new CircleFigure(pMax) { X = i + 1 * 40, Y = i + 1 * 40 };

                    figuresList.Add(cf);
                    /* { X = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).X, Y = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).Y }*/
                }
                if (newCanvas.Children[i] is Polygon)
                {
                    TriangleFigure tf = new TriangleFigure(pMax) { X = i + 1 * 70, Y = i + 1 * 80 };

                    figuresList.Add(tf);
                    /* { X = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).X, Y = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).Y }*/
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

            #endregion

            DeserialiseBinarry(path: "Figures.bin");
           
        }

        private void DeserialiseBinarry(string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream streamwriter = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            
            List<AbstractFigure> intermadiate = null;
            if (File.Exists(path))
                using (streamwriter)
                {
                    intermadiate = bf.Deserialize(streamwriter) as List<AbstractFigure>;
                }
            intermadiate.ForEach(x =>
            {
                RectangleFigure fr;
                CircleFigure cf;
                TriangleFigure tf;
                if (x.shapeForm is ShapeForm.Rectangle)
                {
                    fr = new RectangleFigure(pMax) { X = x.X, Y = x.Y, Dx = x.Dx, Dy = x.Dy };
                    figuresList.Add(fr);
                    RectTree.Items.Add(new TreeViewItem() { Header = fr.shapeNode });
                }
                if (x.shapeForm is ShapeForm.Triangle)
                {
                    tf = new TriangleFigure(pMax) { X = x.X, Y = x.Y, Dx = x.Dx, Dy = x.Dy };
                    figuresList.Add(tf);
                    TrianglesTree.Items.Add(new TreeViewItem() { Header = tf.shapeNode });
                }
                if (x.shapeForm is ShapeForm.Ellipse)
                {
                    cf = new CircleFigure(pMax) { X = x.X, Y = x.Y, Dx = x.Dx, Dy = x.Dy };
                    figuresList.Add(cf);
                    CirclesTree.Items.Add(new TreeViewItem() { Header = cf.shapeNode });
                }


            });

        }

        // serialization testcase
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            string path = "Figures.bin";


            WorkingCanvasSave("Figa.xaml");

            BinarySerialisation(path);

            string pathXML = "Figures.xml";
           

           
            SerializeToXml(figuresList, xmlFilePath: pathXML);
        }

        private void BinarySerialisation(string path)
        {
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
            XmlSerializer xmlSerializer = new XmlSerializer(anyobject.GetType() );

            using (StreamWriter writer = new StreamWriter(xmlFilePath))
            {
                
                xmlSerializer.Serialize(writer, anyobject);
            }
        }

    }
}
