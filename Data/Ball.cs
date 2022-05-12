using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Data
{
    public class Ball : DataAPI, INotifyPropertyChanged
    {
        public override event PropertyChangedEventHandler? PropertyChanged;
        protected override /*virtual*/ void RaisePropertyChanged([CallerMemberName] string propertyName = null) // tu bylo virtual ale w api nie dziala takto
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
        //public int Id { get; init; }
        public override double NrOfFrames { get; set; }
        public override int Diameter { get; set; }
        public override int Radius => Diameter / 2;
        private double _destinationPlaneX;
        public override double DestinationPlaneX
        {
            get => _destinationPlaneX;

            set
            {
                if (value > 640 - Diameter)
                {
                    _destinationPlaneX = 640 - Diameter;
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
                if (value > 360 - Diameter)
                {
                    _destinationPlaneY = 360 - Diameter;
                }
                else _destinationPlaneY = value;
            }
        }
        public override double Mass { get; set; }
        public override PointF Vector { get; set; }

        public Ball(/*int id, */double XCoordinate, double YCoordinate, double NrOfFrames, int Diameter, double DestinationPlaneX, double DestinationPlaneY, double Mass, PointF Vector)
        {
            //this.Id = id;
            this.Diameter = Diameter;
            this.XCoordinate = XCoordinate;
            this.YCoordinate = YCoordinate;
            this.NrOfFrames = NrOfFrames;
            this.DestinationPlaneX = DestinationPlaneX;
            this.DestinationPlaneY = DestinationPlaneY;
            this.Mass = Mass;
            this.Vector = Vector;
        }

        public override void Move()
        {
            if (!_canMove) return;

            //if ((Vector.X > 0 && XCoordinate + Vector.X > DestinationPlaneX)
            //    || (Vector.X < 0 && XCoordinate + Vector.X < DestinationPlaneX))
            //    XCoordinate = DestinationPlaneX;
            //else
            if (Vector.X > 0 && XCoordinate + Vector.X > 640 - Diameter)    //todo: zmienic tutaj na canvas.punkt
                XCoordinate = 640 - Diameter;
            else if (Vector.X < 0 && XCoordinate + Vector.X < 0)
                XCoordinate = 0;
            else
                XCoordinate += Vector.X;

            //if ((Vector.Y > 0 && YCoordinate + Vector.Y > DestinationPlaneY)
            //    || (Vector.Y < 0 && YCoordinate + Vector.Y < DestinationPlaneY))
            //    YCoordinate = DestinationPlaneY;
            //else
            if (Vector.Y > 0 && YCoordinate + Vector.Y > 360 - Diameter)
                YCoordinate = 360 - Diameter;
            else if (Vector.Y < 0 && YCoordinate + Vector.Y < 0)
                YCoordinate = 0;
            else
                YCoordinate += Vector.Y;

            //Speed = (int)(duration / nrOfFrames * 100);
        }

        //public string Details => $"Ball Id: {Id}\nBall Radius: {Radius}\nBall X,Y: {XCoordinate}, {YCoordinate}\nDestination X, Y: {DestinationPlaneX}, {DestinationPlaneY}\nVector X,Y: {Vector.X}, {Vector.Y}\n";

        public override void UpdateMovement(double x, double y, PointF vector, double nrOfFrames)
        {
            _canMove = false;
            //var previousDestX = DestinationPlaneX;
            //var previousDestY = DestinationPlaneY;
            //var previousVector = Vector;

            // sekcja krytyczna - tylko 1 watek na raz moze wykonac te logike
            lock (lockObject)
            {
                DestinationPlaneX = x;
                DestinationPlaneY = y;
                Vector = vector;
                NrOfFrames = nrOfFrames;
                //Console.WriteLine($"MOVEMENT UPDATED for Ball with id {Id}:\n" +
                //$"destination X,Y: {previousDestX}, {previousDestY} => {DestinationPlaneX}, {DestinationPlaneY}\n" +
                //$"Vector X,Y: {previousVector.X}, {previousVector.Y} => {vector.X}, {vector.Y}\n");
            }
            _canMove = true;
        }
    }
}
