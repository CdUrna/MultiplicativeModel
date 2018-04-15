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
using LiveCharts;

namespace WpfMultiplicativeModel.ViewModel
{
    class MultiplicativeViewModel : INotifyPropertyChanged
    {
        private MultiplicativeModelServices multiplicativeModelServices;
        private LiveChartsSettings liveChartsSettings; //модель для найстройки livexharts
        private MultiplicativeModel multiplicativeModel;
        public ObservableCollection<DataElement> Data { get; set; }
        public ObservableCollection<ResultDataElement> ResultsData {get; set;}
        private ComadServices startComand;
        private ComadServices defArg;

        public MultiplicativeViewModel()
        {
            this.multiplicativeModelServices = new MultiplicativeModelServices();
            this.liveChartsSettings = new LiveChartsSettings();
            this.multiplicativeModel = new MultiplicativeModel();
            this.Data = new ObservableCollection<DataElement>();
            this.ResultsData = new ObservableCollection<ResultDataElement>();
            for (int i = 0; i < 16; i++)
            {
                this.Data.Add(new DataElement((i).ToString()));
            }

            for (int i = 0; i < 3; i++)
            {
                this.ResultsData.Add(new ResultDataElement(i+1));
            }
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
                OnPropertyChanged("CoefficientOfDetermination");
            }
        }
        
        public float[] PredicatedElements
        {
            get
            {
                return multiplicativeModel.PredictedElements;
            }
            set
            {
                multiplicativeModel.PredictedElements = value;
                OnPropertyChanged("PredicateDElements");
            }
        }


        public ComadServices StartComand //comand of button
        {
            get
            {
                return startComand ?? (startComand = new ComadServices(obj =>
                {
                    float[] dtata = this.convertStringCollectionInFloatArray(this.Data);
                    multiplicativeModelServices.Algiment(dtata);
                    this.CoefficientOfDetermination = multiplicativeModelServices.coefficientOfDetermination;

                    for (int i = 0; i < 3; i++)
                    {
                        this.ResultsData[i].SeasonalComponentValue = this.multiplicativeModelServices.predictedElements[i].ToString();
                    }

                    //tableFormat
                    this.Labels = new string[multiplicativeModelServices.n + 3];
                    for (int i = 0; i < multiplicativeModelServices.n; i++)
                    {
                        this.SeriesCollection[0].Values.Add((double)multiplicativeModelServices.dataArr[i]);
                    }
                    int indexForDataArr = 0;
                    int indexForPredictedElements = 0;
                    for (int i = 0; i < multiplicativeModelServices.n + 3; i++)
                    {
                        this.Labels[i] = (i + 1).ToString();
                        if (indexForDataArr < 16)
                        {
                            this.SeriesCollection[1].Values.Add(double.NaN); //null for first element
                            indexForDataArr++;
                        }
                        else
                        {
                            this.SeriesCollection[1].Values.Add((double)multiplicativeModelServices.predictedElements[indexForPredictedElements]);
                            if (indexForPredictedElements < 3)
                            {
                                indexForPredictedElements++;
                            }
                        }
                    }
                    //this.SeriesCollection = liveChartsSettings.SeriesCollection;
                }));
            }
        }

        public ComadServices DefArg //comanad of defaoult btn
        {
            get
            {
                return defArg ?? (defArg = new ComadServices(obj =>
                {
                    string[] defData = new string[] { "375", "371", "869", "1015", "357", "471", "992", "1020", "390", "355",
            "992", "905", "461", "454", "920", "927"};
                    for (int i = 0; i < Data.Count; i++)
                    {
                        Data[i].Value = defData[i];
                    }
                }));
            }
        }

       //property for liveCharts
       public SeriesCollection SeriesCollection
        {
            get
            {
                return this.liveChartsSettings.SeriesCollection;
            }
            set
            {
                this.liveChartsSettings.SeriesCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public string[] Labels
        {
            get
            {
                return this.liveChartsSettings.Labels;
            }
            set
            {
                this.liveChartsSettings.Labels = value;
                OnPropertyChanged("Labels");
            }
        }

        public ZoomingOptions ZoomingMode
        {
            get
            {
                return this.liveChartsSettings.ZoomingMode;
            }
            set
            {
                this.liveChartsSettings.ZoomingMode = value;
                OnPropertyChanged("ZoomingMode");
            }
        }

        public double MaxForXAxis
        {
            get
            {
                return this.liveChartsSettings.MaxForXAxis;
            }
            set
            {
                this.liveChartsSettings.MaxForXAxis = value;
                OnPropertyChanged("MaxForXAxis");
            }
        }

        public double MinForXAxis
        {
            get
            {
                return this.liveChartsSettings.MinForXAxis;
            }
            set
            {
                this.liveChartsSettings.MinForXAxis = value;
                OnPropertyChanged("MinForXAxis");
            }
        }

        //converter 
        private float[] convertStringCollectionInFloatArray(IEnumerable<DataElement> collection)
        {
            float[] resultArray = new float[collection.Count()];
            int indexer = 0;
            foreach (var item in collection)
            {
                resultArray[indexer] = Convert.ToSingle(item.Value);
                indexer++;
            }
            return resultArray;
        }
            
        //реализация интерфейса 
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
