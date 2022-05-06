using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ball : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private double _xCoordinate;
        private double _yCoordinate;
        public double XCoordinate
        {
            get
            {
                return _xCoordinate;
            }
            set
            {
                _xCoordinate = value;
                RaisePropertyChanged("XCoordinate");
            }
        }
        public double YCoordinate
        {
            get
            {
                return _yCoordinate;
            }
            set
            {
                _yCoordinate = value;
                RaisePropertyChanged("YCoordinate");
            }
        }
        public double Speed { get; private set; }
        public double Radius { get; private set; }
        public double DestinationPlaneX { get; set; }
        public double DestinationPlaneY { get; set; }

        public Ball(double XCoordinate, double YCoordinate, double Speed, double Radius, double DestinationPlaneX, double DestinationPlaneY)
        {
            this.Radius = Radius;
            this.XCoordinate = XCoordinate;
            this.YCoordinate = YCoordinate;
            this.Speed = Speed;
            this.DestinationPlaneX = DestinationPlaneX;
            this.DestinationPlaneY = DestinationPlaneY;
        }

    }

}
