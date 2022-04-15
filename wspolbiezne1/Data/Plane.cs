using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wspolbiezne1.Data
{
    public class Plane
    {
        public double Width { get; private set; }
        public double Height { get; private set; }

        public Plane (double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
