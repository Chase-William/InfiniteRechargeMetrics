using InfiniteRechargeMetrics.Models;
using System.Collections.ObjectModel;
using InfiniteRechargeMetrics.Data;
using Point = InfiniteRechargeMetrics.Models.Point;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace InfiniteRechargeMetrics.ViewModels.HomeVM
{
    public class TeamStatsViewModel : NotifyClass
    {
        public Team CurrentTeam { get; set; }

        private ObservableCollection<Match> matches;
        public ObservableCollection<Match> Matches {
            get => matches;
            set
            {
                matches = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Point> Points { get; set; }

        public ICommand SendEmailCMD => new Command(async () => {

            var current = Connectivity.NetworkAccess;

            // If we have wifi:
            if (current == NetworkAccess.Internet)
            {
                if (App.GoogleUser == null || string.IsNullOrEmpty(App.GoogleUser?.Email))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Please login to use gmail.", "OK");
                    return;
                }
                List<string> emails = new List<string>();
                emails.Add(App.GoogleUser.Email);
                await EmailService.SendEmail("Team's Match Data", Newtonsoft.Json.JsonConvert.SerializeObject(Matches), emails);
            }
            // no wifi
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must be connected to wifi to send your data.", "OK");
                return;
            }            
        });

        public TeamStatsViewModel(Team _team)
        {            
            CurrentTeam = _team;               
            LoadData();

        }

        private async void LoadData()
        {
            try
            {
                Matches = new ObservableCollection<Match>(await DatabaseService.Provider.GetMatchesFromTeamAsync(CurrentTeam.TeamId));                
                Points = new ObservableCollection<Point>(await DatabaseService.Provider.GetPointsFromMatchesAsync(Matches.ToList()));




                // populating our matches with data
                foreach (Match match in Matches)
                {
                    foreach (Point point in Points)
                    {
                        if (point.MatchId == match.MatchId)
                        {
                            switch (point.GetPointType())
                            {
                                case PointType.AutomonousLow:                                
                                case PointType.AutomonousUpper:                                   
                                case PointType.AutomonousSmall:
                                    match.AutonomousPortPoints.Add(point);
                                    break;
                                case PointType.StageOneLow:
                                case PointType.StageOneUpper:
                                case PointType.StageOneSmall:
                                    match.StageOnePortPoints.Add(point);
                                    break;
                                case PointType.StageTwoLow:
                                case PointType.StageTwoUpper:
                                case PointType.StageTwoSmall:
                                    match.StageTwoPortPoints.Add(point);
                                    break;
                                case PointType.StageThreeLow:
                                case PointType.StageThreeUpper:
                                case PointType.StageThreeSmall:
                                    match.StageThreePortPoints.Add(point);
                                    break;                                
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}
