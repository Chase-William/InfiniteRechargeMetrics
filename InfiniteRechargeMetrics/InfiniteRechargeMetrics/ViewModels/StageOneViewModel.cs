using InfiniteRechargeMetrics.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using Point = InfiniteRechargeMetrics.Models.Point;
using InfiniteRechargeMetrics.Pages.PerformancePages;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     MVVM for the stage One Page
    /// </summary>
    public class StageOneViewModel : StageViewModelBase
    {
        /// <summary>
        ///     Event is fired when the state (Autonmonous / Manual) is changed.
        /// </summary>
        private event Action StageStateChanged;
        /// <summary>
        ///     Tracks interations of stopwatch elapsed
        /// </summary>
        private int timerCounter;

        public MasterRecordPerformancePage MasterPerformancePage { get; set; }

        private double progressBarProgress;
        public double ProgressBarProgress
        {
            get => progressBarProgress;
            set
            {
                progressBarProgress = value;
                NotifyPropertyChanged(nameof(ProgressBarProgress));
            }
        }

        #region Total Value Of Points Props
        // Gets the the total points worth of the collection
        Func<int> LowPortAction;
        Func<int> UpperPortAction;
        Func<int> SmallPortAction;

        public int AutoStageLowPortTotalValue => AutoLowPortPoints.Count * StageConstants.AUTO_LPP;        
        public int AutoStageUpperPortTotalValue => AutoUpperPortPoints.Count * StageConstants.AUTO_LPP;        
        public int AutoStageSmallPortTotalValue => AutoSmallPortPoints.Count * StageConstants.AUTO_SPP;             
        public int ManualStageLowPortTotalValue { get => StageLowPortPoints.Count * StageConstants.MANUAL_LPP; }
        public int ManualStageUpperPortTotalValue { get => StageUpperPortPoints.Count * StageConstants.MANUAL_UPP; }
        public int ManualStageSmallPortTotalValue { get => StageSmallPortPoints.Count * StageConstants.MANUAL_SPP; }
        public override int StageLowPortTotalValue => LowPortAction.Invoke();
        public override int StageUpperPortTotalValue => UpperPortAction.Invoke();
        public override int StageSmallPortTotalValue => SmallPortAction.Invoke();
        #endregion

        #region Stage Points / Variables        

        #region Autonomous Points Scored
        public ObservableCollection<Point> AutoLowPortPoints
        {
            get => Performance.AutoLowPortPoints;
            set => Performance.AutoLowPortPoints = value;
        }
        public ObservableCollection<Point> AutoUpperPortPoints
        {
            get => Performance.AutoUpperPortPoints;
            set => Performance.AutoUpperPortPoints = value;
        }
        public ObservableCollection<Point> AutoSmallPortPoints
        {
            get => Performance.AutoSmallPortPoints;
            set => Performance.AutoSmallPortPoints = value;
        }
        #endregion

        #region Manual Points Scored
        public override ObservableCollection<Point> StageLowPortPoints
        {
            get => Performance.StageOneLowPortPoints;
            set => Performance.StageOneLowPortPoints = value;
        }
        public override ObservableCollection<Point> StageUpperPortPoints
        {
            get => Performance.StageOneUpperPortPoints;
            set => Performance.StageOneUpperPortPoints = value;
        }
        public override ObservableCollection<Point> StageSmallPortPoints
        {
            get => Performance.StageOneSmallPortPoints;
            set => Performance.StageOneSmallPortPoints = value;
        }
        #endregion

        public int RobotsMovedFromSpawnPoints
        {
            get => Performance.RobotsMovedFromSpawnPoints;
            set
            {
                Performance.RobotsMovedFromSpawnPoints = value;
                NotifyPropertyChanged(nameof(RobotsMovedFromSpawnPoints));
            }
        }

        /// <summary>
        ///     Boolean representing whether the team has completed the control panel step.
        /// </summary>
        public bool IsControlPanelFinished { 
            get => Performance.IsStageOneControlPanelFinished; 
            set 
            {
                Performance.IsStageOneControlPanelFinished = value;
                CheckIfStageIsComplete();
            } 
        }

        #endregion

        private StageState stageState;
        public StageState StageState
        {
            get => stageState;
            private set
            {
                stageState = value;
                StageStateChanged?.Invoke();
            }
        }

        /// <summary>
        ///     Command for the timer button
        /// </summary>
        public ICommand StartTimerCMD { get; set; }        

        public StageOneViewModel(StageOnePage _stageOnePage, 
                                 Performance _performance, 
                                 StageCompletionManager _stageCompletionManager) : base(_performance, _stageCompletionManager)
        {           
            StartTimerCMD = new Command(StartAutonomousTimer);
            MasterPerformancePage = _stageOnePage.MasterPerformancePage;
            // Getting a reference to the masterPerforance to make calls on it's timer
            //MasterPerformanceVMO = _stageOnePage.MasterRecordPerformanceVMO;
            // Handles the switch being updated for control panel
            _stageOnePage.ControlPanelSwitch.Toggled += ControlPanelSwitch_Toggled;
            RobotsMovedFromSpawnPoints = StageConstants.ROBOTS_MOVED_FROM_SPAWN_DEFAULT_VALUE;
            // Preparing the timer
            MainTimer.Elapsed += AutononmousState_TimerElapsed;            
            // Binding handlers for our state changed event
            StageStateChanged += UpdateViews;
            StageStateChanged += BindPortLabelsToProp;

            BindPortLabelsToProp();

            // Attaching the notify prop to our collection changed event as a handler
            AutoLowPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(StageLowPortTotalValue));
                CheckIfStageIsComplete();
            };
            AutoUpperPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(StageUpperPortTotalValue));
                CheckIfStageIsComplete();
            };
            AutoSmallPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(StageSmallPortTotalValue));
                CheckIfStageIsComplete();
            };            
        }

        /// <summary>
        ///     Sets the binding property for the UI's implementation according to the stage's state
        /// </summary>
        private void BindPortLabelsToProp()
        {
            if (StageState == StageState.Autononmous)
            {
                LowPortAction = () =>
                {
                    return AutoStageLowPortTotalValue;
                };
                UpperPortAction = () =>
                {
                    return AutoStageUpperPortTotalValue;
                };
                SmallPortAction = () =>
                {
                    return AutoStageSmallPortTotalValue;
                };
            }
            else
            {
                LowPortAction = () =>
                {
                    return ManualStageLowPortTotalValue;
                };
                UpperPortAction = () =>
                {
                    return ManualStageUpperPortTotalValue;
                };
                SmallPortAction = () =>
                {
                    return ManualStageSmallPortTotalValue;
                };
            }
        }

        /// <summary>
        ///     Checks to see if the stage meets the requirements to move to the next stage.
        /// </summary>
        public override void CheckIfStageIsComplete()
        {
            if (!IsControlPanelFinished || !(AddTotalValues(AutoStageLowPortTotalValue +
                                                            AutoStageUpperPortTotalValue +
                                                            AutoStageSmallPortTotalValue +
                                                            ManualStageLowPortTotalValue +
                                                            ManualStageUpperPortTotalValue +
                                                            ManualStageSmallPortTotalValue) >= StageConstants.MIN_VALUE_FOR_COMPLETED_STAGE_ONE))
            {
                base.StageCompletionManager.IsStageOneComplete = false;
                return;
            }

            // Under those conditions the stage can be set to finished (true)
            base.StageCompletionManager.IsStageOneComplete = true;
        }

        /// <summary>
        ///     Starts the timer setup for the autonomous state
        /// </summary>
        private void StartAutonomousTimer()
        {
            SetRecordingState(this, true);
            MainTimer.Interval = 50;
            MainTimer.Start();
            Stopwatch.Start();
            MasterPerformancePage.ClockTimer.Start();
        }

        /// <summary>
        ///     Handles the elasped timer for when the Timer finishes autononmous mode
        /// </summary>
        private void AutononmousState_TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (timerCounter != StageConstants.AUTONOMOUS_MAX_TIMER_ITERATIONS)
            {
                ProgressBarProgress = ProgressBarProgress + StageConstants.AUTONOMOUS_PROGRESSBAR_UPDATE;
                timerCounter++;
            }
            else
            {
                MainTimer.Stop();
                MainTimer.Elapsed -= AutononmousState_TimerElapsed;
                MainTimer.Elapsed += ManualState_TimerElapsed;
                MainTimer.Interval = StageConstants.MANUAL_MODE_MAX_TIME;
                StageState = StageState.Manual;
                MainTimer.Start();

                
            }
        }        

        private void ManualState_TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Stopwatch.Stop();
            MainTimer.Stop();
            MasterPerformancePage.ClockTimer.Stop();
        }

        /// <summary>
        ///     Handler for the control switch being toggled.
        ///     Updates the Performance's control panel value based off the switch.
        /// </summary>
        private void ControlPanelSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            IsControlPanelFinished = e.Value;
        }

        /// <summary>
        ///     Notifies the properties needing to be updated
        /// </summary>
        private void UpdateViews()
        {
            NotifyPropertiesChanged(nameof(StageState), nameof(StageLowPortTotalValue), nameof(StageUpperPortTotalValue), nameof(StageSmallPortTotalValue));
        }        
    }
}
