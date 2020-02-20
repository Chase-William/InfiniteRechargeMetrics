using InfiniteRechargeMetrics.ViewModels;
using InfiniteRechargeMetrics.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfiniteRechargeMetrics.SharedCustomViews;
using System;

namespace InfiniteRechargeMetrics.Pages.MatchPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageOnePage : ContentPage
    {
        private Match Match { get; set; }
        private readonly StageCompletionManager stageCompletionManager;
        public MasterRecordMatchPage MasterMatchPage { get; set; }

        public StageOnePage(MasterRecordMatchPage _masterMatchPage, Match _match, StageCompletionManager _stageCompletionManager)
        {
            InitializeComponent();
            Match = _match;
            stageCompletionManager = _stageCompletionManager;
            MasterMatchPage = _masterMatchPage;

            BindingContext = new StageOneViewModel(this, Match, stageCompletionManager);
        }

        private void SteppedSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            ((StageOneViewModel)BindingContext).RobotsMovedFromSpawnPoints = (int)Math.Round(e.NewValue) * 5;
        }

        // jeeze man, maybe later
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