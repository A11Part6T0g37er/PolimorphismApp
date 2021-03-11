namespace PolimorphismApp
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Shapes;

    internal class CircleFigure : AbstractFigure
    {
        public CircleFigure(Point pMax)
        {
            this.PMax = pMax;
            this.ellipse = new Ellipse() { Height = 40, Width = 40 };
            this.ellipse.Fill = InitBrush();
            Indexer++;

            this.ellipse.Name = "Circle";
        }

        private Canvas internalCanvas;
        private static int Indexer = 0;

        // private Point pMax;
        public Ellipse ellipse { get; private set; }




        public override void Move(Point pMax)
        {


            if (X <= 0 || X >= pMax.X)
                this.Dx = -Dx;
            if (Y <= 0 || X >= pMax.Y)
                Dy = -Dy;

            X += Dx;
            Y += Dy;


        }

        public override void Draw(Canvas canvas, TreeViewItem childTree)
        {
            internalCanvas = canvas;
            X = rd.Next(0, (int)PMax.X);
            Y = rd.Next(0, (int)PMax.Y);
            Canvas.SetLeft(this.ellipse, X);
            Canvas.SetTop(this.ellipse, Y);
            canvas.Children.Add(this.ellipse);

            TreeViewItem ChildItem = new TreeViewItem();
            ChildItem.Header = this.ellipse.Name + " " + Indexer;
            childTree.Items.Add(ChildItem);

        }

        public void Move(Canvas canvas2)
        {

            this.Move2(canvas2);
        }

        // X, Y properties are changed but only final changes are dispalyed
        private void Move2(Canvas canvas2)
        {
            if (X <= 0 || X >= PMax.X)
                Dx = -Dx;
            if (Y <= 0 || X >= PMax.Y)
                Dy = -Dy;

            X += Dx;
            Y -= Dy;

            var left = Canvas.GetLeft(this.ellipse);
            var right = Canvas.GetLeft(this.ellipse);
            var top = Canvas.GetTop(this.ellipse);
            var bottom = Canvas.GetTop(this.ellipse);

            // canvas2.Children.Remove(ellipse);
            Canvas.SetLeft(this.ellipse, X);
            Canvas.SetTop(this.ellipse, Y);

            // canvas2.Children.Add(ellipse);
        }
    }
}