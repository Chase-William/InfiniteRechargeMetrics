using InfiniteRechargeMetrics.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;
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
        public static Timer MainTimer { get; set; }        

        private static bool isRecording;
        /// <summary>
        ///     Method for setting the recording variable
        /// </summary>
        public static void SetRecordingState(StageViewModelBase sender, bool _recording)
        {
            isRecording = _recording;
            sender.NotifyPropertyChanged(nameof(IsRecording));
        }

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
        public virtual ObservableCollection<Point> StageLowPortPoints { get; set; }
        public virtual ObservableCollection<Point> StageUpperPortPoints { get; set; }
        public virtual ObservableCollection<Point> StageSmallPortPoints { get; set; }
        #endregion

        /// <summary>
        ///     Shared Class for tracking the state of all stages and their completion.
        /// </summary>
        public StageCompletionManager StageCompletionManager { get; set; }

        private Performance performance;
        public Performance Performance
        {
            get => performance;
            set => performance = value;
        }

        #region Page Data
        /// <summary>
        ///     Gets whether or not the application is recording
        /// </summary>
        public bool IsRecording { get => isRecording; }
       
        #endregion


        /// <summary>
        ///     Command that is tied to all the buttons that change points.
        /// </summary>
        public ICommand ChangePointsCMD { get; set; }

        public StageViewModelBase(Performance _performance, StageCompletionManager _stageCompletionManager)
        {
            MainTimer = new Timer();
            Stopwatch = new Stopwatch();
            ChangePointsCMD = new Command((object charCode) => ChangePoints(this, (string)charCode, Stopwatch.Elapsed.Milliseconds));
            StageCompletionManager = _stageCompletionManager;
            Performance = _performance;
            SetRecordingState(this, false);
            isRecording = false;

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

            // List<int> test = await Data.DatabaseService.GetNextId();

            // ------------------------------------------------------------------------------ we need to know the ID this have so we can give the points the primary key

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
                            ChangePoints(stageOneViewModel.AutoLowPortPoints, PointType.AutomonousLow);
                            break;
                        // Autonomous Mode Upper Port, 4 points
                        case UPPER_PORT:
                            ChangePoints(stageOneViewModel.AutoUpperPortPoints, PointType.AutomonousUpper);
                            break;
                        // Autonomous Mode Small Port, 6 points
                        case SMALL_PORT:
                            ChangePoints(stageOneViewModel.AutoSmallPortPoints, PointType.AutomonousSmall);
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
                            ChangePoints(stageOneViewModel.StageLowPortPoints, PointType.StageOneLow);
                            break;
                        // Autonomous Mode Upper Port, 4 points
                        case UPPER_PORT:
                            ChangePoints(stageOneViewModel.StageUpperPortPoints, PointType.StageOneUpper);
                            break;
                        // Autonomous Mode Small Port, 6 points
                        case SMALL_PORT:
                            ChangePoints(stageOneViewModel.StageSmallPortPoints, PointType.StageOneSmall);
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
                        ChangePoints(stageTwoViewModel.StageLowPortPoints, PointType.StageTwoLow);
                        break;
                    // Autonomous Mode Upper Port, 4 points
                    case UPPER_PORT:
                        ChangePoints(stageTwoViewModel.StageUpperPortPoints, PointType.StageTwoUpper);
                        break;
                    // Autonomous Mode Small Port, 6 points
                    case SMALL_PORT:
                        ChangePoints(stageTwoViewModel.StageSmallPortPoints, PointType.StageTwoSmall);
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
                        ChangePoints(stageThreeViewModel.StageLowPortPoints, PointType.StageThreeLow);
                        break;
                    case UPPER_PORT:
                        ChangePoints(stageThreeViewModel.StageUpperPortPoints, PointType.StageThreeUpper);
                        break;
                    case SMALL_PORT:
                        ChangePoints(stageThreeViewModel.StageSmallPortPoints, PointType.StageThreeSmall);
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

        public virtual void CheckIfStageIsComplete() { }
    }
}
