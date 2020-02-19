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

            // So we need to set the bindingcontext during initialization otherwise our MasterRecordPerformance's clock
            //  wont display if we create a new context later.
            BindingContext = new StageTwoViewModel(this, performance, stageCompletionManager);
        }
    }
}