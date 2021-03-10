using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PolimorphismApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public Storyboard ellipseStoryboard = new Storyboard();
        public MainWindow()
        {
            InitializeComponent();



            // Create a NameScope for this page so that
            // Storyboards can be used.
            NameScope.SetNameScope(this, new NameScope());
        }

        //just to be sure it still completes basic testcase
        private void CanvasArea_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Shape Rendershape = new Ellipse() { Height = 40, Width = 40 };

            RadialGradientBrush brush = new RadialGradientBrush();

            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 0.850));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 0.400));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 8));
            Rendershape.Fill = brush;

            Canvas.SetLeft(Rendershape, e.GetPosition(canvasFigures).X);
            Canvas.SetTop(Rendershape, e.GetPosition(canvasFigures).Y);

            canvasFigures.Children.Add(Rendershape);

        }

       

        private void CreateRectangleShape(object sender, RoutedEventArgs e)
        {
           


            Point pMax = new Point(canvasFigures.ActualWidth - 20, canvasFigures.ActualHeight - 20);
          




            RectangleFigure rectangle = new RectangleFigure(pMax);
            rectangle.Draw(canvasFigures, RectTree);
            

           
            

        }


        //Trying to make it move
        private void CreateTriangleShape(object sender, RoutedEventArgs e)
        {
            


            TriangleFigure triangleFigure = new TriangleFigure(new Point(canvasFigures.ActualWidth - 20, canvasFigures.ActualHeight - 20));

          
          
            
            
            


            triangleFigure.Draw(canvasFigures, TrianglesTree);

            

            var animation1 = new DoubleAnimation( 500,
                         new Duration(new TimeSpan(0, 0, 0, 2, 0)));
            animation1.AutoReverse = true;

            TranslateTransform animatedTranslateTransform =
                new TranslateTransform();

            triangleFigure.polygon.BeginAnimation(TranslateTransform.XProperty, animation1);
            triangleFigure.polygon.BeginAnimation(TranslateTransform.YProperty, animation1);
           


        }

        private void CreateCircleShape(object sender, RoutedEventArgs e)
        {
            Point pMax = new Point(canvasFigures.ActualWidth - 20, canvasFigures.ActualHeight - 20);

            CircleFigure circle = new CircleFigure(pMax);
            circle.Draw(canvasFigures, CirclesTree);
            int x = 0;
            do
            {
                circle.Move(canvasFigures);
                ++x;
            }
            while (x < 10);
            
            
        }
    }
}
