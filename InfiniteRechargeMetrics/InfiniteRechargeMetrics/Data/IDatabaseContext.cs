using InfiniteRechargeMetrics.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteRechargeMetrics.Data
{
    interface IDatabaseContext
    {
        Task SaveTeamToLocalDBAsync(Team _team);
        Task SaveMatchToLocalDBAsync(Match _performance);        

        Task<List<Match>> GetAllMatchesForTeamAsync(string _teamName);
        Task<List<Point>> GetPointsFromMatchesAsync(List<Match> _matches);

        Task<string[]> GetAllTeamsIdAndAliasConcatenatedAsync();
        Task<string[]> GetAllRobotIdAndAliasConcatenatedAsync();
        
        Task<Team> GetHomeTeamAsync();
        Team GetHomeTeam();
        Task<Team> GetTeamAsync(string _teamId);
        Task RemoveHomeStatusFromTeamAsync(string _teamId);

        Task<bool> DoesMatchExistAsync(string _matchId);
        Task<bool> DoesTeamExistAsync(string _teamId);        
        Task<Tuple<bool, string>> DoesRobotExistAsync(params string[] _robotId);
    }
}
