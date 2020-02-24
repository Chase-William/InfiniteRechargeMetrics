using InfiniteRechargeMetrics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RobotsPages : ContentPage
    {
        public RobotsViewModel RobotsViewModel { get; set; }
        public RobotsPages()
        {
            InitializeComponent();
            RobotsViewModel = new RobotsViewModel();
            BindingContext = RobotsViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RobotsViewModel.RefreshCollection();
        }
    }
}