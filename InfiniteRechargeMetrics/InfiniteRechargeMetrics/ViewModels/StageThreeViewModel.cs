using InfiniteRechargeMetrics.Models;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class StageThreeViewModel : StageViewModelBase
    {
        #region Points Scored
        public int StageThreeLowPortPoints
        {
            get => Performance.StageThreeLowPortPoints;
            set
            {
                if (value < MIN_POINTS || value > MAX_POINTS) return;
                Performance.StageThreeLowPortPoints = value;
                NotifyPropertyChanged(nameof(StageThreeLowPortPoints));
            }
        }
        public int StageThreeUpperPortPoints
        {
            get => Performance.StageThreeUpperPortPoints;
            set
            {
                if (value < MIN_POINTS || value > MAX_POINTS) return;
                Performance.StageThreeUpperPortPoints = value;
                NotifyPropertyChanged(nameof(StageThreeUpperPortPoints));
            }
        }
        public int StageThreeSmallPortPoints
        {
            get => Performance.StageThreeSmallPortPoints;
            set
            {
                if (value < MIN_POINTS || value > MAX_POINTS) return;
                Performance.StageThreeSmallPortPoints = value;
                NotifyPropertyChanged(nameof(StageThreeSmallPortPoints));
            }
        }
        #endregion
        public StageThreeViewModel(Performance _performance) : base(_performance)
        {

        }
    }
}
