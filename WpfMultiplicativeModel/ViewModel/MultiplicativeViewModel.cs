using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

using WpfMultiplicativeModel.service;
using WpfMultiplicativeModel.Model;

namespace WpfMultiplicativeModel.ViewModel
{
    class MultiplicativeViewModel : INotifyPropertyChanged
    {
        private MultiplicativeModelServices multiplicativeModelServices;
        public LiveChartsSettings liveChartsSettings;
        private MultiplicativeModel multiplicativeModel;
        private ObservableCollection<string> data;
        private ComadServices startComand;

        public MultiplicativeViewModel()
        {
            this.multiplicativeModelServices = new MultiplicativeModelServices();
            this.liveChartsSettings = new LiveChartsSettings();
            this.multiplicativeModel = new MultiplicativeModel();
            this.data = new ObservableCollection<string>();
        }


        public float CoefficientOfDetermination
        {
            get
            {
                return multiplicativeModel.CoefficientOfDetermination;
            }
            set
            {
                multiplicativeModel.CoefficientOfDetermination = value;
                OnOnPropertyChanged("CoefficientOfDetermination");
            }
        }
        
        public float[] PredicateDElements
        {
            get
            {
                return multiplicativeModel.PredictedElements;
            }
            set
            {
                multiplicativeModel.PredictedElements = value;
                OnOnPropertyChanged("PredicateDElements");
            }
        }

        public ObservableCollection<string> Data
        {
            get;
            set;
        }
        public ComadServices StartComand
        {
            get
            {
                return startComand ?? (startComand = new ComadServices(obj =>
                {

                }));
            }
        }

        public void StartMultiplicativeModel()
        {

        }
            
        //реализация интерфейса 
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnOnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
