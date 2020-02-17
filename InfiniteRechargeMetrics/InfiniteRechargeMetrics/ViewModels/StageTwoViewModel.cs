using InfiniteRechargeMetrics.Models;
using System.Collections.ObjectModel;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class StageTwoViewModel : StageViewModelBase
    {
        #region Points Scored
        public override int StageLowPortTotalValue => StageLowPortPoints.Count * StageConstants.MANUAL_LPP;
        public override int StageUpperPortTotalValue => StageUpperPortPoints.Count * StageConstants.MANUAL_UPP;
        public override int StageSmallPortTotalValue => StageSmallPortPoints.Count * StageConstants.MANUAL_SPP;

        public override ObservableCollection<Point> StageLowPortPoints {
            get => Performance.StageTwoLowPortPoints;
            set => Performance.StageTwoLowPortPoints = value;
        }
        public override ObservableCollection<Point> StageUpperPortPoints {
            get => Performance.StageTwoUpperPortPoints;
            set => Performance.StageTwoLowPortPoints = value;
        }
        public override ObservableCollection<Point> StageSmallPortPoints {
            get => Performance.StageTwoSmallPortPoints;
            set => Performance.StageTwoSmallPortPoints = value;
        }
        #endregion

        public StageTwoViewModel(Performance _performance) : base(_performance)
        {
            StageLowPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(StageLowPortTotalValue));
            };
            StageUpperPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(StageUpperPortTotalValue));
            };
            StageSmallPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(StageSmallPortTotalValue));
            };
        }
    }
}
