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
    public partial class HomeTeamPage : ContentPage
    {
        public HomeTeamPage()
        {
            InitializeComponent();            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            TeamStats.TempNameVar.Text = "Example Team Name";   // TO BE REMOVED LATER IN DEVELOPMENT

            // Calling the function for loading performances from the database.            
            await TeamStats.OnLoadPerformancesAsync();
            //await TeamStats.OnLoadMatchesAsync();
        }
    }
}