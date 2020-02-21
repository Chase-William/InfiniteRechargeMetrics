using InfiniteRechargeMetrics.Models;
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
    public partial class EditTeamPage : ContentPage
    {
        public Team CurrentTeam { get; set; }

        public EditTeamPage()
        {
            InitializeComponent();           
        }

        public EditTeamPage(Team _team)
        {
            InitializeComponent();
            CurrentTeam = _team;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new EditTeamViewModel(CurrentTeam);
        }
    }
}