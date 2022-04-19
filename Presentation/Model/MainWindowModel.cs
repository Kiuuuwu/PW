using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.Model
{
    public class MainWindowModel /*: INotifyPropertyChanged*/
    {
        //public int NrOfBalls = 2;   //te liczbe chce zbindowac bidirectional do textfielda i losowac tyle kulek


        ////string LoadedNumber;
        ////userInputTextBox.DataBindings.Add("Text", LoadedNumber, "");


        //public List<Ball> CreateManyBalls(int NrOfBalls)
        //{
        //    List<Ball> BallList = new List<Ball>();
        //    BallManager manager = new BallManager();

        //    for (int i = 0; i < NrOfBalls; i++)
        //    {
        //        BallList = manager.CreateBall(BallList);
        //    }
        //    return BallList;    // te liste trzeba jakos przekzac wyswietlaniu zeby wyswietlalo kazdy element z listy
        //}
        //protected override void OnStartup(object sender, StartupEventArgs e)
        //{
        //    DisplayRootViewFor<MainWindowViewModel>();
        //}

        //public MainWindow()
        //{
        //    DataContext = this;
        //    INitializeComponent();
        //}




        //public int _numberOfBalls = 2;
        //public int NrOfBalls
        //{
        //    get { return _numberOfBalls; }
        //    set { _numberOfBalls = value; }
        //}
        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
