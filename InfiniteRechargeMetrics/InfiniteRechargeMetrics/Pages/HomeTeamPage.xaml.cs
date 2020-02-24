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
            HomeTeam = DatabaseService.Provider.GetHomeTeam();

            // if their is no home team set, provide the ui to do so
            if (HomeTeam == null)
            {
                Content = new SetHomeTeamTemplate(Content);                
            }
            // there is a home team set so show their hometeams stats
            else
            {
                Content = new TeamStatsTemplate(HomeTeam);
                BindingContext = new HomeTeamViewModel(HomeTeam);
            }
        }
    }
}