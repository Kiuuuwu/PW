using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public class BallModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double XCoordinate { get; private set; }
        public double YCoordinate { get; private set; }
        public double Diameter { get; private set; }

        public BallModel(double x, double y, double d)
        {
            XCoordinate = x;
            YCoordinate = y;
            Diameter = d;
        }

        public void Update(double x, double y, double d)
        {
            XCoordinate = x;
            RaisePropertyChanged(nameof(XCoordinate));
            YCoordinate = y;
            RaisePropertyChanged(nameof(YCoordinate));
            Diameter = d;
            RaisePropertyChanged(nameof(Diameter));
        }
    }
}
