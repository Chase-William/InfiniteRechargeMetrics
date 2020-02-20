using InfiniteRechargeMetrics.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Point = InfiniteRechargeMetrics.Models.Point;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     enum for determining wether the stage is in autonomous mode or manuals
    /// </summary>
    public enum StageState { Autononmous, Manual }    

    /// <summary>
    ///     Base class for all stage viewmodels
    /// </summary>
    public abstract class StageViewModelBase : NotifyClass, IStageViewModel
    {
        #region Singletons Children Use
        private const string ADD_POINTS = "+";
        public const int MIN_POINTS = 0;
        public const int MAX_POINTS = 999;
        /// <summary>
        ///     Used for determining which port was the intent click to.
        /// </summary>
        private const int LOW_PORT = 0;
        private const int UPPER_PORT = 1;
        private const int SMALL_PORT = 2;
        /// <summary>
        ///     Tracks whether the Xamarin.Essentials Vibrate is supported on this device.
        /// </summary>
        private static bool IsVibrateSupported = true;                          

        public static Stopwatch Stopwatch { get; set; } = new Stopwatch();
        #endregion

        #region Points List & Total Value Props
        //
        //     Calculates the overall value of a list.
        //     Must be overriden.
        //
        public virtual int StageLowPortTotalValue { get; }
        public virtual int StageUpperPortTotalValue { get; }
        public virtual int StageSmallPortTotalValue { get; }
        //
        //      Holds the collection of points made for the current child.
        //      Must be overriden.
        //
        public virtual ObservableCollection<Point> CurrentStagePortPoints { get; set; }
        #endregion

        /// <summary>
        ///     Shared Class for tracking the state of all stages and their completion.
        /// </summary>
        public StageCompletionManager StageCompletionManager { get; set; }

        private Match match;
        public Match Match
        {
            get => match;
            set => match = value;
        }
       
        /// <summary>
        ///     Command that is tied to all the buttons that change points.
        /// </summary>
        public ICommand ChangePointsCMD { get; set; }

        public StageViewModelBase(Match _match, StageCompletionManager _stageCompletionManager)
        {
            Stopwatch = new Stopwatch();
            ChangePointsCMD = new Command((object charCode) => ChangePoints(this, (string)charCode, Stopwatch.Elapsed.Milliseconds));
            StageCompletionManager = _stageCompletionManager;
            Match = _match;
            StageCompletionManager.SetRecordingState(false);           
            
            // when any of the overrided or current version of this collection changed, notify the binding engine
            CurrentStagePortPoints.CollectionChanged += delegate
            {
                NotifyPropertiesChanged(nameof(StageLowPortTotalValue), nameof(StageUpperPortTotalValue), nameof(StageSmallPortTotalValue));
                CheckIfStageIsComplete();
            };
        }

        private static void ChangePoints(StageViewModelBase _viewModel, string entireCode, int _milliSeconds)
        {
            // Will only vibrate the device as long it has worked before and therefore is supported.
            if (IsVibrateSupported)
            {
                try
                {
                    Vibration.Vibrate(TimeSpan.FromMilliseconds(180));
                }
                catch (FeatureNotSupportedException ex)
                {
                    IsVibrateSupported = false;
                }
                catch (Exception ex)
                {
                    IsVibrateSupported = false;
                }
            }
            

            // if nothing was passed in the code, return
            if (entireCode == null) return;
            // Parsing to a number so we can use an enum to make this more readable
            if (!int.TryParse(entireCode.Substring(1, 1), out int portIndex)) return;
            // informs whether we shall add or subtract based off the code
            string portOperator = entireCode.Substring(0, 1);

            Point newPoint = new Point
            {
                TimeClickedFromStart = _milliSeconds
            };

            // Process for StageOne
            if (_viewModel is StageOneViewModel stageOneViewModel)
            {
                // Process Automonous Click
                if (stageOneViewModel.StageState == StageState.Autononmous)
                {
                    switch (portIndex)
                    {
                        // Autonomous Mode Low Port,   2 points
                        case LOW_PORT:
                            ChangePoints(stageOneViewModel.AutonomousPortPoints, PointType.AutomonousLow);
                            break;
                        // Autonomous Mode Upper Port, 4 points
                        case UPPER_PORT:
                            ChangePoints(stageOneViewModel.AutonomousPortPoints, PointType.AutomonousUpper);
                            break;
                        // Autonomous Mode Small Port, 6 points
                        case SMALL_PORT:
                            ChangePoints(stageOneViewModel.AutonomousPortPoints, PointType.AutomonousSmall);
                            break;
                        default:
                            break;
                    }
                }
                // Process Manual Click
                else
                {
                    switch (portIndex)
                    {
                        // Autonomous Mode Low Port,   2 points
                        case LOW_PORT:
                            ChangePoints(stageOneViewModel.CurrentStagePortPoints, PointType.StageOneLow);
                            break;
                        // Autonomous Mode Upper Port, 4 points
                        case UPPER_PORT:
                            ChangePoints(stageOneViewModel.CurrentStagePortPoints, PointType.StageOneUpper);
                            break;
                        // Autonomous Mode Small Port, 6 points
                        case SMALL_PORT:
                            ChangePoints(stageOneViewModel.CurrentStagePortPoints, PointType.StageOneSmall);
                            break;
                        default:
                            break;
                    }
                }
            }
            // Process for Stage Two
            else if (_viewModel is StageTwoViewModel stageTwoViewModel)
            {
                switch (portIndex)
                {
                    // Autonomous Mode Low Port,   2 points
                    case LOW_PORT:
                        ChangePoints(stageTwoViewModel.CurrentStagePortPoints, PointType.StageTwoLow);
                        break;
                    // Autonomous Mode Upper Port, 4 points
                    case UPPER_PORT:
                        ChangePoints(stageTwoViewModel.CurrentStagePortPoints, PointType.StageTwoUpper);
                        break;
                    // Autonomous Mode Small Port, 6 points
                    case SMALL_PORT:
                        ChangePoints(stageTwoViewModel.CurrentStagePortPoints, PointType.StageTwoSmall);
                        break;
                    default:
                        break;
                }
            }
            // Process for Stage Three
            else if (_viewModel is StageThreeViewModel stageThreeViewModel)
            {
                switch (portIndex)
                {
                    case LOW_PORT:
                        ChangePoints(stageThreeViewModel.CurrentStagePortPoints, PointType.StageThreeLow);
                        break;
                    case UPPER_PORT:
                        ChangePoints(stageThreeViewModel.CurrentStagePortPoints, PointType.StageThreeUpper);
                        break;
                    case SMALL_PORT:
                        ChangePoints(stageThreeViewModel.CurrentStagePortPoints, PointType.StageThreeSmall);
                        break;
                    default:
                        break;
                }
            }

            // Modifies Performance port's points 
            void ChangePoints(ObservableCollection<Point> _points, PointType _pointType)
            {
                // Add a new point
                if (portOperator.Equals(ADD_POINTS))
                {
                    newPoint.SetPointType(_pointType);
                    _points.Add(newPoint);
                }
                // Remove the last point
                else if (_points.Count != 0)
                {
                    _points.RemoveAt(_points.Count - 1);
                }
            }
        }

        /// <summary>
        ///     Helper function to add integers together... Math lib didnt have this
        /// </summary>
        public int AddTotalValues(params int[] _value)
        {
            int sum = 0;
            for (int i = 0; i < _value.Length; i++)
            {
                sum += _value[i];
            }
            return sum;
        }

        /// <summary>
        ///     All classes will have their own implementation of this method to check the status of their stage completion
        /// </summary>
        public virtual void CheckIfStageIsComplete() { }
    }
}
