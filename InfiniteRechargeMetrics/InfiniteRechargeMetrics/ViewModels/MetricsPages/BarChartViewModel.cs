using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.Models;
using System.Collections.Generic;

namespace InfiniteRechargeMetrics.ViewModels.MetricsPages
{
    class BarChartViewModel : NotifyClass
    {
        private List<Match> matches = new List<Match>();
        public List<Match> Matches { 
            get => matches;
            set
            {
                matches = value;
                NotifyPropertyChanged();
            } 
        }
       
        private int selectedIndex;
        public int SelectedIndex { 
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                Match = Matches[value];
            } 
        }

        private Match match;
        public Match Match { 
            get => match; 
            set
            {
                match = value;
                Points = DatabaseService.Provider.GetPointsFromMatch(Match.MatchId);
            } 
        }

        private List<Point> points;
        public List<Point> Points { 
            get => points;
            set
            {
                points = value;
                NotifyPropertyChanged();
            } 
        }

        public BarChartViewModel()
        {
            Matches = DatabaseService.Provider.GetAllMatches();
        }        
    }
}
