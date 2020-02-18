using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages.PerformancePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageThreePage : ContentPage
    {
        private Performance performance;
        private StageCompletionManager stageCompletionManager;
        public StageThreePage(Performance _performance, StageCompletionManager _stageCompletionManager)
        {
            InitializeComponent();
            performance = _performance;
            stageCompletionManager = _stageCompletionManager;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = BindingContext ?? new StageThreeViewModel(this, performance, stageCompletionManager);
        }
    }
}