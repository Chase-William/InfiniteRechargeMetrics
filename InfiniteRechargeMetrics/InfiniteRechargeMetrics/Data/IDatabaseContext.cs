using InfiniteRechargeMetrics.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteRechargeMetrics.Data
{
    interface IDatabaseContext
    {
        Task SaveTeamToLocalDBAsync(Team _team, bool _setAsHomeTeam = false);
        Task SaveMatchToLocalDBAsync(Match _match);
        Task SaveRobotToLocalDbASync(Robot _robot);
        Task RemoveTeamFromLocalDBAsync(string _teamId);
        Task OverwriteTeamDataWithNewTeamAsync(Team _toBeOverwrittenTeam, Team _newTeam, bool _setAsHomeTeam = false);
        Task OverwriteRobotDataWithNewRobotAsync(Robot _toBeOverwrittenRobot, Robot _newRobot);
        Task<List<Match>> GetMatchesFromTeamAsync(string _teamName);
        Task<List<Point>> GetPointsFromMatchesAsync(List<Match> _matches);
        List<Point> GetPointsFromMatch(string _matchId);

        List<Match> GetAllMatches();

        Task<string[]> GetAllTeamsIdAndAliasConcatenatedAsync();
        Task<string[]> GetAllRobotIdAsync();
        Task<Robot> GetRobotAsync(string _robotId);
        Task<List<Team>> GetAllTeamsAsync();
        Task<List<Robot>> GetAllRobotsAsync();
        
        Task<Team> GetHomeTeamAsync();
        Team GetHomeTeam();
        Task<Team> GetTeamAsync(string _teamId);
        Task RemoveHomeStatusFromTeamAsync(string _teamId);
        Task<Team> SetHomeStatusForTeamAsync(string _newHomeTeamId);
        Task RemoveRobotFromLocalDBAsync(string _robotId);

        Task<bool> DoesMatchExistAsync(string _matchId);
        Task<bool> DoesTeamExistAsync(string _teamId);        
        Task<Tuple<bool, string>> DoesRobotExistAsync(params string[] _robotId);

        Task<List<Team>> GetSearchResultsForTeamAliasAsync(string _query);
        Task<List<Robot>> GetSearchResultsForRobotIdAsync(string _query);
       

        int GetTeamMatchCount(string _teamId);
    }
}
