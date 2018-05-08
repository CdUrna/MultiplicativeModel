using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMultiplicativeModel.service
{
    public class MultiplicativeModelServices
    {
        public float[] dataArr;
        public float averageData;
        public float[] movingAverage;
        public float[] centeredMovingAverage;
        public float[] Evaluation;
        private SeasonalComponentEstimates seasonalComponentEstimates;
        private float[] componentT;
        public float[] TS;
        private float[] E;
        private float sumE;
        private float sumYTS;

        public int n;
        private float[] Y;
        private float sumY;
        private float sumTY;
        public float sumN, sumPowN;
        public float a;
        public float b;

        //reults data
        public float coefficientOfDetermination;
        public float[] predictedElements;

        public MultiplicativeModelServices()
        {
            this.seasonalComponentEstimates = new SeasonalComponentEstimates();
        }

        public void Algiment(float[] data, int countPredicate = 3)
        {
            this.dataArr = new float[data.Length];
            this.n = data.Length;
            float sumOfData = 0;
            for (int i = 0; i < data.Length; i++)
            {
                this.dataArr[i] = data[i];
                sumOfData += data[i];
            }
            this.averageData = sumOfData / (this.n);

            movingAverage = new float[data.Length -3];
            centeredMovingAverage = new float[data.Length -4];
            Evaluation = new float[data.Length - 4];

            this.getmMovingAverage(data, ref movingAverage);
            this.getCenteredMovingAverage(movingAverage, ref centeredMovingAverage);
            this.getEvaluation(data, ref Evaluation);
            this.getSeasonEvaluation();
            this.seasonalComponentEstimates.getAdjustedSeasonalComponent();
            this.getSumYelements(data);


            this.getComponentTAndE();
            this.getSumYTS();

            this.getCofficientOfDetermination();
            this.Forecasting(countPredicate);
        }

        private void getmMovingAverage(float[] data, ref float[] results)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (i == 0 || i + 2 >= data.Length)
                {
                    continue;
                }
                results[i - 1] = (float)(data[i - 1] + data[i] + data[i + 1] + data[i + 2]) / 3;
            }
        }

        private void getCenteredMovingAverage(float[] data, ref float[] results)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (i == 0) { continue; }
                results[i - 1] = (float)(data[i - 1] + data[i]) / 2;
            }
        }

        private void getEvaluation(float[] data, ref float[] results)
        {
            for (int i = 0; i < this.centeredMovingAverage.Length; i++)
            {
                results[i] = (float)data[i + 2] / this.centeredMovingAverage[i];
            }
        }

        private void getSeasonEvaluation()
        {
            int row = (this.Evaluation.Length / 4) + 1;
            float?[,] matrix = new float?[row, 4]; //замена целых на значения из подобьекта
            int evaluateIndex = 0;
            for (int i = 0; i < row; i++) // проверка
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == 0 && j < 2)
                    {
                        matrix[i, j] = null;
                        continue;
                    };
                    if (this.Evaluation.Length > evaluateIndex)
                    {
                        matrix[i, j] = this.Evaluation[evaluateIndex];
                    }
                    else if (Evaluation.Length == evaluateIndex)
                    {
                        matrix[i, j] = null;
                    }
                    else
                    {
                        matrix[i, j] = null;
                    }
                    evaluateIndex += 1;
                }
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrix[i, j].HasValue)
                    {
                        Console.Write(matrix[i, j].ToString() + " | ");
                    }
                    else
                    {
                        Console.Write("null | ");
                    }
                }
                Console.WriteLine();
            }

            for (int i = 0; i < this.seasonalComponentEstimates.totalForThePeriod.Length; i++)
            {
                int check = 0;
                float total = 0;
                for (int r = 0; r < row; r++)
                {
                    if (matrix[r, i].HasValue)
                    {
                        check += 1;
                        total += matrix[r, i].Value;
                    }
                }
                this.seasonalComponentEstimates.totalForThePeriod[i] = total;
                this.seasonalComponentEstimates.averageSeasonalComponentEstimates[i] = (float)total / check;
                check = 0;
                total = 0;
            }
        }

        private void getSumYelements(float[] data) // реализация функционала метода МНК
        {
            this.sumN = 0;
            this.sumPowN = 0;
            this.getYEl(data);
            this.getSumTY();
            this.getAnadB();
        }

        private void getYEl(float[] data)
        {
            this.Y = new float[data.Length];
            int index = 0;
            this.sumY = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sumN += i+1;
                sumPowN += (float)Math.Pow(i+1, 2);
                if (index < 4)
                {
                    this.Y[i] = data[i] / this.seasonalComponentEstimates.djustedSeasonalComponent[index];
                    this.sumY += this.Y[i];
                    index++;
                }
                else
                {
                    index = 0;
                    this.Y[i] = data[i] / this.seasonalComponentEstimates.djustedSeasonalComponent[index]; //delete Y
                    this.sumY += this.Y[i];
                    index++;
                }
                //Console.WriteLine(this.Y[i]);
            }
        }

        private void getSumTY()
        {
            this.sumTY = 0;
            for (int i = 0; i < this.Y.Length; i++)
            {
                this.sumTY += (i+1) * this.Y[i];
            }
        }

        private void getAnadB() //метод наименших квадратов (агрументы уже получены)
        {
            this.a = ((sumN * sumY) - (n * sumTY))/((float)Math.Pow(sumN, 2)-(n*sumPowN));
            this.b = ((sumN * sumTY) - (sumPowN * sumY)) / ((float)Math.Pow(sumN, 2) - (n * sumPowN));
        }

        private void getComponentTAndE()
        {
            this.componentT = new float[dataArr.Length];
            this.sumE = 0;
            this.E = new float[this.n];
            float prev = 0;

            for (int i = 0; i < componentT.Length; i++)
            {
                if (i == 0)
                {
                    prev = b + a;
                    this.componentT[i] = b + a;
                    continue;
                }
                prev += a;
                this.componentT[i] = prev;
            }

            TS = new float[n];
            //get E
            int indexForS = 0;
            for (int i = 0; i < this.n; i++)
            {
                if (indexForS < 4)
                {
                    TS[i] = this.componentT[i] * this.seasonalComponentEstimates.djustedSeasonalComponent[indexForS];
                    this.E[i] = this.dataArr[i] / TS[i];
                    indexForS++;
                }
                else
                {
                    indexForS = 0;
                    TS[i] = this.componentT[i] * this.seasonalComponentEstimates.djustedSeasonalComponent[indexForS];
                    this.E[i] = this.dataArr[i] / TS[i];
                    indexForS++;
                }
                this.sumE += this.E[i];
            }
        }

        private void getSumYTS()
        {
            this.sumYTS = 0;
            float tempElement = 0;
            for (int i = 0; i < this.n; i++)
            {
                tempElement = this.dataArr[i] - this.TS[i];
                this.sumYTS += (float)Math.Pow(tempElement, 2);
            }
        }

        private void getCofficientOfDetermination()
        {
            float sum = 0;

            for (int i = 0; i < this.n; i++)
            {
                sum += (float)Math.Pow(this.dataArr[i] - this.averageData, 2);
            }

            this.coefficientOfDetermination = (float)Math.Round(1 - (this.sumYTS / sum), 2);
        }

        private void Forecasting(int countPredicate = 3)
        {
            this.predictedElements = new float[countPredicate];
            int indexForSeasonComponent = this.n%4;

            for (int i = 0; i < countPredicate; i++)
            {
                if (indexForSeasonComponent < 4)
                {
                    this.predictedElements[i] = (this.b + (this.a * (n + i + 1))) + this.seasonalComponentEstimates.djustedSeasonalComponent[indexForSeasonComponent];
                    indexForSeasonComponent++;
                }
                else
                {
                    indexForSeasonComponent = 0;
                    this.predictedElements[i] = (this.b + (this.a * (n + i + 1))) + this.seasonalComponentEstimates.djustedSeasonalComponent[indexForSeasonComponent];
                    indexForSeasonComponent++;
                }
            }
        }
    }

    internal class SeasonalComponentEstimates
    {
        public float[] totalForThePeriod;
        public float[] averageSeasonalComponentEstimates;
        public float correlationАactor;
        public float[] djustedSeasonalComponent; //S

        public SeasonalComponentEstimates()
        {
            this.totalForThePeriod = new float[4];
            this.averageSeasonalComponentEstimates = new float[4];
            this.djustedSeasonalComponent = new float[4];
        }

        public void getAdjustedSeasonalComponent()
        {
            float sum = 0;
            for (int i = 0; i < this.averageSeasonalComponentEstimates.Length; i++)
            {
                sum += this.averageSeasonalComponentEstimates[i];
            }

            this.correlationАactor = this.averageSeasonalComponentEstimates.Length / sum;

            for (int i = 0; i < this.djustedSeasonalComponent.Length; i++)
            {
                this.djustedSeasonalComponent[i] = this.averageSeasonalComponentEstimates[i] * this.correlationАactor;
            }
        }
    }
}
