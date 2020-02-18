using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.PerformancePages;
using System.Collections.ObjectModel;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class StageTwoViewModel : StageViewModelBase, IStageViewModel
    {
        #region Points Scored
        public override int StageLowPortTotalValue => StageLowPortPoints.Count * StageConstants.MANUAL_LPP;
        public override int StageUpperPortTotalValue => StageUpperPortPoints.Count * StageConstants.MANUAL_UPP;
        public override int StageSmallPortTotalValue => StageSmallPortPoints.Count * StageConstants.MANUAL_SPP;

        public override ObservableCollection<Point> StageLowPortPoints
        {
            get => Performance.StageTwoLowPortPoints;
            set => Performance.StageTwoLowPortPoints = value;
        }
        public override ObservableCollection<Point> StageUpperPortPoints
        {
            get => Performance.StageTwoUpperPortPoints;
            set => Performance.StageTwoLowPortPoints = value;
        }
        public override ObservableCollection<Point> StageSmallPortPoints
        {
            get => Performance.StageTwoSmallPortPoints;
            set => Performance.StageTwoSmallPortPoints = value;
        }
        #endregion

        /// <summary>
        ///     Boolean representing whether the team has completed the control panel step.
        /// </summary>
        public bool IsControlPanelFinished
        {
            get => Performance.IsStageTwoControlPanelFinished;
            set
            {
                Performance.IsStageTwoControlPanelFinished = value;
                CheckIfStageIsComplete();
            }
        }

        public StageTwoViewModel(StageTwoPage _stageTwoPage, Performance _performance, StageCompletionManager _stageCompletionManager) : base(_performance, _stageCompletionManager)
        {
            _stageTwoPage.ControlPanelSwitch.Toggled += ControlPanelSwitch_Toggled;            
        }

        /// <summary>
        ///     Handler for the control switch being toggled.
        ///     Updates the Performance's control panel value based off the switch.
        /// </summary>
        private void ControlPanelSwitch_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            IsControlPanelFinished = e.Value;
        }

        public override void CheckIfStageIsComplete()
        {
            if (!IsControlPanelFinished || !(base.AddTotalValues(StageLowPortTotalValue +
                                                                 StageUpperPortTotalValue +
                                                                 StageSmallPortTotalValue) >= StageConstants.MIN_VALUE_FOR_COMPLETED_STAGE_TWO))
            {
                base.StageCompletionManager.IsStageTwoComplete = false;
                return;
            }
            else
            {
                base.StageCompletionManager.IsStageTwoComplete = true;
            }
        }
    }
}
