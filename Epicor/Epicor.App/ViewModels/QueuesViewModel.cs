

using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;


namespace Epicor.App.ViewModels
{
    public partial class QueuesViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _starDateMessage;

        [ObservableProperty]
        private string _endDateMessage;

        [ObservableProperty]
        private bool _isActive;

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
                   
                    if(_endDate < _startDate)
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


        public QueuesViewModel()
        {
            IsActive = false;
            StarDateMessage = "La fecha inicio es requerida";
            EndDateMessage = "La fecha final es requerida";
        }
    }
}
