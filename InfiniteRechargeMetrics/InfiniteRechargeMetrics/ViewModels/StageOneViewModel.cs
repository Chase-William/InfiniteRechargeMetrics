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
    public class StageOneViewModel : StageViewModelBase, IStageViewModel
    {
        private event Action StageStateChanged;
        private event Action CheckIfMeetStageRequirements;
        private int timerCounter;

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

        // Funcs that will be used to make our properties for Total values more dynamic depending on the state of the stage.
        public Func<int> LowPortTotalValueAction;
        public Func<int> UpperPortTotalValueAction;
        public Func<int> SmallPortTotalValueAction;

        public override int StageLowPortTotalValue => LowPortTotalValueAction.Invoke();
        public override int StageUpperPortTotalValue => UpperPortTotalValueAction.Invoke();
        public override int StageSmallPortTotalValue => SmallPortTotalValueAction.Invoke();

        #region Stage Points Scored        

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
        public bool IsControlPanelFinished { get; set; }

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

        public StageOneViewModel(StageOnePage _stageOnePage, Performance _performance) : base(_performance)
        {
            StartTimerCMD = new Command(StartAutonomousTimer);
            CheckIfMeetStageRequirements += StageOneViewModel_CheckIfMeetStageRequirements;
            _stageOnePage.ControlPanelSwitch.Toggled += ControlPanelSwitch_Toggled;
            RobotsMovedFromSpawnPoints = StageConstants.ROBOTS_MOVED_FROM_SPAWN_DEFAULT_VALUE;
            // Preparing the timer
            MainTimer.Elapsed += AutononmousState_TimerElapsed;

            // Binding handlers for our state changed event
            StageStateChanged += BindTotalValueBasedOffState;
            StageStateChanged += UpdateViews;

            // Binding for autonomous as default
            BindTotalValueBasedOffState();

            // Attaching the notify prop to our collection changed event as a handler
            AutoLowPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(StageLowPortTotalValue));
            };
            AutoUpperPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(StageUpperPortTotalValue));
            };
            AutoSmallPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(StageSmallPortTotalValue));
            };
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

        /// <summary>
        ///     Checks all the required fields for this stage to be considered complete
        /// </summary>
        private void StageOneViewModel_CheckIfMeetStageRequirements()
        {
            // Check auto
            // check manage 
            // -- both must come to atleast 9 points --------------------------------------------------------------------------------- start here
            // check control panel <- do this first.. faster
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
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Handles the controlpanel switch being toggled
        /// </summary>
        private void ControlPanelSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            IsControlPanelFinished = e.Value;
        }

        /// <summary>
        ///     Handles all increment and decrement buttons being pressed,
        ///     Propogates this information to the parent class for determining the right actions.     
        /// </summary>
        public void HandleChangePointsBtnClicked(object charCode)
        {
            base.ChangePoints(this, (string)charCode, Stopwatch.Elapsed.Milliseconds);
        }

        /// <summary>
        ///     Sets the action for our total value props to autonomous settings.
        /// </summary>
        private void BindTotalValueBasedOffState()
        {
            // Do if state is autonomous
            if (StageState == StageState.Autononmous)
            {
                // Initializing Funcs for our dynamic properties
                LowPortTotalValueAction = () =>
                {
                    return AutoLowPortPoints.Count * StageConstants.AUTO_LPP;
                };
                UpperPortTotalValueAction = () =>
                {
                    return AutoUpperPortPoints.Count * StageConstants.AUTO_UPP;
                };
                SmallPortTotalValueAction = () =>
                {
                    return AutoSmallPortPoints.Count * StageConstants.AUTO_SPP;
                };
            }
            // do if not autonomous
            else
            {
                // Initializing Funcs for our dynamic properties
                LowPortTotalValueAction = () =>
                {
                    return StageLowPortPoints.Count * StageConstants.MANUAL_LPP;
                };
                UpperPortTotalValueAction = () =>
                {
                    return StageUpperPortPoints.Count * StageConstants.MANUAL_UPP;
                };
                SmallPortTotalValueAction = () =>
                {
                    return StageSmallPortPoints.Count * StageConstants.MANUAL_SPP;
                };
            }
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
