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
        private StageCompletionManager stageCompletionManager;

        public StageTwoPage(Performance _performance, StageCompletionManager _stageCompletionManager)
        {
            InitializeComponent();
            performance = _performance;
            stageCompletionManager = _stageCompletionManager;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = BindingContext ?? new StageTwoViewModel(this, performance, stageCompletionManager);
        }
    }
}