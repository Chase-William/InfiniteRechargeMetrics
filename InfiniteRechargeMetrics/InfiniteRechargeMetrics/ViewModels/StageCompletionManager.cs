using System;
using System.Runtime.CompilerServices;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     Class tasked with managing the completion status of different pages
    /// </summary>
    public class StageCompletionManager
    {
        #region Stage State Managers
        private static event Action<bool, string> StageCompletionChanged;
        private static bool isStageOneComplete;
        public static bool IsStageOneComplete
        {
            get => isStageOneComplete;
            set
            {
                isStageOneComplete = value;
                StageCompletionChanged?.Invoke(value, nameof(IsStageOneComplete));
            }
        }
        private static bool isStageTwoComplete;
        public static bool IsStageTwoComplete
        {
            get => isStageTwoComplete;
            set
            {
                isStageTwoComplete = value;
                StageCompletionChanged?.Invoke(value, nameof(IsStageTwoComplete));
            }
        }
        private static bool isStageThreeComplete;
        public static bool IsStageThreeComplete
        {
            get => isStageThreeComplete;
            set
            {
                isStageThreeComplete = value;
                StageCompletionChanged?.Invoke(value, nameof(IsStageThreeComplete));
            }
        }
        #endregion

        // Event triggers when the corresponding property changes
        public static event Action StageOneCompletionStatusChanged;
        public static event Action StageTwoCompletionStatusChanged;
        public static event Action StageThreeCompletionStatusChanged;

        static StageCompletionManager()
        {
            StageCompletionChanged += StageCompletionManager_StageCompletionChanged;
        }

        /// <summary>
        ///     Determines specifically which property was changed and triggers an event
        /// </summary>
        private static void StageCompletionManager_StageCompletionChanged(bool obj, [CallerMemberName] string _propName = "")
        {
            switch (_propName)
            {
                case nameof(IsStageOneComplete):
                    StageOneCompletionStatusChanged?.Invoke();
                    break;
                case nameof(IsStageTwoComplete):
                    StageTwoCompletionStatusChanged?.Invoke();
                    break;
                case nameof(IsStageThreeComplete):
                    StageThreeCompletionStatusChanged?.Invoke();
                    break;
                default:
                    break;
            }
        }
    }
}
