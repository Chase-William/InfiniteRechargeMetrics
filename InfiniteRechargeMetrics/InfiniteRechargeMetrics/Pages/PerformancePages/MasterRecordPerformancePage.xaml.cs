using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.PerformancePages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages.PerformancePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterRecordPerformancePage : TabbedPage
    {        
        public MasterRecordPerformancePage(Performance _performance)
        {
            InitializeComponent();
            this.Children.Add(new StageOnePage(_performance));
            this.Children.Add(new StageTwoPage(_performance));
            this.Children.Add(new StageThreePage(_performance));
        }
    }
}