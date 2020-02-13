using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages.PerformancePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageTwoPage : ContentPage
    {
        private Performance performance;
        public StageTwoPage(Performance _performance)
        {
            InitializeComponent();
            performance = _performance;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new StageTwoViewModel(performance);
        }
    }
}