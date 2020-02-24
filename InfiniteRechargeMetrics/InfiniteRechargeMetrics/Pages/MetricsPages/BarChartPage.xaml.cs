using InfiniteRechargeMetrics.ViewModels.MetricsPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages.MetricsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarChartPage : ContentPage
    {
        public BarChartPage()
        {
            InitializeComponent();
            BindingContext = new BarChartViewModel();
        }
    }
}