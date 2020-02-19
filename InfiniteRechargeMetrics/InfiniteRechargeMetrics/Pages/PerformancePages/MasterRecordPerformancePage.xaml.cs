using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels;
using System;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages.PerformancePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterRecordPerformancePage : TabbedPage
    {
        /// <summary>
        ///     Update the clock every second (1000ms)
        /// </summary>
        private const int UPDATE_CLOCK_INTERVAL = 1000;
        private readonly PerformanceSetupPage performanceSetupPage;
        public Performance Performance { get; set; }
        public StageCompletionManager StageCompletionManager { get; set; } = new StageCompletionManager();        
        private Timer clockTimer = new Timer();


        public MasterRecordPerformancePage(PerformanceSetupPage _performanceSetupPage, Performance _performance)
        {
            InitializeComponent();
            performanceSetupPage = _performanceSetupPage;
            Performance = _performance;
            clockTimer.Interval = UPDATE_CLOCK_INTERVAL;
            clockTimer.Elapsed += delegate
            {
                TimeSpan time = new TimeSpan(StageViewModelBase.Stopwatch.ElapsedTicks);
                Device.BeginInvokeOnMainThread(() => Title = $"{time.Minutes.ToString("00")}:{time.Seconds.ToString("00")}");                
            };
        }

        /// <summary>
        ///     Start the clock timer
        /// </summary>
        public void StartClockTimer() => clockTimer.Start();
        /// <summary>
        ///     Stops the clock timer
        /// </summary>
        public void StopClockTimer()
        {
            clockTimer.Stop();
            Device.BeginInvokeOnMainThread(() => Title = "Recording Finished");
        }

        protected override void OnAppearing()
        {       
            base.OnAppearing();
            this.Children.Add(new StageOnePage(this, Performance, StageCompletionManager));
            this.Children.Add(new StageTwoPage(Performance, StageCompletionManager));
            this.Children.Add(new StageThreePage(Performance, StageCompletionManager));
            this.Children.Add(new FinalizeRecordingPage(performanceSetupPage, Performance));
        }
    }
}