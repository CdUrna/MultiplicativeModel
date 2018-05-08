using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace WpfMultiplicativeModel.ViewModel
{
    public class LiveChartsSettings : Window, INotifyPropertyChanged
    {
        private ZoomingOptions _zoomingMode;
        private double _maximumXAxis;
        private double _minimumXAxis;

        public LiveChartsSettings()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Начальные данные",
                    Values = new ChartValues<double>{}
                },
                new LineSeries
                {
                    Title = "спрогнозированые данные",
                    Values = new ChartValues<double>{}
                },
                new LineSeries
                {
                    Title = "уровни ряда",
                    Values = new ChartValues<double>{},
                    PointForeground = new SolidColorBrush(Colors.Firebrick),
                    
                }
            };
            ZoomingMode = ZoomingOptions.X;
            MaxForXAxis = 20;
            MinForXAxis = 0;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public ZoomingOptions ZoomingMode
        {
            get { return _zoomingMode; }
            set
            {
                _zoomingMode = value;
                OnPropertyChanged("ZoomingMode");
            }
        }

        public double MaxForXAxis
        {
            get
            {
                return _maximumXAxis;
            }

            set
            {
                _maximumXAxis = value;
                OnPropertyChanged("MaxForXAxis");
            }
        }

        public double MinForXAxis
        {
            get
            {
                return _minimumXAxis;
            }

            set
            {
                _minimumXAxis = value;
                OnPropertyChanged("MinForXAxis");
            }
        }

        //------------------------------------
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
