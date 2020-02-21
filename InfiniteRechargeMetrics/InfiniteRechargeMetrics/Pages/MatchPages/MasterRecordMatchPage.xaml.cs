using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels;
using System;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages.MatchPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterRecordMatchPage : TabbedPage
    {
        /// <summary>
        ///     Update the clock every second (1000ms)
        /// </summary>
        private const int UPDATE_CLOCK_INTERVAL = 1000;
        private readonly MatchSetupPage matchSetupPage;
        public Match Match { get; set; }
        public StageCompletionManager StageCompletionManager { get; set; } = new StageCompletionManager();        
        private Timer clockTimer = new Timer();


        public MasterRecordMatchPage(MatchSetupPage _matchSetupPage, Match _performance)
        {
            InitializeComponent();
            matchSetupPage = _matchSetupPage;
            Match = _performance;
            clockTimer.Interval = UPDATE_CLOCK_INTERVAL;
            clockTimer.Elapsed += delegate
            {
                TimeSpan time = new TimeSpan(StageViewModelBase.Stopwatch.ElapsedTicks);
                Device.BeginInvokeOnMainThread(() => Title = $"{time.Minutes.ToString("00")}:{time.Seconds.ToString("00")}");                
            };

            this.Children.Add(new StageOnePage(this, Match, StageCompletionManager));
            this.Children.Add(new StageTwoPage(Match, StageCompletionManager));
            this.Children.Add(new StageThreePage(Match, StageCompletionManager));
            this.Children.Add(new FinalizeRecordingPage(matchSetupPage, Match));
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
    }
}