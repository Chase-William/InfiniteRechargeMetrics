using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            searchBar.TextChanged += SearchBar_TextChanged;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
                RobotsViewModel.RobotsSearchResults = new ObservableCollection<Robot>(await DatabaseService.Provider.GetSearchResultsForRobotIdAsync(e.NewTextValue));
            else
                RobotsViewModel.RobotsSearchResults = new ObservableCollection<Robot>(await DatabaseService.Provider.GetAllRobotsAsync());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RobotsViewModel.RefreshCollection();
        }
    }
}