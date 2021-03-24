﻿// <copyright file="MainWindow.xaml.cs" company="IndieWare Ink.">
// Copyright (c) IndieWare Ink.. All rights reserved.
// </copyright>

using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
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
            Interval = TimeSpan.FromMilliseconds(28),
        };

        public bool StopClicked { get; private set; } = false;
        public object locker { get; private set; } = new object();

        public MainWindow()
        {
            this.InitializeComponent();
            // used by all shapes
            this.pMax = new Point(this.canvasFigures.ActualWidth - 30, this.canvasFigures.ActualHeight - 30);

            this.timer.Tick += this.Timer_Tick;
            this.timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.pMax = new Point(this.canvasFigures.ActualWidth - 30, this.canvasFigures.ActualHeight - 30);

            Thread[] tread = new Thread[1];
            // TODO:
            //Thread thread = new Thread(start: new ParameterizedThreadStart((AbstractFigure, object) => NewMethod)) { Name = "Drawing Thread" };

            foreach (var figure in this.figuresList)
            {
                if (figure != null)
                {
                    //Thread   myThread = new Thread(new ParameterizedThreadStart((figure1) => figure.Draw(this.canvasFigures))) { Name="Draw`em up!"};
                    //   myThread.SetApartmentState(ApartmentState.STA); // controls must be constructed in the STA thread
                    //   myThread.IsBackground = true;
                    //myThread.Start();
                    //myThread.Join();

                    int id = Thread.CurrentThread.ManagedThreadId;
                    Trace.WriteLine("Ticker thread: " + id);
                    SynchronizationContext uiContext = SynchronizationContext.Current;


                  tread[0]   = new Thread(new ThreadStart(() => NewMethod(figure, uiContext))) { Name = "Drawing Thread" };
                     
                    tread[0].Start();
                    

                        figure.Move(this.pMax);
                    
                    //Task.Factory.StartNew(()=> figure.Draw(this.canvasFigures)).Wait();
                }
            }
            #region Event raising
            List<AbstractFigure> allShapes = this.figuresList.FindAll(x => x.CollisionManager != null);
            List<AbstractFigure> eventList = allShapes;


            List<AbstractFigure> eventList2 = eventList;
            if (eventList.Count > 1)
            {
                eventList2.RemoveAt(0);

                foreach (var x in eventList.ToArray())
                {
                    if (x.CollisionManager != null)
                    {
                        if (eventList2.Count > 2)
                        {
                            foreach (var y in eventList2)
                            {
                                if (y.CollisionManager != null)
                                {
                                    if ((y.shapeForm == x.shapeForm) && (y.id != x.id))
                                    {
                                        var x1 = (y.X);
                                        var y1 = (y.Y);

                                        var x2 = x.X;
                                        var y2 = x.Y;

                                        Rect r1 = new Rect(x1, y1, 40, 40);

                                        Rect r2 = new Rect(x2, y2, 40, 40);

                                        if (r1.IntersectsWith(r2))
                                        {
                                            x.CollisionManager.SimulateCollision(x.X, x.Y);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            #endregion




            //var ellipse1Geom = first.rect.RenderedGeometry;
            //var ellipse2Geom = second.rect.RenderedGeometry;
            //var detail = ellipse1Geom.FillContainsWithDetail(ellipse2Geom);
            //if (detail != IntersectionDetail.Empty)
            //{

            //    // We have an intersection or one contained inside the other


            //}
        }

        private void NewMethod(AbstractFigure figure, object uiContext)
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            Trace.WriteLine("Run thread: " + id);

            SynchronizationContext uiContext2 = uiContext as SynchronizationContext;

            uiContext2.Post(UpdateUI,figure);
        }
        private void UpdateUI(object state)
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            Trace.WriteLine("UpdateUI thread:" + id);
            var figa = state as AbstractFigure;
            figa.Draw(this.canvasFigures);
        }



        /// <summary>
        /// Stop / Play shape moving around canvas.   Mouse.DirectlyOver method is used to get a shape from canva, which then is casted into proper figure in order to remove or add to figureList; depends on toggle button named 'StopClicked'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IInputElement clickedElement = Mouse.DirectlyOver;

            if (clickedElement is Rectangle)
            {
                var obj = clickedElement as Rectangle;
                int i = this.canvasFigures.Children.IndexOf(obj);

                Rectangle some = (Rectangle)this.canvasFigures.Children[i];

                int X = (int)obj.TransformToAncestor(this.canvasFigures).Transform(new Point(0, 0)).X;
                int Y = (int)obj.TransformToAncestor(this.canvasFigures).Transform(new Point(0, 0)).Y;

                var toDel = this.figuresList.Find(x => x.X == X && x.Y == Y);

                if (this.StopClicked)
                    this.figuresList.Remove(toDel);
                if (!this.StopClicked)
                {
                    if (!this.figuresList.Contains(toDel))
                    {
                        RectangleFigure rf = new RectangleFigure(this.pMax) { Y = Y, X = X };
                        this.canvasFigures.Children.Remove(obj);
                        this.figuresList.Add(rf);
                    }
                }
            }

            if (clickedElement is Ellipse)
            {
                var obj = clickedElement as Ellipse;
                int i = this.canvasFigures.Children.IndexOf(obj);

                Ellipse some = (Ellipse)this.canvasFigures.Children[i];

                int X = (int)obj.TransformToAncestor(this.canvasFigures).Transform(new Point(0, 0)).X;
                int Y = (int)obj.TransformToAncestor(this.canvasFigures).Transform(new Point(0, 0)).Y;
                var toDel = this.figuresList.Find(x => x.X == X && x.Y == Y);

                if (this.StopClicked)
                    this.figuresList.Remove(toDel);
                if (!this.StopClicked)
                {
                    if (!this.figuresList.Contains(toDel))
                    {
                        CircleFigure rf = new CircleFigure(this.pMax) { Y = Y, X = X };
                        this.canvasFigures.Children.Remove(obj);
                        this.figuresList.Add(rf);
                    }
                }
            }

            if (clickedElement is Polygon)
            {
                var obj = clickedElement as Polygon;
                int i = this.canvasFigures.Children.IndexOf(obj);

                Polygon some = (Polygon)this.canvasFigures.Children[i];

                int X = (int)obj.TransformToAncestor(this.canvasFigures).Transform(new Point(0, 0)).X;
                int Y = (int)obj.TransformToAncestor(this.canvasFigures).Transform(new Point(0, 0)).Y;
                var toDel = this.figuresList.Find(x => x.X == X && x.Y == Y);

                if (this.StopClicked)
                    this.figuresList.Remove(toDel);
                if (!this.StopClicked)
                {
                    if (!this.figuresList.Contains(toDel))
                    {
                        TriangleFigure rf = new TriangleFigure(this.pMax) { Y = Y, X = X };
                        this.canvasFigures.Children.Remove(obj);
                        this.figuresList.Add(rf);
                    }
                }
            }
        }

        private void CreateRectangleShape(object sender, RoutedEventArgs e)
        {
            RectangleFigure rectangle = new RectangleFigure(this.pMax);

            this.RectTree.Items.Add(new TreeViewItem() { Header = rectangle.shapeNode });

            this.figuresList.Add(rectangle);
        }

        private void CreateTriangleShape(object sender, RoutedEventArgs e)
        {
            TriangleFigure triangleFigure = new TriangleFigure(this.pMax);

            this.TrianglesTree.Items.Add(new TreeViewItem() { Header = triangleFigure.shapeNode });

            this.figuresList.Add(triangleFigure);
        }

        private void CreateCircleShape(object sender, RoutedEventArgs e)
        {
            CircleFigure circle = new CircleFigure(this.pMax);


            this.CirclesTree.Items.Add(new TreeViewItem() { Header = circle.shapeNode });
            this.figuresList.Add(circle);
        }

        private void Button_StopPlay_Click(object sender, RoutedEventArgs e)
        {
            if (!this.StopClicked)
            {
                this.StopClicked = true;
            }
            else
            {
                this.StopClicked = false;
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
                this.figuresList.Clear();
                this.canvasFigures.Children.Clear();


                this.CirclesTree.Items.Clear();
                this.RectTree.Items.Clear();
                this.TrianglesTree.Items.Clear();

                switch (ext)
                {
                    case ".json":
                        this.JSONDeserializing(pathJSON: loadedFileName);

                        break;
                    case ".xml":
                        this.DeserializeXML(path: loadedFileName);

                        break;
                    case ".bin":
                        this.DeserialiseBinarry(path: loadedFileName);
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
            this.InitializeListDeserialized(newList);
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
                    fr = new RectangleFigure(this.pMax) { X = x.X, Y = x.Y, Dx = x.Dx, Dy = x.Dy, id = x.id };
                    this.figuresList.Add(fr);
                    this.RectTree.Items.Add(new TreeViewItem() { Header = fr.shapeNode });
                }
                if (x.shapeForm is ShapeForm.Triangle)
                {
                    tf = new TriangleFigure(this.pMax) { X = x.X, Y = x.Y, Dx = x.Dx, Dy = x.Dy, id = x.id };
                    this.figuresList.Add(tf);
                    this.TrianglesTree.Items.Add(new TreeViewItem() { Header = tf.shapeNode });
                }
                if (x.shapeForm is ShapeForm.Ellipse)
                {
                    cf = new CircleFigure(this.pMax) { X = x.X, Y = x.Y, Dx = x.Dx, Dy = x.Dy, id = x.id };
                    this.figuresList.Add(cf);
                    this.CirclesTree.Items.Add(new TreeViewItem() { Header = cf.shapeNode });
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
            this.InitializeListDeserialized(intermadiate);
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
            this.InitializeListDeserialized(intermadiate);
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
                        this.JSONSerialization(pathJSON: SaveFileName, anyObject: this.figuresList);

                        break;
                    case ".xml":
                        SerializeToXml(xmlFilePath: SaveFileName, anyObject: this.figuresList);

                        break;
                    case ".bin":

                        this.BinarySerialization(path: SaveFileName, anyObject: this.figuresList);
                        break;
                }

                // In the times saving everything from Canva. For memory only
                // WorkingCanvasSave("Figa.xaml");
            }
        }

        private void JSONSerialization<T>(string pathJSON, T anyObject)
        {
            TextWriter textWriter = new StreamWriter(pathJSON);
            JsonTextWriter streamwriter = new JsonTextWriter(textWriter);

            JsonSerializer jz = new JsonSerializer();
            using (streamwriter)
            {
                jz.Serialize(streamwriter, anyObject);
            }
            textWriter.Close();
        }

        private void BinarySerialization<T>(string path, T anyObject)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream streamwriter = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

            using (streamwriter)
            {
                formatter.Serialize(streamwriter, anyObject);
            }
        }


        /// <summary>
        /// Save all objects on canvas into xaml file of path string.
        /// </summary>
        /// <param name="path">Relative filename of string type.</param>
        /// <remarks>NOT really need it. </remarks> 
        private void WorkingCanvasSave(string path)
        {
            FileStream streamwriter = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

            using (streamwriter)
            {
                XamlWriter.Save(this.canvasFigures, streamwriter);
            }
        }
        public static void SerializeToXml<T>(T anyObject, string xmlFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(anyObject.GetType());

            using (StreamWriter writer = new StreamWriter(xmlFilePath))
            {
                xmlSerializer.Serialize(writer, anyObject);
            }
        }

        private void Button_AddEvent(object sender, RoutedEventArgs e)
        {
            var selected = ((TreeViewItem)(this.GlobalFiguresTree.SelectedItem));
            if (selected == null || (selected.Header.ToString() == "Triangles" || selected.Header.ToString() == "Figures" || selected.Header.ToString() == "Rectangles" || selected.Header.ToString() == "Circles"))
            {
                return;
            }
            this.figuresList.Find(x => x.shapeNode == selected?.Header.ToString())?.Register();









            selected.Background = new LinearGradientBrush(new GradientStopCollection(new List<GradientStop>() { new GradientStop((Color)ColorConverter.ConvertFromString("#7FB3CFFD"), 0), new GradientStop((Color)ColorConverter.ConvertFromString("#80F3BDFB"), 1) }));
        }

        private void Button_RemoveEvent(object sender, RoutedEventArgs e)
        {
            var selected = ((TreeViewItem)(this.GlobalFiguresTree.SelectedItem));
            if (selected == null || (selected.Header.ToString() == "Triangles" || selected.Header.ToString() == "Figures" || selected.Header.ToString() == "Rectangles" || selected.Header.ToString() == "Circles"))
            {
                return;
            }

            this.figuresList.Find(x => x.shapeNode == selected.Header.ToString()).Unregister();
            if (selected != null)
                if (!selected.Background.Equals(null))
                {
                    selected.Background = new SolidColorBrush();
                }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string Text = ((TreeViewItem)((TreeView)sender).SelectedItem).Header.ToString();
        }
    }
}

