using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Data;

namespace ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {


        private LogicAPI _logicAPI;

        public ICommand Apply { get; set; }
        public ICommand Start { get; set; }
        public ObservableCollection<DataAPI> ObsCollBall => _logicAPI.getCollection();

        public MainWindowViewModel()
        {

            _logicAPI = LogicAPI.CreateAPI();
            Apply = new RelayCommand(async () => await _logicAPI.CreateBall(NrOfBalls));
            Start = new RelayCommand(async () => await _logicAPI.BallsMovement());
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
