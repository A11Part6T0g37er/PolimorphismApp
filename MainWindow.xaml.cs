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
        Storyboard ellipseStoryboard = new Storyboard();
        public MainWindow()
        {
            InitializeComponent();



            // Create a NameScope for this page so that
            // Storyboards can be used.
            NameScope.SetNameScope(this, new NameScope());
        }


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

        private RadialGradientBrush InitBrush()
        {
            RadialGradientBrush brush = new RadialGradientBrush();
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 0.250));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 0.100));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7689"), 8));
            return brush;


        }

        private void CreateRectangleShape(object sender, RoutedEventArgs e)
        {
           


            Point pMax = new Point(canvasFigures.ActualWidth - 20, canvasFigures.ActualHeight - 20);
          




            RectangleFigure rectangle = new RectangleFigure(pMax);
            rectangle.Draw();
            canvasFigures.Children.Add((UIElement)rectangle);


            

        }


        //Trying to make it move
        private void CreateTriangleShape(object sender, RoutedEventArgs e)
        {
            //Polygon Rendershape = new Polygon() ;
            //System.Windows.Point Point1 = new System.Windows.Point(30, 10);
            //Point Point2 = new System.Windows.Point(40, 40);
            //Point Point3 = new System.Windows.Point(20, 40);
            //PointCollection polygonPoints = new PointCollection();
            //polygonPoints.Add(Point1);
            //polygonPoints.Add(Point2);
            //polygonPoints.Add(Point3);
            //Rendershape.Points = polygonPoints;
            //Rendershape.Fill = InitBrush();


            TriangleFigure triangleFigure = new TriangleFigure(new Point(canvasFigures.ActualWidth - 20, canvasFigures.ActualHeight - 20));

            triangleFigure.Draw();

            //Animation attemp
            DoubleAnimation animDouble = new DoubleAnimation();
            animDouble.AutoReverse = true;
            animDouble.From = 100;
            animDouble.To = 0;
            animDouble.Duration = new TimeSpan(0, 0, 2);


            // Create a NameScope for the page so that 
            // we can use Storyboards.
            NameScope.SetNameScope(this, new NameScope());


            // Assign the EllipseGeometry a name so that
            // it can be targeted by a Storyboard.
            this.RegisterName(
               "AnimatedTranslateTransform", triangleFigure);

            Storyboard pathAnimationStoryboard = new Storyboard();
            pathAnimationStoryboard.RepeatBehavior = RepeatBehavior.Forever;
            pathAnimationStoryboard.Children.Add(animDouble);

            Storyboard.SetTargetName(animDouble, "AnimatedTranslateTransform");
            Storyboard.SetTargetProperty(animDouble,
                new PropertyPath(TranslateTransform.YProperty));

          
            triangleFigure.polygon.Loaded += delegate (object sendre, RoutedEventArgs eventer)
            {
                // Start the storyboard.
                //pathAnimationStoryboard.Begin(this);
            };

            canvasFigures.Children.Add(triangleFigure.polygon);


            
        }

        private void CreateCircleShape(object sender, RoutedEventArgs e)
        {
            Point pMax = new Point(canvasFigures.ActualWidth - 20, canvasFigures.ActualHeight - 20);

            CircleFigure circle = new CircleFigure(pMax);
            circle.Draw();
            canvasFigures.Children.Add(circle.ellipse);
        }
    }
}
