using System.Collections.ObjectModel;
using CommonHelpers.Common;
using UploadingToWebApi.Uwp.Models;

namespace UploadingToWebApi.Uwp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            PieSeriesData.Add(new ChartDataItem { Value = 20 });
            PieSeriesData.Add(new ChartDataItem { Value = 45 });
            PieSeriesData.Add(new ChartDataItem { Value = 35 });


            OhlcSeriesData.Add(new ChartDataItem { High = 10, Open = 5, Low = 2, Close = 8 });
            OhlcSeriesData.Add(new ChartDataItem { High = 15, Open = 7, Low = 3, Close = 5 });
            OhlcSeriesData.Add(new ChartDataItem { High = 20, Open = 15, Low = 10, Close = 19 });
            OhlcSeriesData.Add(new ChartDataItem { High = 7, Open = 2, Low = 1, Close = 5 });
            OhlcSeriesData.Add(new ChartDataItem { High = 25, Open = 15, Low = 10, Close = 12 });


        }
        
        public ObservableCollection<ChartDataItem> PieSeriesData { get; set; } = new ObservableCollection<ChartDataItem>();

        public ObservableCollection<ChartDataItem> OhlcSeriesData { get; set; } = new ObservableCollection<ChartDataItem>();
    }
}
