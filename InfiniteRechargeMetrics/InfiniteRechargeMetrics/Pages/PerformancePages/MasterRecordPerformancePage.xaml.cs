using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.PerformancePages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages.PerformancePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterRecordPerformancePage : TabbedPage
    {
        public Performance Performance { get; set; }

        public MasterRecordPerformancePage(Performance _performance)
        {
            Performance = _performance;
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Children.Add(new StageOnePage(Performance));
            this.Children.Add(new StageTwoPage(Performance));
            this.Children.Add(new StageThreePage(Performance));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }
    }
}