using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;
using System.Drawing;
using System.Windows;
using Data;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
       

        private BallManager _ballManager;
        //public ICommand Apply => new RelayCommand(() => _ballManager.BallsMovement(NrOfBalls));
        
        public ICommand Apply { get; set; }
        public ICommand Start { get; set; }
        public ObservableCollection<Ball> ObsCollBall => _ballManager.CurrentBalls;

        public MainWindowViewModel()
        {

            _ballManager = new BallManager();
            Apply = new RelayCommand(() => _ballManager.CreateBall(NrOfBalls));
            Start = new RelayCommand(() => _ballManager.BallsMovement(NrOfBalls));
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

        public void DrawBalls()
        {
            //DrawingManager drawingManager = new DrawingManager();
            //foreach(Ball ball in _ballManager.CurrentBalls)
            //{
            //    DrawEllipse();
            //}
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //void OnClickApplyButton(object sender, EventArgs e)
        //{
        //    _ballManager.CreateBall();
        //}

        //void CreateBall()
        //{
        //    _ballManager.CreateBall();
        //}

        //void BallsMovement()
        //{
        //    _ballManager.BallsMovement();
        //}


        //public class ApplyEvent : ICommand
        //{
        //    private MainWindowViewModel _mainWIndowViewModel;
        //    public ApplyEvent(MainWindowViewModel mainWIndowViewModel)
        //    {
        //        _mainWIndowViewModel = mainWIndowViewModel;
        //    }
        //    public event EventHandler? CanExecuteChanged
        //    {
        //        add { CommandManager.RequerySuggested += value; }
        //        remove { CommandManager.RequerySuggested -= value; }
        //    }
        //    public bool CanExecute(object? parameter)
        //    {
        //        return true;
        //    }
        //    public void Execute(object? parameter)
        //    {
        //        _mainWIndowViewModel.ObsCollBall.Clear();
        //        for (int i = 0; i < _mainWIndowViewModel.NrOfBalls; i++)
        //        {
        //            _mainWIndowViewModel.CreateBall();
        //        }
        //        _mainWIndowViewModel.BallsMovement();
        //    }
        //}
    }
}
