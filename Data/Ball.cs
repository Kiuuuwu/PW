using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Data
{
    public class Ball : DataAPI, INotifyPropertyChanged
    {
        private Logger _logger;
        private Canvas _canvas = new Canvas(new Point(0, 0), new Point(640, 360));
        public override event PropertyChangedEventHandler? PropertyChanged;
        protected override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private object lockObject = new object();
        private bool _canMove = true;
        private double _xCoordinate;
        private double _yCoordinate;
        public override double XCoordinate
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
        public override double YCoordinate
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
        public int Id { get; init; }
        public override double NrOfFrames { get; set; }
        public override int Diameter { get; set; }
        public override int Radius => Diameter / 2;
        private double _destinationPlaneX;
        public override double DestinationPlaneX
        {
            get => _destinationPlaneX;

            set
            {
                if (value > _canvas.RightDownCorner.X - Diameter)
                {
                    _destinationPlaneX = _canvas.RightDownCorner.X - Diameter;
                }
                else _destinationPlaneX = value;
            }
        }
        private double _destinationPlaneY;
        public override double DestinationPlaneY
        {
            get => _destinationPlaneY;

            set
            {
                if (value > _canvas.RightDownCorner.Y - Diameter)
                {
                    _destinationPlaneY = _canvas.RightDownCorner.Y - Diameter;
                }
                else _destinationPlaneY = value;
            }
        }
        public override double Mass { get; set; }
        public override PointF Vector { get; set; }

        public Ball(int id, double XCoordinate, double YCoordinate, double NrOfFrames, int Diameter, double DestinationPlaneX, double DestinationPlaneY, double Mass, PointF Vector, Logger logger)
        {
            this.Id = id;
            this.Diameter = Diameter;
            this.XCoordinate = XCoordinate;
            this.YCoordinate = YCoordinate;
            this.NrOfFrames = NrOfFrames;
            this.DestinationPlaneX = DestinationPlaneX;
            this.DestinationPlaneY = DestinationPlaneY;
            this.Mass = Mass;
            this.Vector = Vector;
            _logger = logger;
        }

        public override void Move()
        {
            if (!_canMove) return;

            //if ((Vector.X > 0 && XCoordinate + Vector.X > DestinationPlaneX)
            //    || (Vector.X < 0 && XCoordinate + Vector.X < DestinationPlaneX))
            //    XCoordinate = DestinationPlaneX;
            //else
            if (Vector.X > _canvas.LeftUpCorner.X && XCoordinate + Vector.X > _canvas.RightDownCorner.X - Diameter)
                XCoordinate = _canvas.RightDownCorner.X - Diameter;
            else if (Vector.X < _canvas.LeftUpCorner.X && XCoordinate + Vector.X < _canvas.LeftUpCorner.X)
                XCoordinate = _canvas.LeftUpCorner.X;
            else
                XCoordinate += Vector.X;

            //if ((Vector.Y > 0 && YCoordinate + Vector.Y > DestinationPlaneY)
            //    || (Vector.Y < 0 && YCoordinate + Vector.Y < DestinationPlaneY))
            //    YCoordinate = DestinationPlaneY;
            //else
            if (Vector.Y > _canvas.LeftUpCorner.Y && YCoordinate + Vector.Y > _canvas.RightDownCorner.Y - Diameter)
                YCoordinate = _canvas.RightDownCorner.Y - Diameter;
            else if (Vector.Y < _canvas.LeftUpCorner.Y && YCoordinate + Vector.Y < _canvas.LeftUpCorner.Y)
                YCoordinate = _canvas.LeftUpCorner.Y;
            else
                YCoordinate += Vector.Y;

            _logger.SaveLogsToFile(this);
        }
        public override void UpdateMovement(double x, double y, PointF vector, double nrOfFrames)
        {
            _canMove = false;
            // sekcja krytyczna - tylko 1 watek na raz moze wykonac te logike
            lock (lockObject)
            {
                DestinationPlaneX = x;
                DestinationPlaneY = y;
                Vector = vector;
                NrOfFrames = nrOfFrames;
            }
            _canMove = true;
            _logger.SaveLogsToFile(this);
        }
    }
}
