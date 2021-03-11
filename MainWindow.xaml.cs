namespace PolimorphismApp
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        public Storyboard TriangleBoard = new Storyboard();

        public Storyboard StoryboardAttemp = new Storyboard();

        public MainWindow()
        {
            this.InitializeComponent();



            // Create a NameScope for this page so that
            // Storyboards can be used.
            NameScope.SetNameScope(this, new NameScope());
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



            Point pMax = new Point(this.canvasFigures.ActualWidth - 20, this.canvasFigures.ActualHeight - 20);





            RectangleFigure rectangle = new RectangleFigure(pMax);
            rectangle.Draw(this.canvasFigures, this.RectTree);





        }


        // Trying to make it move
        private void CreateTriangleShape(object sender, RoutedEventArgs e)
        {



            #region WorkingAnimation
            TriangleFigure triangleFigure = new TriangleFigure(new Point(this.canvasFigures.ActualWidth - 20, this.canvasFigures.ActualHeight - 20));

            triangleFigure.Draw(this.canvasFigures, this.TrianglesTree);

            var animation1 = new DoubleAnimation(100, 0, new Duration(new TimeSpan(0, 0, 0, 1, 0)))
            {
                AutoReverse = true
            };

            this.StoryboardAttemp.Children.Add(animation1);

            Storyboard.SetTarget(animation1, triangleFigure.polygon);
            Storyboard.SetTargetProperty(
                animation1,
                new PropertyPath(FrameworkElement.HeightProperty));

            StoryboardAttemp.Begin(); 
            #endregion


            // changing X Y properties block

            // Create a DoubleAnimationUsingPath to move the
            // rectangle horizontally along the path by animating
            // its TranslateTransform.
            TranslateTransform animatedTranslateTransform =
                new TranslateTransform();

            var animateMove = new DoubleAnimation(triangleFigure.X, 120, new Duration(new TimeSpan(0, 0, 2)));

            // triangleBoard.Children.Add(animateMove);
            // Storyboard.SetTarget(animateMove, triangleFigure.polygon);
            // Storyboard.SetTargetProperty(animateMove,
            // new PropertyPath(TranslateTransform.XProperty));
            DoubleAnimationUsingPath translateXAnimation =
                new DoubleAnimationUsingPath();

           // init pathGeometry
            PathFigure pFigure = new PathFigure();
            pFigure.StartPoint = new Point(10, 100);
            PolyBezierSegment pBezierSegment = new PolyBezierSegment();
            pBezierSegment.Points.Add(new Point(35, 0));
            pBezierSegment.Points.Add(new Point(135, 0));
            pBezierSegment.Points.Add(new Point(160, 100));
            pBezierSegment.Points.Add(new Point(180, 190));
            pBezierSegment.Points.Add(new Point(285, 200));
            pBezierSegment.Points.Add(new Point(310, 100));
            pFigure.Segments.Add(pBezierSegment);

            // Freeze the PathGeometry for performance benefits.
            PathGeometry animationPath = new PathGeometry();
            animationPath.Figures.Add(pFigure);
            animationPath.Freeze();

            translateXAnimation.PathGeometry = animationPath;
            translateXAnimation.Duration = TimeSpan.FromSeconds(5);

            // Set the Source property to X. This makes
            // the animation generate horizontal offset values from
            // the path information.
            translateXAnimation.Source = PathAnimationSource.X;

            // Set the animation to target the X property
            // of the TranslateTransform named "AnimatedTranslateTransform".
            Storyboard.SetTarget(translateXAnimation, triangleFigure.polygon);
            Storyboard.SetTargetProperty(
                translateXAnimation,
                new PropertyPath(TranslateTransform.XProperty));

            translateXAnimation.BeginAnimation(TranslateTransform.XProperty, animateMove);
            this.TriangleBoard.Children.Add(translateXAnimation);
            this.TriangleBoard.Begin();
        }

        private void CreateCircleShape(object sender, RoutedEventArgs e)
        {
            Point pMax = new Point(this.canvasFigures.ActualWidth - 20, this.canvasFigures.ActualHeight - 20);

            CircleFigure circle = new CircleFigure(pMax);
            circle.Draw(this.canvasFigures, this.CirclesTree);
            int x = 0;
            do
            {
                circle.Move(this.canvasFigures);
                ++x;
            }
            while (x < 10);

        }
    }
}
