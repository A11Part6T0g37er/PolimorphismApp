// <copyright file="MainWindow.xaml.cs" company="IndieWare Ink.">
// Copyright (c) IndieWare Ink.. All rights reserved.
// </copyright>

using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Serialization;
using Path = System.IO.Path;

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

            RectTree.Items.Add(new TreeViewItem() { Header = rectangle.shapeNode });

            figuresList.Add(rectangle);

        }

        private void CreateTriangleShape(object sender, RoutedEventArgs e)
        {

            TriangleFigure triangleFigure = new TriangleFigure(pMax);

            TrianglesTree.Items.Add(new TreeViewItem() { Header = triangleFigure.shapeNode });

            figuresList.Add(triangleFigure);
        }

        private void CreateCircleShape(object sender, RoutedEventArgs e)
        {

            CircleFigure circle = new CircleFigure(pMax);


            CirclesTree.Items.Add(new TreeViewItem() { Header = circle.shapeNode });
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
        // Нужно разработать собственное событие в 4 этапа по Рихтеру.
        // Событие должно срабатывать когда 2 фигуры одного типа (круг с кругом , прямоугольник с прямоугольником, треугольник с треугольником ) пересекаются.
        // Если пересечение с фигурой другого типа то ничего не происходит.
        // В контексте в наследнике евентаргс передавайте координату пересечения. Дальше на это событие нужно динамически вешать функцию

        //Давайте на экран добавим две кнопки + и -
        //Если вы выбираете в списке слева какую-то фигуру
        //И нажимаете кнопку + То на событие разработанное вами в этой фигуре вешается функция

        //Если вы нажимаете несколько раз то таких приклеиваний к событию будет несколько
        //Точно также у определённой фигуры можно и отнять вызов функции
        //Функцию которую будем вешать пусть будет простой

        //Например просто вызов звука beep
        //И вывод координату столкновения на консоль


        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {

            // Dialog window openFile
            OpenFileDialog Fd = new OpenFileDialog() { InitialDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) };
            Fd.Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml|Binary files (*.bin)|*.bin|All files (*.*)|*.*";

            Fd.ShowDialog();
            string loadedFileName = Fd.FileName ?? string.Empty;


            if (!string.IsNullOrEmpty(loadedFileName))
            {
                string ext = Path.GetExtension(loadedFileName);

                // make canvas and tree clean from any objects it`s currently has in order to restore previous state
                figuresList.Clear();
                canvasFigures.Children.Clear();


                CirclesTree.Items.Clear();
                RectTree.Items.Clear();
                TrianglesTree.Items.Clear();

                switch (ext)
                {
                    case ".json":
                        JSONDeserializing(pathJSON: loadedFileName);

                        break;
                    case ".xml":
                        DeserializeXML(path: loadedFileName);

                        break;
                    case ".bin":
                        DeserialiseBinarry(path: loadedFileName);
                        break;
                }


                // almost completely workable canvas objects transfer by XAML generated file. Unknown issue with coordinates of shapes.
                // whole code underneath is valid and executable as mentioned
                #region Ancestor of accomplished binarry attemp

                // for canvas glory sake
                /*Canvas newCanvas = new Canvas();

                if (!LoadedFileName.Equals(String.Empty))
                {
                    FileStream Fs = new FileStream(@LoadedFileName, FileMode.Open);

                    newCanvas = System.Windows.Markup.XamlReader.Load(Fs) as Canvas;

                    Fs.Close();

                }*/

                #region RepopulateFromCanvas
                /* for (int i = 0; i < newCanvas.Children.Count; i++)
                 {
                     if (newCanvas.Children[i] is Rectangle)
                     {

                         RectangleFigure rf = new RectangleFigure(pMax) { X = i + 1 * 30, Y = i + 1 * 50 };
                         *//* { X = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).X, Y = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).Y }*//*

                         figuresList.Add(rf);
                     }
                     if (newCanvas.Children[i] is Ellipse)
                     {
                         CircleFigure cf = new CircleFigure(pMax) { X = i + 1 * 40, Y = i + 1 * 40 };

                         figuresList.Add(cf);
                         *//* { X = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).X, Y = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).Y }*//*
                     }
                     if (newCanvas.Children[i] is Polygon)
                     {
                         TriangleFigure tf = new TriangleFigure(pMax) { X = i + 1 * 70, Y = i + 1 * 80 };

                         figuresList.Add(tf);
                         *//* { X = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).X, Y = (int)newCanvas.Children[i].TransformToAncestor(newCanvas).Transform(newPoint(0, 0)).Y }*//*
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
          */
                #endregion

                #endregion







            }
        }

        private void JSONDeserializing(string pathJSON)
        {
            StreamReader textReader = new StreamReader(pathJSON);
            string result;
            using (textReader)
            {
                result = textReader.ReadToEnd();

            }
            var newList = JsonConvert.DeserializeObject<List<AbstractFigure>>(result);
            InitializeListDeserialized(newList);
        }

        private void InitializeListDeserialized(List<AbstractFigure> newList)
        {
            newList.ForEach(x =>
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

        private void DeserializeXML(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<AbstractFigure>));
            FileStream streamwriter = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            List<AbstractFigure> intermadiate = null;
            if (File.Exists(path))
                using (streamwriter)
                {
                    intermadiate = serializer.Deserialize(streamwriter) as List<AbstractFigure>;
                }
            InitializeListDeserialized(intermadiate);

        }
        // workable function of Binary branch
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
            InitializeListDeserialized(intermadiate);

        }

        // serialization testcase
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            string path = "Figures.bin";
            SaveFileDialog Fd = new SaveFileDialog() { InitialDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) };
            Fd.Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml|Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            Fd.ShowDialog();
            string SaveFileName = Fd.FileName ?? String.Empty;


            if (!string.IsNullOrEmpty(SaveFileName))
            {
                string ext = Path.GetExtension(SaveFileName);

                switch (ext)
                {


                    case ".json":
                        JSONSerialization(SaveFileName);

                        break;
                    case ".xml":
                        SerializeToXml(figuresList, xmlFilePath: SaveFileName);

                        break;
                    case ".bin":

                        BinarySerialization(SaveFileName);
                        break;

                }

                // In the times saving everything from Canva. For memory only
                // WorkingCanvasSave("Figa.xaml");




            }
        }

        private void JSONSerialization(string pathJSON)
        {
            TextWriter textWriter = new StreamWriter(pathJSON);
            JsonTextWriter streamwriter = new JsonTextWriter(textWriter);

            JsonSerializer jz = new JsonSerializer();
            using (streamwriter)
            {

                jz.Serialize(streamwriter, figuresList);
            }
            textWriter.Close();
        }

        private void BinarySerialization(string path)
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
            XmlSerializer xmlSerializer = new XmlSerializer(anyobject.GetType());

            using (StreamWriter writer = new StreamWriter(xmlFilePath))
            {

                xmlSerializer.Serialize(writer, anyobject);
            }
        }

        private void Button_AddEvent(object sender, RoutedEventArgs e)
        {

        }

        private void Button_RemoveEvent(object sender, RoutedEventArgs e)
        {
            AbstractFigure ab = figuresList.Find(x => x.shapeNode == ((TreeViewItem)(GlobalFiguresTree.SelectedItem)).Header.ToString());



        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string Text = ((TreeViewItem)((TreeView)sender).SelectedItem).Header.ToString();

        }

    }
}

