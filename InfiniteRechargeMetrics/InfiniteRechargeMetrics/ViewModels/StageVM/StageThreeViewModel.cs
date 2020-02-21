using Point = InfiniteRechargeMetrics.Models.Point;
using InfiniteRechargeMetrics.Models;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

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
            get => Match.StageThreePortPoints;
            set => Match.StageThreePortPoints = value;
        }

        /// <summary>
        ///     Boolean representing whether the team has completed the control panel step.
        /// </summary>
        public bool IsControlPanelFinished
        {
            get => Match.IsStageThreeControlPanelFinished;
            set
            {
                Match.IsStageThreeControlPanelFinished = value;
                CheckIfStageIsComplete();
            }
        }
        public bool DroidOneRandevu { 
            get => Match.DroidOneRandevu; 
            set
            {
                Match.DroidOneRandevu = value;
                CheckIfStageIsComplete();
            } 
        }
        public bool DroidTwoRandevu
        {
            get => Match.DroidTwoRandevu;
            set
            {
                Match.DroidTwoRandevu = value;
                CheckIfStageIsComplete();
            }
        }
        public bool DroidThreeRandevu
        {
            get => Match.DroidThreeRandevu;
            set
            {
                Match.DroidThreeRandevu = value;
                CheckIfStageIsComplete();
            }
        }
        public bool IsRandevuLevel
        {
            get => Match.IsRandevuLevel;
            set
            {
                Match.IsRandevuLevel = value;
            }
        }
        #endregion
       

       

        public StageThreeViewModel(Match _performance, StageCompletionManager _stageCompletionManager) : base(_performance, _stageCompletionManager) 
        {
            
        }

      

        public override void CheckIfStageIsComplete()
        {
            if (!IsControlPanelFinished || !(base.AddTotalValues(StageLowPortTotalValue +
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
