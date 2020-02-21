using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     Class tasked with managing the completion status of different pages
    /// </summary>
    public class StageCompletionManager : NotifyClass
    {
        /// <summary>
        ///     Each of the stage property track the state of the stages.
        ///     When changed listeners will be notified.
        /// </summary>
        #region Stage States      
        private bool isStageOneComplete;
        public bool IsStageOneComplete
        {
            get => isStageOneComplete;
            set
            {
                isStageOneComplete = value;
                NotifyPropertyChanged(nameof(IsStageOneComplete));
            }
        }        
        private bool isStageTwoComplete;
        public bool IsStageTwoComplete
        {
            get => isStageTwoComplete;
            set
            {
                isStageTwoComplete = value;
                NotifyPropertyChanged(nameof(IsStageTwoComplete));
            }
        }
        private bool isStageThreeComplete;
        public bool IsStageThreeComplete
        {
            get => isStageThreeComplete;
            set
            {
                isStageThreeComplete = value;
                NotifyPropertyChanged(nameof(IsStageThreeComplete));
            }
        }
        #endregion       

        /// <summary>
        ///     Property that signals whether the match is out of time (2:30)
        /// </summary>
        private bool isTimeOut;
        public bool IsTimeOut
        {
            get => isTimeOut;
            set
            {
                isTimeOut = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsRecording { get; set; }

        /// <summary>
        ///     Method for setting the recording variable
        /// </summary>
        public void SetRecordingState(bool _recording)
        {
            IsRecording = _recording;
            NotifyPropertyChanged(nameof(IsRecording));
        }
    }
}
