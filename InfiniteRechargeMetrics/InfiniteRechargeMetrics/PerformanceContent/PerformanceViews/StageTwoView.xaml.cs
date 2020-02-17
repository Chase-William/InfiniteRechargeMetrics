using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.PerformanceContent.PerformanceViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageTwoView : ContentView
    {
        private Performance performance;
        public StageTwoView(Performance _performance)
        {
            InitializeComponent();
            performance = _performance;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    BindingContext = new StageTwoViewModel(performance);
        //}
    }
}