using InfiniteRechargeMetrics.ViewModels;
using InfiniteRechargeMetrics.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages.PerformancePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageOnePage : ContentPage
    {
        private Performance performance;

        public StageOnePage(Performance _performance)
        {
            InitializeComponent();
            performance = _performance;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new StageOneViewModel(performance);

            // Starts the animation of the start button
            StartBtnAnimation();
        }

        private void StartBtnAnimation()
        {
            var lightToDark = false;
            StartBtn.Animate(
            "colorchange",
            x =>
            {
                if (lightToDark)
                {
                    x = 1 - x;
                }
                var test = (int)(239 * x);
                StartBtn.BackgroundColor = Color.FromRgb(test, test, test);
            },
            length: 500,
            finished: delegate (double d, bool b)
            {
                if (!lightToDark)
                    StartBtn.BackgroundColor = Color.FromRgb(239, 225, 112);
                else
                    StartBtn.BackgroundColor = Color.FromRgb(249, 235, 122);
            },
            repeat: () =>
            {
                lightToDark = !lightToDark;
                return true;
            });
        }
    }
}