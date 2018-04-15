using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfMultiplicativeModel.Model
{   //periodNumber == predictedElements[n]
    class MultiplicativeModel
    {
        private float[] dataArray;
        private float[] predictedElements;
        private float coefficientOfDetermination;

        public MultiplicativeModel()
        {
            predictedElements = new float[3];
        }

        public float[] DataArray
        {
            get
            {
                return this.dataArray;
            }
            set
            {
                this.dataArray = value;
            }
        }

        public float CoefficientOfDetermination
        {
            get;
            set;
        }

        public float[] PredictedElements
        {
            get
            {
                return this.predictedElements;
            }
            set
            {
                this.predictedElements = value;
            }
        }
    }

    public class SeasonalComponentEstimates
    {
        public float[] totalForThePeriod;
        public float[] averageSeasonalComponentEstimates;
        public SeasonalComponentEstimates()
        {
            this.totalForThePeriod = new float[4];
            this.averageSeasonalComponentEstimates = new float[4];
        }
    }

    class DataElement: INotifyPropertyChanged
    {
        private string value;
        private string tabIndex;

        public DataElement(string index)
        {
            this.TabIndex = index;
        }

        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        public string TabIndex
        {
            get
            {
                return this.tabIndex;
            }
            set
            {
                this.tabIndex = value;
                OnPropertyChanged("TabIndex");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }

    class ResultDataElement : INotifyPropertyChanged
    {
        private string periodNumber; 
        private string seasonalComponentValue;

        public ResultDataElement(int number, float value=0)
        {
            this.periodNumber = Convert.ToString(number);
            this.seasonalComponentValue = Convert.ToString(value);
        }

        public string PeriodNumber
        {
            get
            {
                return this.periodNumber;
            }
            set
            {
                this.seasonalComponentValue = value;
            }
        }

        public string SeasonalComponentValue
        {
            get
            {
                return this.seasonalComponentValue;
            }
            set
            {
                this.seasonalComponentValue = value;
                OnPropertyChanged("SeasonalComponentValue");
            }
        }

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
