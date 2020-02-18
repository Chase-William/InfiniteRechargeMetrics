using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     Class tasked with managing the completion status of different pages
    /// </summary>
    public class StageCompletionManager : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string _propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propName));
        }
    }
}
