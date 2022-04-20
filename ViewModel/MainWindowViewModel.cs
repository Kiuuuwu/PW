using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
//using System.Windows.Controls;
using Logic;

namespace ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private BallManager _ballManager;

        public MainWindowViewModel()
        {
            _ballManager = new BallManager();
        }

        public int _numberOfBalls;
        public int NrOfBalls
        {
            get { return _numberOfBalls; }
            set
            {
                if (value != _numberOfBalls)
                {
                    _numberOfBalls = value;
                    OnPropertyChanged("NrOfBalls");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        //void OnClickApplyButton(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < NrOfBalls; i++)
        //    {
        //        _ballManager.CreateBall();
        //    }
        //}
    }
}
