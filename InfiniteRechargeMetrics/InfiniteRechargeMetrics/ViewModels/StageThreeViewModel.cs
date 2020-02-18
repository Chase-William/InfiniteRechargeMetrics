using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.PerformancePages;
using System.Collections.ObjectModel;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class StageThreeViewModel : StageViewModelBase, IStageViewModel
    {
        #region Points Scored
        public override int StageLowPortTotalValue => StageLowPortPoints.Count * StageConstants.MANUAL_LPP;
        public override int StageUpperPortTotalValue => StageUpperPortPoints.Count * StageConstants.MANUAL_UPP;
        public override int StageSmallPortTotalValue => StageSmallPortPoints.Count * StageConstants.MANUAL_SPP;

        public override ObservableCollection<Point> StageLowPortPoints
        {
            get => Performance.StageThreeLowPortPoints;
            set => Performance.StageThreeLowPortPoints = value;
        }
        public override ObservableCollection<Point> StageUpperPortPoints
        {
            get => Performance.StageThreeUpperPortPoints;
            set => Performance.StageThreeUpperPortPoints = value;
        }
        public override ObservableCollection<Point> StageSmallPortPoints
        {
            get => Performance.StageThreeSmallPortPoints;
            set => Performance.StageThreeSmallPortPoints = value;
        }
        #endregion        

        public StageThreeViewModel(StageThreePage _stageThreePage, Performance _performance, StageCompletionManager _stageCompletionManager) : base(_performance, _stageCompletionManager) 
        {
            _stageThreePage.DroidOneRandevuSwitch.Toggled   += (e, a) => Performance.DroidOneRandevu   = a.Value;
            _stageThreePage.DroidTwoRandevuSwitch.Toggled   += (e, a) => Performance.DroidTwoRandevu   = a.Value;
            _stageThreePage.DroidThreeRandevuSwitch.Toggled += (e, a) => Performance.DroidThreeRandevu = a.Value;
            _stageThreePage.IsRandevuBarLevelSwitch.Toggled += (e, a) => Performance.IsRandevuLevel    = a.Value;
        }

        public override void CheckIfStageIsComplete()
        {
            if (!(base.AddTotalValues(StageLowPortTotalValue +
                                      StageUpperPortTotalValue +
                                      StageSmallPortTotalValue) >= StageConstants.MIN_VALUE_FOR_COMPLETED_STAGE_TWO))
            {
                base.StageCompletionManager.IsStageThreeComplete = false;
                return;
            }
            else
            {
                base.StageCompletionManager.IsStageThreeComplete = true;
            }
        }
    }
}
