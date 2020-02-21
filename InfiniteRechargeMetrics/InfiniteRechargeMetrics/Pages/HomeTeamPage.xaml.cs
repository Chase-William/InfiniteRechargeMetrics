using InfiniteRechargeMetrics.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfiniteRechargeMetrics.Templates;
using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.ViewModels;
using InfiniteRechargeMetrics.ViewModels.HomeVM;

namespace InfiniteRechargeMetrics.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeTeamPage : ContentPage
    {
        public Team HomeTeam { get; set; }

        public HomeTeamPage() 
        { 
            InitializeComponent();
            HomeTeam = DatabaseService.Provider.GetHomeTeam();
        }
        /// <summary>
        ///     Parameterized Contructor used by the APP on startup based off if a hometeam is set in the local database.
        /// </summary>
        public HomeTeamPage(Team _homeTeam) 
        { 
            InitializeComponent();
            HomeTeam = _homeTeam;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            // if their is a home team set, display the UI for the team
            if (HomeTeam == null)
            {
                Content = new SetHomeTeamTemplate();                
            }
            // otherwise there is no home team set, therefore display the set home team view
            else
            {
                Content = new TeamStatsTemplate(HomeTeam);
                BindingContext = new HomeTeamViewModel(HomeTeam);
            }
        }
    }
}