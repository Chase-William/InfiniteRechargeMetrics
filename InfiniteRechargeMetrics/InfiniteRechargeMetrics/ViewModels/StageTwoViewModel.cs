using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.PerformancePages;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class StageTwoViewModel : StageViewModelBase
    {
        #region Points Scored
        public int StageTwoLowPortPoints
        {
            get => Performance.StageTwoLowPortPoints;
            set
            {
                if (value < MIN_POINTS || value > MAX_POINTS) return;
                Performance.StageTwoLowPortPoints = value;
                NotifyPropertyChanged(nameof(StageTwoLowPortPoints));
            }
        }
        public int StageTwoUpperPortPoints
        {
            get => Performance.StageTwoUpperPortPoints;
            set
            {
                if (value < MIN_POINTS || value > MAX_POINTS) return;
                Performance.StageTwoUpperPortPoints = value;
                NotifyPropertyChanged(nameof(StageTwoUpperPortPoints));
            }
        }
        public int StageTwoSmallPortPoints
        {
            get => Performance.StageTwoSmallPortPoints;
            set
            {
                if (value < MIN_POINTS || value > MAX_POINTS) return;
                Performance.StageTwoSmallPortPoints = value;
                NotifyPropertyChanged(nameof(StageTwoSmallPortPoints));
            }
        }
        #endregion

        public StageTwoViewModel(Performance _performance) : base(_performance)
        {

        }
    }
}
