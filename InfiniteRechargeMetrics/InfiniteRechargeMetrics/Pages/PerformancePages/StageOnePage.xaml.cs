using InfiniteRechargeMetrics.ViewModels;
using InfiniteRechargeMetrics.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfiniteRechargeMetrics.SharedCustomViews;
using System;

namespace InfiniteRechargeMetrics.Pages.PerformancePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageOnePage : ContentPage
    {
        private Performance performance;
        private StageCompletionManager stageCompletionManager;
        public MasterRecordPerformancePage MasterPerformancePage { get; set; }

        public StageOnePage(MasterRecordPerformancePage _masterPerformancePage, Performance _performance, StageCompletionManager _stageCompletionManager)
        {
            InitializeComponent();
            performance = _performance;
            stageCompletionManager = _stageCompletionManager;
            MasterPerformancePage = _masterPerformancePage;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = BindingContext ?? new StageOneViewModel(this, performance, stageCompletionManager);
            // Starts the animation of the start button
            // StartBtnAnimation();
        }

        private void SteppedSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            ((StageOneViewModel)BindingContext).RobotsMovedFromSpawnPoints = (int)Math.Round(e.NewValue) * 5;
        }

        //private void StartBtnAnimation()
        //{
        //    var lightToDark = false;
        //    StartBtn.Animate(
        //    "colorchange",
        //    x =>
        //    {
        //        if (lightToDark)
        //        {
        //            x = 1 - x;
        //        }
        //        var test = (int)(239 * x);
        //        StartBtn.BackgroundColor = Color.FromRgb(test, test, test);
        //    },
        //    length: 500,
        //    finished: delegate (double d, bool b)
        //    {
        //        if (!lightToDark)
        //            StartBtn.BackgroundColor = Color.FromRgb(239, 225, 112);
        //        else
        //            StartBtn.BackgroundColor = Color.FromRgb(249, 235, 122);
        //    },
        //    repeat: () =>
        //    {
        //        lightToDark = !lightToDark;
        //        return true;
        //    });
        //}
    }
}