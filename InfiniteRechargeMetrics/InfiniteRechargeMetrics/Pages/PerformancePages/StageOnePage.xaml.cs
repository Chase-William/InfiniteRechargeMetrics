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
        }
    }
}