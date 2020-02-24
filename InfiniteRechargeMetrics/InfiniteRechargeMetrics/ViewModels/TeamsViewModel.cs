using InfiniteRechargeMetrics.Models;
using System.Collections.Generic;
using InfiniteRechargeMetrics.Data;
using System.Windows.Input;
using Xamarin.Forms;
using InfiniteRechargeMetrics.Pages;
using System.Collections.ObjectModel;

namespace InfiniteRechargeMetrics.ViewModels
{
    public enum SelectionState { ViewDetails, Delete, Edit }

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
        public ICommand RefreshResultsCMD => new Command(RefreshCollection);

        // Determines what should be done when an item is selected
        public SelectionState SelectedState { get; set; }

        public ICommand DeleteTeamsCMD => new Command(() =>
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

        public ICommand EditTeamCMD => new Command(() =>
        {
            if (SelectedState == SelectionState.Edit)
            {
                FrameListViewBorderColor = Color.Transparent;
                SelectedState = SelectionState.ViewDetails;
            }
            else
            {
                FrameListViewBorderColor = Color.Yellow;
                SelectedState = SelectionState.Edit;
            }
        });

        public ICommand CreateNewTeamCMD => new Command(() =>
        {
            SelectedState = SelectionState.ViewDetails;
            App.Current.MainPage.Navigation.PushModalAsync(new EditTeamPage(new Team()));
        });
        
        public TeamsViewModel()
        {
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
                    EditSelectedItem();
                    break;
                default:
                    break;
            }   
        }

        private void EditSelectedItem()
        {
            if (SelectedTeam == null) return;
            App.Current.MainPage.Navigation.PushModalAsync(new EditTeamPage(SelectedTeam));           
        }

        private async void DeleteSelectedItem()
        {
            await DatabaseService.Provider.RemoveTeamFromLocalDBAsync(SelectedTeam.TeamId);
            RefreshCollection();
        }

        public async void RefreshCollection()
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

    }
}
