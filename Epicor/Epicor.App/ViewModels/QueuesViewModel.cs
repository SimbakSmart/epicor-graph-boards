

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Epicor.Core.Models;
using Epicor.Infraestructure.Helpers;
using Epicor.Infraestructure.Services;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.Extensions;


namespace Epicor.App.ViewModels
{
    public partial class QueuesViewModel : ObservableObject
    {
      

        private QueueServices qs = null;

        [ObservableProperty]
        private string _starDateMessage;

        [ObservableProperty]
        private string _endDateMessage;

        [ObservableProperty]
        private bool _isActive;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SendFiltersCommand))]
        private bool _isLoading;

        #region FILTERS 
        private DateTime? _startDate;
        private DateTime? _endDate;
        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                SetProperty(ref _startDate, value);

                if (value is DateTime?)
                {
                    StarDateMessage = string.Empty;
                }
                else
                {
                    IsActive = false;
                    StarDateMessage = "La fecha inicio es requerida";
                }
            }
        }
        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                SetProperty(ref _endDate, value);

                if (value is DateTime?)
                {

                    if (_endDate < _startDate)
                    {
                        EndDateMessage = "La fecha final no puede ser menor";
                    }
                    else
                    {
                        EndDateMessage = string.Empty;
                        IsActive = true;
                    }
                }
                else
                {
                    IsActive = false;
                    EndDateMessage = "La fecha final es requerida";
                }
            }
        } 
        #endregion

        #region TOTALES 
        [ObservableProperty]
        private List<Queues> _total;

        [ObservableProperty]
        private int _granTotal;

        [ObservableProperty]
        private int _totalOpen;

        [ObservableProperty]
        private int _totalClosed; 
        #endregion

        #region BAR GRAPH RESPONSALES 
        [ObservableProperty]
        private ColumnSeries<double> _userSeriesBar;

        [ObservableProperty]
        private List<Queues> _listBar = null;

        [ObservableProperty]
        public ISeries[] _seriesBar;

        [ObservableProperty]
        public Axis[] _xAxesBar;
        #endregion

        #region  TABLE RANGE DAYS 
        [ObservableProperty]
        private List<Queues> _listByRange;
        #endregion

        #region GRAPH STATUS        
        [ObservableProperty]
        private ColumnSeries<double> _userSeriesStatus;

        [ObservableProperty]
        private List<Queues> _listStatus = null;

        [ObservableProperty]
        public ISeries[] _seriesStatus;

        [ObservableProperty]
        public Axis[] _xAxesStatus;
        #endregion

        #region PIE GRAPH URGENCY   
        [ObservableProperty]
        private List<Queues> _listUrgency = null;

        [ObservableProperty]
        private IEnumerable<ISeries> _seriesUrgency; 
        #endregion

        public static QueuesViewModel instance = null;
        public QueuesViewModel()
        {
            instance = this;
            IsActive = false;
            IsLoading =false;
            StarDateMessage = "La fecha inicio es requerida";
            EndDateMessage = "La fecha final es requerida";
            qs = new QueueServices();
            Task.Run(async () => await LoadDataAsync());
        }

        public static QueuesViewModel GetIntance()
        {
            if(instance == null)   
               return new QueuesViewModel();

            return instance;

        }
   
        public  async Task LoadDataAsync()
        {
            qs = new QueueServices();
            IsLoading = true;
            await GetTotalsAsync();
            await BarGraphByResponsableAsync();
            await GetTotalsByRangeAsync();
            await BarGraphBySatusAsync();
            await UrgencyPieChartAsync();
            await qs.DisposeAsync();
            IsLoading = false;
        }

        private async Task GetTotalsAsync(FiltersParams filters = null)
        {
            if (filters != null)
            {
                Total = await qs.GetTotalsAsync(filters);
            }
            else
            {
                Total = await qs.GetTotalsAsync();
            }

            GranTotal = Total.Select(t => t.Total).FirstOrDefault();
            TotalOpen = Total.Select(t => t.TotalOpen).FirstOrDefault();
            TotalClosed = Total.Select(t => t.TotalClosed).FirstOrDefault();
        }

        private async Task BarGraphByResponsableAsync(FiltersParams filters = null)
        {
            ListBar?.Clear();

            if (filters != null)
            {
                ListBar = await qs.GetTotalsByResponsableAsync(filters);
            }
            else
            {
                ListBar = await qs.GetTotalsByResponsableAsync();
            }


            UserSeriesBar = new ColumnSeries<double>()
            {
                Name = "Reportes Activos",
                Values = ListBar.Select(q => (double)q.Total).ToList(),
                Padding = 1,
                MaxBarWidth = double.PositiveInfinity,
                Fill = new SolidColorPaint(new SKColor(25, 118, 210, 255)),


            };
            Axis _axis = new Axis()
            {
                Labels = ListBar.Select(q => q.Name).ToList(),
                TextSize = 12,
                LabelsAlignment = LiveChartsCore.Drawing.Align.Start,
                IsVisible = true,
                LabelsRotation = -90,
                Position = AxisPosition.Start,
                Padding = new LiveChartsCore.Drawing.Padding(0)
            };

            SeriesBar = new ISeries[] { UserSeriesBar };
            XAxesBar = new Axis[] { _axis };
        }

        private async Task GetTotalsByRangeAsync(FiltersParams filters = null)
        {
            if (filters != null)
            {
                ListByRange = await qs.GetTotalsByRangeDayseAsync(filters);
            }
            else
            {
                ListByRange = await qs.GetTotalsByRangeDayseAsync();
            }

        }

        private async Task BarGraphBySatusAsync(FiltersParams filters = null)
        {
            ListStatus?.Clear();
            if (filters != null)
            {
                ListStatus = await qs.GetTotalsByStatuseAsync(filters);
            }
            else
            {
                ListStatus = await qs.GetTotalsByStatuseAsync();
            }


            UserSeriesStatus = new ColumnSeries<double>()
            {
                Name = "Reportes Activos",
                Values = ListStatus.Select(q => (double)q.Total).ToList(),
                Padding = 1,
                MaxBarWidth = double.PositiveInfinity,
                // Fill = new SolidColorPaint(new SKColor(235, 95, 2, 255)),
                Fill = new SolidColorPaint(new SKColor(25, 118, 210, 255)),
            };


            Axis _axis = new Axis()
            {
                Labels = ListStatus.Select(q => q.Status).ToList(),
                TextSize = 12,
                LabelsAlignment = LiveChartsCore.Drawing.Align.Start,
                IsVisible = true,
                LabelsRotation = -90,
                Position = AxisPosition.Start,
                Padding = new LiveChartsCore.Drawing.Padding(0)
            };

            SeriesStatus = new ISeries[] { UserSeriesStatus };
            XAxesStatus = new Axis[] { _axis };

        }

        private async Task UrgencyPieChartAsync()
        {
            int _index = 0;
            ListUrgency = await qs.GetTotalsByUrgencyAsync();
            string[] _urgencyArray = ListUrgency.Select(q => q.Urgency).ToArray();
            double[] _totalUrgencyArray = ListUrgency.Select(q => (double)q.Total).ToArray();

            SeriesUrgency = _totalUrgencyArray.AsPieSeries((value, series) =>
            {
                series.Name = _urgencyArray[_index++ % _urgencyArray.Length] + " " + value.ToString();
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle;
                series.DataLabelsSize = 20;
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(0, 0, 0));
                series.Values = new List<double>() { value };

            });
        }


        [RelayCommand]
        private async Task SendFiltersAsync()
        {
            IsLoading = true;
            try
            {
                if (StartDate.HasValue && EndDate.HasValue)
                {
                    var filters = new FiltersParams.FiltersParamsBuilder()
                                     .WithStartDate(StartDate.Value)
                                     .WithEndDate(EndDate.Value)
                                     .Build();


                    await GetTotalsAsync(filters);
                    await BarGraphByResponsableAsync(filters);
                    await GetTotalsByRangeAsync(filters);
                    await BarGraphBySatusAsync(filters);
                }
            }
            catch
            {

            }finally
            {
                IsLoading = false;
            }

        }
        
        [RelayCommand]
        private async Task RefreshAsync()
        {
            OnPropertyChanged(nameof(IsLoading));
            await LoadDataAsync();
            StartDate = null;
            EndDate = null;
        }



    }
}
