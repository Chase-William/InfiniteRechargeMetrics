using System;
using System.ComponentModel;
using System.Timers;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class MasterRecordPerformanceViewModel
    {
        /// <summary>
        ///     Update the clock every second (1000ms)
        /// </summary>
        private const int UPDATE_CLOCK_INTERVAL = 1000;

        public Timer ClockTimer { get; set; } = new Timer();

        public string DynamicTitle { get; set; }

        public MasterRecordPerformanceViewModel() 
        {
            ClockTimer.Interval = UPDATE_CLOCK_INTERVAL;
            ClockTimer.Elapsed += delegate
            {
                TimeSpan time = new TimeSpan(StageViewModelBase.Stopwatch.ElapsedTicks);
                DynamicTitle = $"{time.ToString("T")}";
            };
        }
    }
}
