using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Data
{
    public abstract class DataAPI
    {
        public abstract event PropertyChangedEventHandler? PropertyChanged;
        protected abstract void RaisePropertyChanged([CallerMemberName] string propertyName = null);
        public abstract double XCoordinate
        {
            get; set;
        }
        public abstract double YCoordinate
        {
            get; set;
        }
        public abstract int Radius { get; }
        public abstract double NrOfFrames { get; set; }
        public abstract int Diameter { get; set; }
        public abstract double DestinationPlaneX { get; set; }
        public abstract double DestinationPlaneY { get; set; }
        public abstract double Mass { get; set; }
        public abstract PointF Vector { get; set; }
        public abstract void Move();
        public abstract void UpdateMovement(double x, double y, PointF vector, double nrOfFrames);
    }
}
