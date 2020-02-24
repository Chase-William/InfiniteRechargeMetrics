using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class RobotsViewModel : NotifyClass
    {
        #region Robot Queryies and List
        private ObservableCollection<Robot> robotsSearchResults;
        public ObservableCollection<Robot> RobotsSearchResults
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
        #endregion

        private Color frameListViewBorderColor;
        public Color FrameListViewBorderColor { 
            get => frameListViewBorderColor;
            set
            {
                frameListViewBorderColor = value;
                NotifyPropertyChanged();
            }
        }

        public SelectionState SelectedState { get; set; }

        public ICommand RefreshResultsCMD => new Command(async () =>
        {
            IsRefreshing = true;
            if (!string.IsNullOrEmpty(QueryString))
                RobotsSearchResults = new ObservableCollection<Robot>(await DatabaseService.Provider.GetSearchResultsForRobotIdAsync(QueryString));
            else
                RobotsSearchResults = new ObservableCollection<Robot>(await DatabaseService.Provider.GetAllRobotsAsync());
            IsRefreshing = false;
        });

        public ICommand DeleteRobotsCMD => new Command(() =>
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

        public ICommand EditRobotCMD => new Command(() =>
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

        public ICommand CreateNewRobotCMD => new Command(() =>
        {
            SelectedState = SelectionState.ViewDetails;
            App.Current.MainPage.Navigation.PushModalAsync(new EditRobotPage(new Robot()));
        });      

        public RobotsViewModel()
        {
        }


        /// <summary>
        ///     When a team is selected this will load a page to show all the details about that team.
        /// </summary>
        private void RobotSelected()
        {
            if (SelectedRobot == null) return;

            switch (SelectedState)
            {
                case SelectionState.ViewDetails:
                    App.Current.MainPage.Navigation.PushModalAsync(new RobotDetailsPage(SelectedRobot));
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
            if (SelectedRobot == null) return;
            App.Current.MainPage.Navigation.PushModalAsync(new EditRobotPage(SelectedRobot));
        }

        private async void DeleteSelectedItem()
        {
            await DatabaseService.Provider.RemoveRobotFromLocalDBAsync(SelectedRobot.RobotId);
            RefreshCollection();
        }

        public async void RefreshCollection()
        {
            try
            {
                IsRefreshing = true;
                if (!string.IsNullOrEmpty(QueryString))
                    RobotsSearchResults = new ObservableCollection<Robot>(await DatabaseService.Provider.GetSearchResultsForRobotIdAsync(QueryString));
                else
                    RobotsSearchResults = new ObservableCollection<Robot>(await DatabaseService.Provider.GetAllRobotsAsync());
                IsRefreshing = false;
            }
            catch { }
        }


        /// <summary>
        ///     Loads all the teams for the Teams listview/
        /// </summary>
        private async void LoadAllRobots()
        {
            RobotsSearchResults = new ObservableCollection<Robot>(await DatabaseService.Provider.GetAllRobotsAsync());
        }
    }
}
