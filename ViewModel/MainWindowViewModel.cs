using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
       

        private BallManager _ballManager;

        public ICommand Apply { get; set; }
        public ICommand Start { get; set; }
        public ObservableCollection<Ball> ObsCollBall => _ballManager.CurrentBalls;

        public MainWindowViewModel()
        {

            _ballManager = new BallManager();
            Apply = new RelayCommand(() => _ballManager.CreateBall(NrOfBalls));
            Start = new RelayCommand(() => _ballManager.BallsMovement());
        }

       

        private int _numberOfBalls;
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

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
