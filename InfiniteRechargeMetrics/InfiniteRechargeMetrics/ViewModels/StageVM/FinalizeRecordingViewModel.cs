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

        public Match Match { get; set; }
        public StackLayout AddRemoveBtnLayout { get; set; }
        //public FinalizeRecordingPage FinalizeRecordingPage { get; set; }

        #region Robots Visibility Props

        /// <summary>
        ///     These properties are used to determine how many robot layouts are visible.
        ///     They are used in bindings for the UI and also change the RobotsRecordingUIInsertPos value;        
        /// </summary>

        //private bool isRobotOneBeingRecorded;
        //public bool IsRobotOneBeingRecorded {
        //    get => isRobotOneBeingRecorded; 
        //    set
        //    {
        //        isRobotOneBeingRecorded = value;
        //        DeterminePosChange(value);
        //        NotifyPropertyChanged();
        //    }
        //}
        //private bool isRobotTwoBeingRecorded;
        //public bool IsRobotTwoBeingRecorded { 
        //    get => isRobotTwoBeingRecorded;
        //    set
        //    {
        //        isRobotTwoBeingRecorded = value;
        //        DeterminePosChange(value);
        //        NotifyPropertyChanged();
        //    }
        //}
        //private bool isRobotThreeBeingRecorded;
        //public bool IsRobotThreeBeingRecorded {
        //    get => isRobotThreeBeingRecorded;
        //    set
        //    {
        //        isRobotThreeBeingRecorded = value;
        //        DeterminePosChange(value);
        //        NotifyPropertyChanged();
        //    }
        //}
        #endregion

        /// <summary>
        ///     Keeps track of where the add or remove button should be inserted
        /// </summary>
        public int RobotsRecordingUIInsertPos { get; set; }
        public ICommand FinishedRecordingCMD { get; set; }
        public FinalizeRecordingViewModel(Match _match)
        {
            Match = _match;
            FinishedRecordingCMD = new Command(FinishRecording);
        }

        /// <summary>
        ///     Starts the finishing process for this performance.
        /// </summary>
        private async void FinishRecording()
        {         
            await App.Current.MainPage.Navigation.PopModalAsync();
            await Data.DatabaseService.Provider.SaveMatchToLocalDBAsync(Match);
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
