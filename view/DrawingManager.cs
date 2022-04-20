using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace View
{
    internal class DrawingManager
    {
        public void DrawEllipse(Canvas ballsPlane)
        {
            // Create a red Ellipse.
            Ellipse myEllipse = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Colors.Black;
            myEllipse.Fill = mySolidColorBrush;
            myEllipse.StrokeThickness = 2;
            myEllipse.Stroke = Brushes.Black;

            // Set the width and height of the Ellipse.
            myEllipse.Width = 100;
            myEllipse.Height = 100;

            // How to set center of ellipse???

            ballsPlane.Children.Add(myEllipse);
        }

    }
}
