using Xamarin.Forms;
using System.Windows.Input;
using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.MatchPages;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class FinalizeRecordingViewModel : NotifyClass
    {
        #region Singletons
        // Positions that will determine whether a robot's UI is revealed.
        private const int REVEAL_FIRST_ROBOT_UI = 0;
        private const int REVEAL_SECOND_ROBOT_UI = 3;
        private const int REVEAL_THIRD_ROBOT_UI = 6;
        // Positions that will determine whether a robot's UI is made hidden.
        private const int HIDE_FIRST_ROBOT = 3;
        private const int HIDE_SECOND_ROBOT = 6;
        private const int HIDE_THIRD_ROBOT = 9;

        /// <summary>
        ///     Represents how much the RobotRecordingPos should be only be changed by.
        /// </summary>
        private const int INSERT_POS_CHANGE_AMT = 3;
        #endregion  

        private readonly MatchSetupPage performanceSetupPage;
        public Match Performance { get; set; }
        public StackLayout AddRemoveBtnLayout { get; set; }
        public FinalizeRecordingPage FinalizeRecordingPage { get; set; }

        #region Robots Visibility Props

        /// <summary>
        ///     These properties are used to determine how many robot layouts are visible.
        ///     They are used in bindings for the UI and also change the RobotsRecordingUIInsertPos value;        
        /// </summary>

        private bool isRobotOneBeingRecorded;
        public bool IsRobotOneBeingRecorded {
            get => isRobotOneBeingRecorded; 
            set
            {
                isRobotOneBeingRecorded = value;
                DeterminePosChange(value);
                NotifyPropertyChanged();
            }
        }
        private bool isRobotTwoBeingRecorded;
        public bool IsRobotTwoBeingRecorded { 
            get => isRobotTwoBeingRecorded;
            set
            {
                isRobotTwoBeingRecorded = value;
                DeterminePosChange(value);
                NotifyPropertyChanged();
            }
        }
        private bool isRobotThreeBeingRecorded;
        public bool IsRobotThreeBeingRecorded {
            get => isRobotThreeBeingRecorded;
            set
            {
                isRobotThreeBeingRecorded = value;
                DeterminePosChange(value);
                NotifyPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        ///     Keeps track of where the add or remove button should be inserted
        /// </summary>
        public int RobotsRecordingUIInsertPos { get; set; }
        public ICommand AddRobotCMD { get; set; }
        public ICommand RemoveRobotCMD { get; set; }
        public ICommand FinishedRecordingCMD { get; set; }
        public FinalizeRecordingViewModel(MatchSetupPage _performanceSetupPage, FinalizeRecordingPage _finalizePerformancePage, StackLayout _addRemoveBtnLayout, Match _performance)
        {
            FinalizeRecordingPage = _finalizePerformancePage;
            performanceSetupPage = _performanceSetupPage;
            //_finalizePerformancePage.RobotOneIdEntry.TextChanged   += (e, a) => Performance.RobotOneId   = a.NewTextValue;
            //_finalizePerformancePage.RobotTwoIdEntry.TextChanged   += (e, a) => Performance.RobotTwoId   = a.NewTextValue;
            //_finalizePerformancePage.RobotThreeIdEntry.TextChanged += (e, a) => Performance.RobotThreeId = a.NewTextValue;

            //_finalizePerformancePage.RobotOneInfoEditor.TextChanged   += (e, a) => Performance.RobotOneInfo   = a.NewTextValue;
            //_finalizePerformancePage.RobotTwoInfoEditor.TextChanged   += (e, a) => Performance.RobotTwoInfo   = a.NewTextValue;
            //_finalizePerformancePage.RobotThreeInfoEditor.TextChanged += (e, a) => Performance.RobotThreeInfo = a.NewTextValue;

            //_finalizePerformancePage.PerformanceCommentsEditor.TextChanged += (e, a) => Performance.Comments = a.NewTextValue;

            Performance = _performance;
            AddRemoveBtnLayout = _addRemoveBtnLayout;
            AddRobotCMD = new Command(RevealNextRobotUI);
            RemoveRobotCMD = new Command(HideLastRobotUI);
            FinishedRecordingCMD = new Command(FinishRecording);
        }

        /// <summary>
        ///     Starts the finishing process for this performance.
        /// </summary>
        private async void FinishRecording()
        {
            // Getting the robot Ids
            Performance.RobotOneId = FinalizeRecordingPage.RobotOneIdEntry.Text;
            Performance.RobotTwoId = FinalizeRecordingPage.RobotTwoIdEntry.Text;
            Performance.RobotThreeId = FinalizeRecordingPage.RobotThreeIdEntry.Text;
            // Getting the robot info
            Performance.RobotOneInfo = FinalizeRecordingPage.RobotOneInfoEditor.Text;
            Performance.RobotTwoInfo = FinalizeRecordingPage.RobotTwoInfoEditor.Text;
            Performance.RobotThreeInfo = FinalizeRecordingPage.RobotThreeInfoEditor.Text;
            // Getting the performance info
            Performance.Comments = FinalizeRecordingPage.PerformanceCommentsEditor.Text;
            // Saving to the database

            await App.Current.MainPage.Navigation.PopModalAsync();

            await Data.DatabaseService.Provider.SaveMatchToLocalDBAsync(Performance);
        }

        /// <summary>
        ///     Reveals the next robot UI unless all three are visible.
        /// </summary>
        private void RevealNextRobotUI()
        {
            switch (RobotsRecordingUIInsertPos)
            {
                case REVEAL_FIRST_ROBOT_UI:
                    IsRobotOneBeingRecorded = true;
                    break;
                case REVEAL_SECOND_ROBOT_UI:
                    IsRobotTwoBeingRecorded = true;
                    break;
                case REVEAL_THIRD_ROBOT_UI:
                    IsRobotThreeBeingRecorded = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///     Hides the last robot UI unless all are hidden.
        /// </summary>
        private void HideLastRobotUI()
        {
            switch (RobotsRecordingUIInsertPos)
            {
                case HIDE_FIRST_ROBOT:
                    IsRobotOneBeingRecorded = false;
                    break;
                case HIDE_SECOND_ROBOT:
                    IsRobotTwoBeingRecorded = false;
                    break;
                case HIDE_THIRD_ROBOT:
                    IsRobotThreeBeingRecorded = false;
                    break;
                default:
                    break;
            }
        }
       
        /// <summary>
        ///     Changes the position of the add / remove button in the UI.
        /// </summary>
        private void DeterminePosChange(bool _value)
        {
            if (_value)
            {
                RobotsRecordingUIInsertPos += INSERT_POS_CHANGE_AMT;
            }
            else
            {
                RobotsRecordingUIInsertPos -= INSERT_POS_CHANGE_AMT;
            }
        }
    }
}
