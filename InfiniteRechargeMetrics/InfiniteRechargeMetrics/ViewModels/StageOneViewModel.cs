using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.PerformancePages;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     MVVM for the stage One Page
    /// </summary>
    public class StageOneViewModel : StageViewModelBase, IStageViewModel
    {
        #region Stage Points Scored

        #region Autonomous Points Scored
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
        #endregion

        #region Manual Points Scored
        public int StageOneLowPortPoints
        {
            get => Performance.StageOneLowPortPoints;
            set
            {
                if (value < 0) return;
                Performance.StageOneLowPortPoints = value;
                NotifyPropertyChanged(nameof(StageOneLowPortPoints));
            }
        }
        public int StageOneUpperPortPoints
        {
            get => Performance.StageOneUpperPortPoints;
            set
            {
                if (value < 0) return;
                Performance.StageOneUpperPortPoints = value;
                NotifyPropertyChanged(nameof(StageOneUpperPortPoints));
            }
        }
        public int StageOneSmallPortPoints
        {
            get => Performance.StageOneSmallPortPoints;
            set
            {
                if (value < 0) return;
                Performance.StageOneSmallPortPoints = value;
                NotifyPropertyChanged(nameof(StageOneSmallPortPoints));
            }
        }
        #endregion

        #endregion

        public StageState StageState { get; private set; } = StageState.Autononmous;

        public ICommand ChangePointsCMD { get; set; }

        public StageOneViewModel(Performance _performance) : base(_performance) 
        {
            ChangePointsCMD = new Command(HandleChangePointsBtnClicked);
        }

        public void HandleChangePointsBtnClicked(object charCode)
        {
            base.ChangePoints(this, (string)charCode);
        }
    }
}
