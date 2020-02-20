using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages.MatchPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageTwoPage : ContentPage
    {
        private Match Match { get; set; }
        private StageCompletionManager stageCompletionManager;

        public StageTwoPage(Match _performance, StageCompletionManager _stageCompletionManager)
        {
            InitializeComponent();
            Match = _performance;
            stageCompletionManager = _stageCompletionManager;

            // So we need to set the bindingcontext during initialization otherwise our MasterRecordPerformance's clock
            //  wont display if we create a new context later.
            BindingContext = new StageTwoViewModel(this, Match, stageCompletionManager);
        }
    }
}