using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.PerformancePages;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using Point = InfiniteRechargeMetrics.Models.Point;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     MVVM for the stage One Page
    /// </summary>
    public class StageOneViewModel : StageViewModelBase, IStageViewModel
    {
        private event Action StageStateChanged;

        private const int AUTOMONOUS_DURATION = 1000;
        public Timer MainTimer { get; set; } = new Timer();
        public Stopwatch Stopwatch { get; set; } = new Stopwatch();

        // Funcs that will be used to make our properties for Total values more dynamic depending on the state of the stage.
        public Func<int> LowPortTotalValueAction;
        public Func<int> UpperPortTotalValueAction;
        public Func<int> SmallPortTotalValueAction;

        #region Stage Points Scored

        // --- Properties used for getting the count of a specific collection
        public int GenericLowPortTotalValue { get => LowPortTotalValueAction.Invoke(); }
        public int GenericUpperPortTotalValue { get => UpperPortTotalValueAction.Invoke(); }
        public int GenericSmallPortTotalValue { get => SmallPortTotalValueAction.Invoke(); }

        #region Autonomous Points Scored
        public ObservableCollection<Point> AutoLowPortPoints {
            get => Performance.AutoLowPortPoints;
            set => Performance.AutoLowPortPoints = value;
        }
        public ObservableCollection<Point> AutoUpperPortPoints {
            get => Performance.AutoUpperPortPoints;
            set => Performance.AutoUpperPortPoints = value;
        }
        public ObservableCollection<Point> AutoSmallPortPoints {
            get => Performance.AutoSmallPortPoints;
            set => Performance.AutoSmallPortPoints = value;
        }        
        #endregion

        #region Manual Points Scored
        public ObservableCollection<Point> StageOneLowPortPoints {
            get => Performance.StageOneLowPortPoints;
            set => Performance.StageOneLowPortPoints = value;
        }
        public ObservableCollection<Point> StageOneUpperPortPoints {
            get => Performance.StageOneUpperPortPoints;
            set => Performance.StageOneUpperPortPoints = value;
        }
        public ObservableCollection<Point> StageOneSmallPortPoints {
            get => Performance.StageOneSmallPortPoints;
            set => Performance.StageOneSmallPortPoints = value;
        }
        #endregion

        /// <summary>
        ///     Boolean representing whether the team has completed the control panel step.
        /// </summary>
        public bool IsControlPanelFinished { get; set; }

        #endregion

        private StageState stageState;
        public StageState StageState {
            get => stageState;
            private set
            {
                stageState = value;
                StageStateChanged?.Invoke();
            }
        }

        /// <summary>
        ///     Command that is tied to all the buttons that change points.
        /// </summary>
        public ICommand ChangePointsCMD { get; set; }
        /// <summary>
        ///     Command for the timer button
        /// </summary>
        public ICommand StartTimerCMD { get; set; }

        public StageOneViewModel(StageOnePage _stageOnePage, Performance _performance) : base(_performance) 
        {
            ChangePointsCMD = new Command(HandleChangePointsBtnClicked);
            StartTimerCMD = new Command(StartAutonomousTimer);
            _stageOnePage.ControlPanelSwitch.Toggled += ControlPanelSwitch_Toggled;
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
                NotifyPropertyChanged(nameof(GenericLowPortTotalValue));
            };
            AutoUpperPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(GenericUpperPortTotalValue));
            };
            AutoSmallPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(GenericSmallPortTotalValue));
            };
            StageOneLowPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(GenericLowPortTotalValue));
            };
            StageOneUpperPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(GenericUpperPortTotalValue));
            };
            StageOneSmallPortPoints.CollectionChanged += delegate
            {
                NotifyPropertyChanged(nameof(GenericSmallPortTotalValue));
            };
        }        

        /// <summary>
        ///     Starts the timer setup for the autonomous state
        /// </summary>
        private void StartAutonomousTimer()
        {            
            MainTimer.Interval = AUTOMONOUS_DURATION;
            MainTimer.AutoReset = false;
            MainTimer.Start();
            Stopwatch.Start();
        }
        
        /// <summary>
        ///     Handles the elasped timer for when the Timer finishes autononmous mode
        /// </summary>
        private void AutononmousState_TimerElapsed(object sender, ElapsedEventArgs e)
        {
            MainTimer.Elapsed -= AutononmousState_TimerElapsed;
            StageState = StageState.Manual;
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
                    return AutoLowPortPoints.Count * StageConfig.AUTO_LPP;
                };
                UpperPortTotalValueAction = () =>
                {
                    return AutoUpperPortPoints.Count * StageConfig.AUTO_UPP;
                };
                SmallPortTotalValueAction = () =>
                {
                    return AutoSmallPortPoints.Count * StageConfig.AUTO_SPP;
                };
            }   
            // do if not autonomous
            else
            {
                // Initializing Funcs for our dynamic properties
                LowPortTotalValueAction = () =>
                {
                    return StageOneLowPortPoints.Count * StageConfig.MANUAL_LPP;
                };
                UpperPortTotalValueAction = () =>
                {
                    return StageOneUpperPortPoints.Count * StageConfig.MANUAL_UPP;
                };
                SmallPortTotalValueAction = () =>
                {
                    return StageOneSmallPortPoints.Count * StageConfig.MANUAL_SPP;
                };
            }
        }

        /// <summary>
        ///     Notifies the properties needing to be updated
        /// </summary>
        private void UpdateViews()
        {
            NotifyPropertiesChanged(nameof(StageState), nameof(GenericLowPortTotalValue), nameof(GenericUpperPortTotalValue), nameof(GenericSmallPortTotalValue));
        }
    }
}
