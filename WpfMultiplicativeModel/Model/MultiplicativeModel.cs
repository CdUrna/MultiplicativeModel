using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfMultiplicativeModel.Model
{
    class MultiplicativeModel
    {
        private float[] dataArray;
        private float[] predictedElements;
        private float coefficientOfDetermination;

        public MultiplicativeModel()
        {

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

        public float[] PredictedElements { get; set; }
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
}
