using InfiniteRechargeMetrics.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.PerformanceContent.PerformanceViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageThreeView : ContentView
    {
        private Performance performance;
        public StageThreeView(Performance _performance)
        {
            InitializeComponent();
            performance = _performance;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    BindingContext = new StageThreeViewModel(performance);
        //}
    }
}