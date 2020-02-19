using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.PerformancePages;
using System.Collections.ObjectModel;
using System.Linq;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class StageThreeViewModel : StageViewModelBase, IStageViewModel
    {
        #region Points Scored
        public override int StageLowPortTotalValue => CurrentStagePortPoints.Where(point => point.GetPointType() == PointType.StageThreeLow).ToList().Count * StageConstants.MANUAL_LPP;
        public override int StageUpperPortTotalValue => CurrentStagePortPoints.Where(point => point.GetPointType() == PointType.StageThreeUpper).ToList().Count * StageConstants.MANUAL_UPP;
        public override int StageSmallPortTotalValue => CurrentStagePortPoints.Where(point => point.GetPointType() == PointType.StageThreeSmall).ToList().Count * StageConstants.MANUAL_SPP;

        public override ObservableCollection<Point> CurrentStagePortPoints
        {
            get => Performance.StageThreePortPoints;
            set => Performance.StageThreePortPoints = value;
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
