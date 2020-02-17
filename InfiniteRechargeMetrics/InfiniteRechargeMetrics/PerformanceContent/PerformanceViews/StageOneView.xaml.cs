using InfiniteRechargeMetrics.ViewModels;
using InfiniteRechargeMetrics.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfiniteRechargeMetrics.SharedCustomViews;
using System;

namespace InfiniteRechargeMetrics.PerformanceContent.PerformanceViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageOneView : ContentView
    {
        private Performance performance;

        public StageOneView(Performance _performance)
        {
            InitializeComponent();
            performance = _performance;
            BindingContext = BindingContext ?? new StageOneViewModel(this, performance);
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    // We wanna check that the context hasnt already been set otherwise swiping in the tabbed page will cause a new viewmodel to be instaniated
        //    BindingContext = BindingContext ?? new StageOneViewModel(this, performance);
        //    // Starts the animation of the start button
        //    // StartBtnAnimation();
        //}

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