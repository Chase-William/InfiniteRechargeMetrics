using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.ViewModels.HomeVM
{
    /// <summary>
    ///     ViewModel for the HomeTeamPage class
    /// </summary>
    public class HomeTeamViewModel
    {
        public ICommand EditThisTeamCMD { get; set; }
        public Team CurrentTeam { get; set; }

        public HomeTeamViewModel(Team _team)
        {
            CurrentTeam = _team;
            EditThisTeamCMD = new Command(EditCurrentTeam);
        }

        /// <summary>
        ///     Launches the EditTeamPage as a modal
        /// </summary>
        private void EditCurrentTeam()
        {
            App.Current.MainPage.Navigation.PushModalAsync(new EditTeamPage(CurrentTeam));
        }
    }
}
