using InfiniteRechargeMetrics.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamDetails : ContentPage
    {
        /// <summary>
        ///     A collection of the all the performances the choosen team has.
        /// </summary>
        public ObservableCollection<Performance> Performances { get; set; }

        public TeamDetails()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // Calling the function for loading performances from the database.
            await TeamStats.OnLoadPerformancesAsync();
            // await TeamStats.OnLoadMatchesAsync();
        }
    }
}