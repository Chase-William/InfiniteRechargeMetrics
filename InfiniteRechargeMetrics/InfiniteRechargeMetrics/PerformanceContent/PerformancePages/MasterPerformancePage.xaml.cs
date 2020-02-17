using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.PerformanceContent.PerformancePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPerformancePage : ContentPage
    {
        public Performance Performance { get; set; }
        public MasterPerformanceViewModel MasterPerformanceViewModel { get; set; }
        public MasterPerformancePage(Performance _performance)
        {
            Performance = _performance;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MasterPerformanceViewModel = new MasterPerformanceViewModel(Performance);
            BindingContext = MasterPerformanceViewModel;
            ViewSwitcher.Children.AddVertical(MasterPerformanceViewModel.StageOneView);
            ViewSwitcher.Children.AddVertical(MasterPerformanceViewModel.StageTwoView);
            ViewSwitcher.Children.AddVertical(MasterPerformanceViewModel.StageThreeView);
            ViewSwitcher.RaiseChild(MasterPerformanceViewModel.StageOneView);
            TabHost.SelectedTabIndexChanged += TabHost_SelectedTabIndexChanged;
        }

        private void TabHost_SelectedTabIndexChanged(object sender, SelectedPositionChangedEventArgs e)
        {           
            switch ((int)e.SelectedPosition)
            {
                case 0:
                    ViewSwitcher.RaiseChild(MasterPerformanceViewModel.StageOneView);
                    break;
                case 1:
                    ViewSwitcher.RaiseChild(MasterPerformanceViewModel.StageTwoView);
                    break;
                case 2:
                    ViewSwitcher.RaiseChild(MasterPerformanceViewModel.StageThreeView);
                    break;
                default:
                    break;
            }
        }
    }
}