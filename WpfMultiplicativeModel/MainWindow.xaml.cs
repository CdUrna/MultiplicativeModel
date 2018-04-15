using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfMultiplicativeModel;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Globalization;

using WpfMultiplicativeModel.ViewModel;
namespace WpfMultiplicativeModel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LiveChartsSettings liveChartsSettings;
        public service.MultiplicativeModelServices multiplicativeModel;
        //private ZoomingOptions _zoomingMode;
        //private double _maximumXAxis;
        //private double _minimumXAxis;
        //private double _forYAxis;
        public MainWindow()
        {
            InitializeComponent();
            liveChartsSettings = new LiveChartsSettings();
            multiplicativeModel = new service.MultiplicativeModelServices();
            //DataContext = liveChartsSettings; ==!==

            DataContext = new MultiplicativeViewModel();
            //YFormatter = value => value.ToString("R");

            //modifying the series collection will animate and update the chart

            //modifying any series values will also animate and update the chart
            //SeriesCollection[3].Values.Add(5d);

            //DataContext = this;
        }

        //======================================================================================
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //

            //this.liveChartsSettings.SeriesCollection[0].Values = new ChartValues<double> { };
            ////
            //float[] data = new float[DataGrid.Children.Count];

            //for (int i = 0; i < DataGrid.Children.Count; i++)
            //{
            //    UIElement element = DataGrid.Children[i];
            //    if (String.Equals(element.GetType().Name, "TextBox"))
            //    {
            //        TextBox el = (TextBox)element;
            //        data[i] = Convert.ToInt32(el.Text);
            //    }
            //}
            ////this.SeriesCollection[0].Values.Add
            //multiplicativeModel.Algiment(data);

            //TextBlockForCoefficientOfDetermination.Text = multiplicativeModel.coefficientOfDetermination.ToString();
            //TextBoxForForecasting1.Text = multiplicativeModel.predictedElements[0].ToString();
            //TextBoxForForecasting2.Text = multiplicativeModel.predictedElements[1].ToString();
            //TextBoxForForecasting3.Text = multiplicativeModel.predictedElements[2].ToString();

            ////format Table 
            //this.liveChartsSettings.Labels = new string[multiplicativeModel.n + 3];
            //for (int i = 0; i < multiplicativeModel.n; i++)
            //{
            //    this.liveChartsSettings.SeriesCollection[0].Values.Add((double)multiplicativeModel.dataArr[i]);
            //}

            //int indexForDataArr = 0;
            //int indexForPredictedElements = 0;
            //for (int i = 0; i < multiplicativeModel.n + 3; i++)
            //{
            //    this.liveChartsSettings.Labels[i] = (i + 1).ToString();
            //    if (indexForDataArr < 16)
            //    {
            //        this.liveChartsSettings.SeriesCollection[1].Values.Add(double.NaN); //null for first element
            //        indexForDataArr++;
            //    }
            //    else
            //    {
            //        this.liveChartsSettings.SeriesCollection[1].Values.Add((double)multiplicativeModel.predictedElements[indexForPredictedElements]);
            //        if (indexForPredictedElements < 3)
            //        {
            //            indexForPredictedElements++;
            //        }
            //    }
            //}
            //this.liveChartsSettings.SeriesCollection = liveChartsSettings.SeriesCollection;
            //Console.WriteLine("done");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //string[] defData = new string[] { "375", "371", "869", "1015", "357", "471", "992", "1020", "390", "355",
            //"992", "905", "461", "454", "920", "927"};
        }
    }
}
