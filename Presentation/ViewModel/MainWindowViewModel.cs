using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        // tutaj ma byb binding i INotifyPropertyChanged
        //public string _numberOfBalls = "2";
        //public string NrOfBalls
        //{
        //    get { return _numberOfBalls; }
        //    set { _numberOfBalls = value; }
        //}



        public string _numberOfBalls = "2";
        public string NrOfBalls
        {
            get { return _numberOfBalls; }
            set {
                if (value != _numberOfBalls)
                {
                    _numberOfBalls = value;
                    OnPropertyChanged("NrOfBalls");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

    }
}
