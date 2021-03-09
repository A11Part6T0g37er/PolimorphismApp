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
            rectangle.Draw(canvasFigures);
           


            

        }


        //Trying to make it move
        private void CreateTriangleShape(object sender, RoutedEventArgs e)
        {
            


            TriangleFigure triangleFigure = new TriangleFigure(new Point(canvasFigures.ActualWidth - 20, canvasFigures.ActualHeight - 20));

            //attemp to bind to TreeViewList node
            TreeView treeView = new TreeView();
            treeView.Items.Add("fds");
            TreeViewItem trVI = new TreeViewItem();
          
            NameScope.SetNameScope(this, new NameScope());
            
            


            triangleFigure.Draw(canvasFigures);

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

           


            
        }

        private void CreateCircleShape(object sender, RoutedEventArgs e)
        {
            Point pMax = new Point(canvasFigures.ActualWidth - 20, canvasFigures.ActualHeight - 20);

            CircleFigure circle = new CircleFigure(pMax);
            circle.Draw(canvasFigures);
           
        }
    }
}
