﻿using InfiniteRechargeMetrics.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;
using Point = InfiniteRechargeMetrics.Models.Point;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     enum for determining wether the stage is in autonomous mode or manuals
    /// </summary>
    public enum StageState { Autononmous, Manual }

    /// <summary>
    ///     enum for determining the meaning of the command parameters from XAML command bindings.
    ///     Low   == 0
    ///     Upper == 1
    ///     Small == 2
    /// </summary>
    public enum PointFromXAMLIdentifier { Low, Upper, Small }

    /// <summary>
    ///     Base class for all stage viewmodels
    /// </summary>
    public abstract class StageViewModelBase : INotifyPropertyChanged
    {
        #region Singletons Children Use
        private const string ADD_POINTS = "+";
        public const int MIN_POINTS = 0;
        public const int MAX_POINTS = 999;

        public static Timer MainTimer { get; set; } = new Timer();

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
        ///     Gets whether or not the application is recording
        /// </summary>
        public bool IsRecording { get => isRecording; }

        private bool isStageComplete;
        /// <summary>
        ///     Is set to true when the stage has all the requirements fulfilled to be consider complete.
        /// </summary>
        public bool IsStageComplete
        {
            get => isStageComplete;
            set
            {
                isStageComplete = value;
                NotifyPropertyChanged(nameof(IsStageComplete));
            }
        }

        private Performance performance;
        public Performance Performance
        {
            get => performance;
            set => performance = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     To use this method; YOU MUST SPECIFY PROPERTY NAME.
        /// </summary>
        protected void NotifyPropertiesChanged(params string[] propertyNames)
        {
            foreach (string propName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        /// <summary>
        ///     Command that is tied to all the buttons that change points.
        /// </summary>
        public ICommand ChangePointsCMD { get; set; }

        public StageViewModelBase(Performance _performance)
        {
            ChangePointsCMD = new Command((object charCode) => ChangePoints(this, (string)charCode, Stopwatch.Elapsed.Milliseconds));
            Performance = _performance;
            SetRecordingState(this, false);
        }

        protected async void ChangePoints(StageViewModelBase _viewModel, string entireCode, int _milliSeconds)
        {
            // if nothing was passed in the code, return
            if (entireCode == null) return;
            // Parsing to a number so we can use an enum to make this more readable
            if (!int.TryParse(entireCode.Substring(1, 1), out int portIndex)) return;
            // informs whether we shall add or subtract based off the code
            string portOperator = entireCode.Substring(0, 1);

            List<int> test = await Data.DatabaseService.GetNextId();

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
                        case (int)PointFromXAMLIdentifier.Low:
                            ChangePoints(stageOneViewModel.AutoLowPortPoints, PointType.AutomonousLow);
                            break;
                        // Autonomous Mode Upper Port, 4 points
                        case (int)PointFromXAMLIdentifier.Upper:
                            ChangePoints(stageOneViewModel.AutoUpperPortPoints, PointType.AutomonousUpper);
                            break;
                        // Autonomous Mode Small Port, 6 points
                        case (int)PointFromXAMLIdentifier.Small:
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
                        case (int)PointFromXAMLIdentifier.Low:
                            ChangePoints(stageOneViewModel.StageLowPortPoints, PointType.StageOneLow);
                            break;
                        // Autonomous Mode Upper Port, 4 points
                        case (int)PointFromXAMLIdentifier.Upper:
                            ChangePoints(stageOneViewModel.StageUpperPortPoints, PointType.StageOneUpper);
                            break;
                        // Autonomous Mode Small Port, 6 points
                        case (int)PointFromXAMLIdentifier.Small:
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
                    case (int)PointFromXAMLIdentifier.Low:
                        ChangePoints(stageTwoViewModel.StageLowPortPoints, PointType.StageOneLow);
                        break;
                    // Autonomous Mode Upper Port, 4 points
                    case (int)PointFromXAMLIdentifier.Upper:
                        ChangePoints(stageTwoViewModel.StageUpperPortPoints, PointType.StageOneUpper);
                        break;
                    // Autonomous Mode Small Port, 6 points
                    case (int)PointFromXAMLIdentifier.Small:
                        ChangePoints(stageTwoViewModel.StageSmallPortPoints, PointType.StageOneSmall);
                        break;
                    default:
                        break;
                }
            }
            // Process for Stage Three
            else if (_viewModel is StageThreeViewModel stageThreeViewModel)
            {

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
    }
}
