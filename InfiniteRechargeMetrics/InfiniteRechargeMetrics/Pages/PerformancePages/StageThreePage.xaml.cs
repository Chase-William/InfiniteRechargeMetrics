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
        public StageThreePage(Performance _performance)
        {
            InitializeComponent();
            performance = _performance;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new StageThreeViewModel(performance);
        }
    }
}