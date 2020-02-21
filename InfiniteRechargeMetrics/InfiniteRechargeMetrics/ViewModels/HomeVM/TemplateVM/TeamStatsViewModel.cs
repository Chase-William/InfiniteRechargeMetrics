using InfiniteRechargeMetrics.Models;
using System.Collections.ObjectModel;
using InfiniteRechargeMetrics.Data;
using Point = InfiniteRechargeMetrics.Models.Point;
using System.Linq;

namespace InfiniteRechargeMetrics.ViewModels.HomeVM
{
    public class TeamStatsViewModel
    {
        public Team CurrentTeam { get; set; }

        public ObservableCollection<Match> Matches { get; set; }

        public ObservableCollection<Point> Points { get; set; }

        public TeamStatsViewModel(Team _team)
        {            
            CurrentTeam = _team;

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                Matches = new ObservableCollection<Match>(await DatabaseService.Provider.GetAllMatchesForTeamAsync(CurrentTeam.TeamId));
                Points = new ObservableCollection<Point>(await DatabaseService.Provider.GetPointsFromMatches(Matches.ToList()));
            }
            catch { }
        }
    }
}
