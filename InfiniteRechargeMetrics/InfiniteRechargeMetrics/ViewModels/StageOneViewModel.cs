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

        private bool isControlPanelFinished;
        /// <summary>
        ///     Boolean representing whether the team has completed the control panel step. ------------------------------- need to make it databindable and also link to our static class, use interface instead
        /// </summary>
        public bool IsControlPanelFinished { 
            get => isControlPanelFinished; 
            set 
            {
                isControlPanelFinished = value;
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

        public StageOneViewModel(StageOnePage _stageOnePage, Performance _performance) : base(_performance)
        {           
            StartTimerCMD = new Command(StartAutonomousTimer);
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
            StageLowPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(StageLowPortTotalValue));
                CheckIfStageIsComplete();
            };
            StageUpperPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(StageUpperPortTotalValue));
                CheckIfStageIsComplete();
            };
            StageSmallPortPoints.CollectionChanged += delegate
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
        private void CheckIfStageIsComplete()
        {
            if (!IsControlPanelFinished)
            {
                IsStageComplete = false;
                return;
            }           

            if (!(AddTotalValues(AutoStageLowPortTotalValue + 
                                 AutoStageUpperPortTotalValue + 
                                 AutoStageSmallPortTotalValue +
                                 ManualStageLowPortTotalValue + 
                                 ManualStageUpperPortTotalValue + 
                                 ManualStageSmallPortTotalValue) >= StageConstants.MIN_VALUE_FOR_COMPLETED_STAGE_ONE))
            {
                IsStageComplete = false;
                return;
            }

            // Under those conditions the stage can be set to finished (true)
            IsStageComplete = true;
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
        ///     Notifies the properties needing to be updated
        /// </summary>
        private void UpdateViews()
        {
            NotifyPropertiesChanged(nameof(StageState), nameof(StageLowPortTotalValue), nameof(StageUpperPortTotalValue), nameof(StageSmallPortTotalValue));
        }

        /// <summary>
        ///     Helper function to add integers together... Math lib didnt have this
        /// </summary>
        private int AddTotalValues(params int[] _value)
        {
            int sum = 0;
            for (int i = 0; i < _value.Length; i++)
            {
                sum += _value[i];
            }
            return sum;
        }
    }
}
