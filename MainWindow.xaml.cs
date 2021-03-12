namespace PolimorphismApp
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<AbstractFigure> figuresList = new List<AbstractFigure>();
        public Point pMax;
        public MainWindow()
        {
            this.InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(14),
            };
            timer.Tick += Timer_Tick;
            timer.Start();



            // used by all shapes
            pMax = new Point(this.canvasFigures.ActualWidth - 30, this.canvasFigures.ActualHeight - 30);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            pMax = new Point(this.canvasFigures.ActualWidth - 30, this.canvasFigures.ActualHeight - 30);


            foreach (var figure in this.figuresList)
            {
                figure.Move(pMax);
                figure.Draw(canvasFigures);
            }

        }

        // just to be sure it still completes basic testcase
        private void CanvasArea_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Shape rendershape = new Ellipse() { Height = 40, Width = 40 };

            RadialGradientBrush brush = new RadialGradientBrush();

            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 0.850));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 0.400));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 8));
            rendershape.Fill = brush;

            Canvas.SetLeft(rendershape, e.GetPosition(this.canvasFigures).X);
            Canvas.SetTop(rendershape, e.GetPosition(this.canvasFigures).Y);

            this.canvasFigures.Children.Add(rendershape);

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
    }
}
