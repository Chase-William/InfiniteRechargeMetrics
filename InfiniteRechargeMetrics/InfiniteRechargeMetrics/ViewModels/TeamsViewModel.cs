using InfiniteRechargeMetrics.Models;
using System.Collections.Generic;
using InfiniteRechargeMetrics.Data;
using System.Windows.Input;
using Xamarin.Forms;
using InfiniteRechargeMetrics.Pages;
using System.Collections.ObjectModel;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class TeamsViewModel : NotifyClass
    {
        private ObservableCollection<Team> teamsSearchResults = new ObservableCollection<Team>();
        public ObservableCollection<Team> TeamsSearchResults {
            get => teamsSearchResults;
            set
            {
                teamsSearchResults = value;
                NotifyPropertyChanged();
            }
        }
        private Team selectedTeam;
        public Team SelectedTeam { 
            get => selectedTeam; 
            set
            {
                selectedTeam = value;
                TeamSelected();
                selectedTeam = null;
                NotifyPropertyChanged();
            }
        }
        public string QueryString { get; set; }

        private bool isRefreshing;
        public bool IsRefreshing { 
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                NotifyPropertyChanged();
            }
        }

        private Color frameListViewBorderColor = Color.Transparent;
        public Color FrameListViewBorderColor { 
            get => frameListViewBorderColor;
            set
            {
                frameListViewBorderColor = value;
                NotifyPropertyChanged();
            } 
        } 
        public ICommand RefreshResultsCMD => new Command(async () =>
        {
            RefreshCollection();
        });

        // Determines what should be done when an item is selected
        public SelectionState SelectedState { get; set; } = SelectionState.ViewDetails;

        public ICommand DeleteTeams => new Command(() =>
        {
            if (SelectedState == SelectionState.Delete)
            {
                FrameListViewBorderColor = Color.Transparent;
                SelectedState = SelectionState.ViewDetails;
            }
            else
            {
                FrameListViewBorderColor = Color.Red;
                SelectedState = SelectionState.Delete;
            }
        });


        public TeamsViewModel()
        {
            LoadAllTeams();
            TeamsSearchResults.CollectionChanged += TeamsSearchResults_CollectionChanged;
        }

        private void TeamsSearchResults_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var test = e.Action;
        }


        /// <summary>
        ///     When a team is selected this will load a page to show all the details about that team.
        /// </summary>
        private void TeamSelected()
        {
            if (selectedTeam == null) return;

            switch (SelectedState)
            {
                case SelectionState.ViewDetails:
                    App.Current.MainPage.Navigation.PushModalAsync(new ViewTeamDetailsPage(SelectedTeam));
                    break;
                case SelectionState.Delete:
                    DeleteSelectedItem();
                    break;
                case SelectionState.Edit:

                    break;
                default:
                    break;
            }   
        }

        private async void DeleteSelectedItem()
        {
            await DatabaseService.Provider.RemoveTeamFromLocalDBAsync(SelectedTeam.TeamId);
            RefreshCollection();


        }

        private async void RefreshCollection()
        {
            try
            {
                IsRefreshing = true;
                if (!string.IsNullOrEmpty(QueryString))
                    TeamsSearchResults = new ObservableCollection<Team>(await DatabaseService.Provider.GetSearchResultsForTeamAliasAsync(QueryString));
                else
                    TeamsSearchResults = new ObservableCollection<Team>(await DatabaseService.Provider.GetAllTeamsAsync());
                IsRefreshing = false;
            }
            catch { }
        }

        /// <summary>
        ///     Loads all the teams for the Teams listview/
        /// </summary>
        private async void LoadAllTeams()
        {
            TeamsSearchResults = new ObservableCollection<Team>(await DatabaseService.Provider.GetAllTeamsAsync());
        }

        public enum SelectionState { Delete, ViewDetails, Edit }
    }
}
