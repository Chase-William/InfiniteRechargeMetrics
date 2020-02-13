using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.PerformancePages;
using System.ComponentModel;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     MVVM for the stage One Page
    /// </summary>
    public class StageOneViewModel : StageViewModelBase
    {
        public int AutoLowPortPoints
        {
            get => Performance.AutoLowPortPoints;
            set
            {
                if (value < 0) return;
                Performance.AutoLowPortPoints = value;
                NotifyPropertyChanged(nameof(AutoLowPortPoints));
            }
        }
        public int AutoUpperPortPoints
        {
            get => Performance.AutoUpperPortPoints;
            set
            {
                if (value < 0) return;
                Performance.AutoUpperPortPoints = value;
                NotifyPropertyChanged(nameof(AutoUpperPortPoints));
            }
        }
        public int AutoSmallPortPoints
        {
            get => Performance.AutoSmallPortPoints;
            set
            {
                if (value < 0) return;
                Performance.AutoSmallPortPoints = value;
                NotifyPropertyChanged(nameof(AutoSmallPortPoints));
            }
        }

        public StageOneViewModel(Performance _performance) : base(_performance)
        {

        }
    }
}
