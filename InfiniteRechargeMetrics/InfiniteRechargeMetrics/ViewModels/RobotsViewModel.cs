using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class RobotsViewModel : NotifyClass
    {
        private List<Robot> robotsSearchResults;
        public List<Robot> RobotsSearchResults
        {
            get => robotsSearchResults;
            set
            {
                robotsSearchResults = value;
                NotifyPropertyChanged();
            }
        }
        private Robot selectedRobot;
        public Robot SelectedRobot
        {
            get => selectedRobot;
            set
            {
                selectedRobot = value;
                RobotSelected();
                selectedRobot = null;
                NotifyPropertyChanged();
            }
        }

        public string QueryString { get; set; }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand RefreshResultsCMD => new Command(async () =>
        {
            IsRefreshing = true;
            if (!string.IsNullOrEmpty(QueryString))
                RobotsSearchResults = await DatabaseService.Provider.GetSearchResultsForRobotIdAsync(QueryString);
            else
                RobotsSearchResults = await DatabaseService.Provider.GetAllRobotsAsync();
            IsRefreshing = false;
        });

        public RobotsViewModel()
        {
            LoadAllRobots();
        }

        /// <summary>
        ///     When a team is selected this will load a page to show all the details about that team.
        /// </summary>
        private void RobotSelected()
        {
            //if (selectedTeam != null)
            //    App.Current.MainPage.Navigation.PushModalAsync(new ViewTeamDetailsPage(SelectedTeam));
        }

        /// <summary>
        ///     Loads all the teams for the Teams listview/
        /// </summary>
        private async void LoadAllRobots()
        {
            RobotsSearchResults = await DatabaseService.Provider.GetAllRobotsAsync();
        }
    }
}
