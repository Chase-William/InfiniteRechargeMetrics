using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteRechargeMetrics.Models;
using System.Linq;
using System;
/// <summary>
/// 
///     Utility class used to interact with our local database.
/// 
///     References:
///         https://github.com/praeclarum/sqlite-net/wiki/GettingStarted
/// 
/// </summary>
namespace InfiniteRechargeMetrics.Data
{
    public abstract class DatabaseService
    {
        public static Provider Provider { get => new Provider(); }
    }

    public class Provider : IDatabaseContext
    {        
        /// <summary>
        ///     Returns a list of performances.
        /// </summary>
        public async Task<List<Match>> GetMatchesFromTeamAsync(string _teamId)
        {                              
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Match>();            
            return await cn.QueryAsync<Match>("SELECT * FROM Match WHERE team_id_fk = ?", _teamId);
        }

        /// <summary>
        ///     Gets all the identifiers for each team in the localdb
        /// </summary>
        public async Task<string[]> GetAllTeamsIdAndAliasConcatenatedAsync()
        {            
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Team>();
            // Getting the instance without await, therefore we store it wrapped in the Task
            Task<Team[]> teams = cn.Table<Team>().ToArrayAsync();
            // Getting there result and returning it as an string array
            return teams.Result.Select(team => $"Id: {team.TeamId} || Alias: {team.TeamAlias}").ToArray();
        }

        /// <summary>
        ///     Gets the home team instance synchronously.
        /// </summary>
        public Team GetHomeTeam()
        {
            using (SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath))
            {
                cn.CreateTable<Team>();
                return cn.Query<Team>("SELECT * FROM Team WHERE is_home_team = ?", true).FirstOrDefault();
            }                          
        }

        /// <summary>
        ///     Gets the instance of the home team if it found one.
        /// </summary>
        public async Task<Team> GetHomeTeamAsync()
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Team>();
            return cn.QueryAsync<Team>("SELECT team_id FROM Team WHERE is_home_team = ?", true).Result.FirstOrDefault();
        }

        /// <summary>
        ///     Returns an instance of the team it found, if it found one.       
        /// </summary>
        public async Task<Team> GetTeamAsync(string _teamId)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Team>();            
            return cn.QueryAsync<Team>("SELECT * FROM Team WHERE team_id = ?", _teamId).Result.FirstOrDefault();
        }

        public async Task<Robot> GetRobotAsync(string _robotId)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Robot>();
            return cn.QueryAsync<Robot>("SELECT * FROM Robot WHERE robot_id = ?", _robotId).Result.FirstOrDefault();
        }

        /// <summary>
        ///     Saves the past instance of a Match and its various properties to the local database.
        /// </summary>
        public async Task SaveMatchToLocalDBAsync(Match _match)
        {

            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);

            // Creating the tables if they do not exist.
            await cn.CreateTablesAsync<Match, Point>();

            // Inserting our performance and gettings it identifier
            await cn.InsertAsync(_match);
            var test = _match.MatchId;

            // In the following loops we attach the matchId

            // Autonomous Points
            foreach (var point in _match.AutonomousPortPoints) { point.MatchId = _match.MatchId; }
            await cn.InsertAllAsync(_match.AutonomousPortPoints);

            // Normal Stage Points
            foreach (var point in _match.StageOnePortPoints) { point.MatchId = _match.MatchId; }
            await cn.InsertAllAsync(_match.StageOnePortPoints);
            foreach (var point in _match.StageTwoPortPoints) { point.MatchId = _match.MatchId; }
            await cn.InsertAllAsync(_match.StageTwoPortPoints);
            foreach (var point in _match.StageThreePortPoints) { point.MatchId = _match.MatchId; }
            await cn.InsertAllAsync(_match.StageThreePortPoints);                       
        }

        /// <summary>
        ///     Inserts a new team into the local database.
        ///     <br/>If choosen to insert as home team, the current home team be unassigned as the home team.
        ///     <br/>If choosen not to insert as the home team.
        /// </summary>
        public async Task SaveTeamToLocalDBAsync(Team _newTeam, bool _setAsHomeTeam = false)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);            
            await cn.CreateTableAsync<Team>();  
            
            // Set the new team as the home team
            if (_setAsHomeTeam)
            {
                await cn.QueryAsync<Team>("UPDATE Team SET is_home_team = false WHERE is_home_team = true");
                await cn.InsertAsync(_newTeam);
            }
            // Do not set the new team as the home team
            else
            {
                await cn.InsertAsync(_newTeam);
            }
        }

        /// <summary>
        ///     Unsets a team as the home team for this app.
        /// </summary>
        public async Task RemoveHomeStatusFromTeamAsync(string _teamId)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Team>();
            await cn.QueryAsync<Team>("UPDATE Team SET is_home_team = false WHERE team_id = ?", _teamId);
        }

        /// <summary>
        ///     Gets all the points for all the matches provided.
        /// </summary>
        public async Task<List<Point>> GetPointsFromMatchesAsync(List<Match> _matches)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Point>();

            List<Point> tempList = new List<Point>();

            foreach (var match in _matches)
            {
                tempList.AddRange(await cn.QueryAsync<Point>("SELECT * FROM Point WHERE match_id = ?", match.MatchId));
            }

            return tempList;       
        }

        /// <summary>
        ///     Returns a boolean indicating whether the match alreayd exist or not
        ///     <br/>true == does exist
        ///     <br/>false == doesn't exist
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DoesMatchExistAsync(string _matchId)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Match>();
            var match = cn.QueryAsync<Match>("SELECT * FROM Match WHERE match_id = ?", _matchId).Result.FirstOrDefault();
            if (match != null)
                return true;
            else
                return false;
        }

        /// <summary>
        ///     Returns a boolean indicating if the team exist or not.
        ///     <br/>true == does exist
        ///     <br/>false == doesn't exist
        /// </summary>
        public async Task<bool> DoesTeamExistAsync(string _teamId)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Team>();
            var team = cn.QueryAsync<Team>("SELECT * FROM Team WHERE team_id = ?", _teamId).Result.FirstOrDefault();
            if (team != null)
                return true;
            else
                return false;
        }

        /// <summary>
        ///     Gets all the robot ids and alias and joins them together into one string each
        /// </summary>
        public async Task<string[]> GetAllRobotIdAsync()
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Robot>();
            // Getting all the robot instances
            Task<Robot[]> teams = cn.Table<Robot>().ToArrayAsync();
            // Getting there result and returning it as an string array
            return teams.Result.Select(robot => $"Id: {robot.RobotId}").ToArray();
        }

        /// <summary>
        ///     Returns a tuple with a bool indicating wether any of the robots already exist.
        ///     If one does exist it will return a string of the Id.
        ///     <br/>true == exist
        ///     <br/>false == doesn't exist
        /// </summary>
        public async Task<Tuple<bool, string>> DoesRobotExistAsync(params string[] _robotId)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Robot>();

            foreach (var id in _robotId)
            {
                var robots = await cn.QueryAsync<Robot>("SELECT * FROM Robot WHERE robot_id = ?", id);
                // Checking each robot id for a matching robot id
                // If we find one we return and tell the user
                if (robots.Count != 0)
                {
                    return Tuple.Create(true, robots.FirstOrDefault().RobotId);
                }
            }
            // I get this, need to specify since null doesn't define a type.. all references can be null (or value types marked nullable)
            return Tuple.Create<bool, string>(false, null);
        }

        /// <summary>
        ///     Sets the team's is_home column to true for the given team id
        /// </summary>
        public async Task<Team> SetHomeStatusForTeamAsync(string _newHomeTeamId)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Team>();
            // First we unassign the last home team.
            await cn.QueryAsync<Team>("UPDATE Team SET is_home_team = false WHERE is_home_team = true");
            // Then we assign the new team as the home team.
            await cn.QueryAsync<Team>("UPDATE Team SET is_home_team = true WHERE team_id = ? ", _newHomeTeamId);

            return cn.QueryAsync<Team>("SELECT * FROM Team WHERE is_home_team = ?", true).Result.FirstOrDefault();
        }

        public async Task RemoveTeamFromLocalDBAsync(string _teamId)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTablesAsync<Team, Match, Point>();

            // Getting all the matchs
            List<Match> matches = await cn.QueryAsync<Match>("SELECT * FROM Match WHERE team_id_fk = ?", _teamId);

            // deleteing all the points to each match
            foreach (var match in matches)
            {
                await cn.QueryAsync<Point>("DELETE FROM Point WHERE match_id", match.MatchId);
            }
            // deleting all the matchs
            await cn.QueryAsync<Match>("DELETE FROM Match WHERE team_id_fk = ?", _teamId);
            // deleting all the teams
            await cn.QueryAsync<Team>("DELETE FROM Team WHERE team_id = ?", _teamId);
        }

        public async Task RemoveRobotFromLocalDBAsync(string _robotId)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Robot>();

            await cn.QueryAsync<Robot>("DELETE FROM Robot WHERE robot_id = ?", _robotId);
        }

        /// <summary>
        ///     Overwrites a team but keeps all its attached. Meaning that all matches associated with the old team will now be 
        ///         associated with the new team. This is done by changing the primary key in all associated matches with the old 
        ///         primary key. They will be assigned to the new foreign key for the team and then them old team record's id is updated
        ///         to the new team's id.
        ///         <br/>
        ///         <br/>
        ///     If the set as home team is marked true; the new team will be also be mark as the home team. Along with this, the old home team
        ///         is no longer marked as the home team.
        /// </summary>
        public async Task OverwriteTeamDataWithNewTeamAsync(Team _toBeOverwrittenTeam, Team _newTeam, bool _setAsHomeTeam = false)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTablesAsync<Match, Team>();
            // Updating all matches
            await cn.QueryAsync<Match>("UPDATE Match SET team_id_fk = ? WHERE team_id_fk = ?", _newTeam.TeamId, _toBeOverwrittenTeam.TeamId);
            // Updating values of table
            await cn.QueryAsync<Team>("UPDATE Team SET team_id = ? WHERE team_id = ?", _newTeam.TeamId, _toBeOverwrittenTeam.TeamId);

            // If the teams alias are different and the new team alias isnt null then replace the alias with the new one
            if (_toBeOverwrittenTeam.TeamAlias != _newTeam.TeamAlias && !string.IsNullOrEmpty(_newTeam.TeamAlias))
            {
                // Updating the alias based off the new set id
                await cn.QueryAsync<Team>("UPDATE Team SET team_alias = ? WHERE team_id = ?", _newTeam.TeamAlias, _newTeam.TeamId);
            }

            // If the team is not set to be the home team
            if (_setAsHomeTeam)
            {
                // First set any team that is already the home team to not be anymore.
                await cn.QueryAsync<Team>("UPDATE Team SET is_home_team = false WHERE is_home_team = true");
                // Secondly now assign the new passed team as the home team
                await cn.QueryAsync<Team>("UPDATE Team SET is_home_team = true WHERE team_id = ?", _newTeam.TeamId);
            }           
        }

        public async Task OverwriteRobotDataWithNewRobotAsync(Robot _toBeOverwrittenRobot, Robot _newRobot)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Robot>();
            await cn.QueryAsync<Team>("UPDATE Robot SET robot_id = ?, robot_info = ?, image_path = ? WHERE robot_id = ?", _newRobot.RobotId, _newRobot.RobotInfo, _newRobot.ImagePath, _toBeOverwrittenRobot.RobotId);
        }

        /// <summary>
        ///     Queries the database for 
        /// </summary>
        public async Task<List<Team>> GetSearchResultsForTeamAliasAsync(string _query)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Team>();
            return await cn.QueryAsync<Team>("SELECT * FROM Team WHERE team_alias Like ?", "%" + _query + "%");
        }

        /// <summary>
        ///     Gets all the teams from the database.
        /// </summary>
        public async Task<List<Team>> GetAllTeamsAsync()
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Team>();
            return await cn.Table<Team>().ToListAsync();
        }

        /// <summary>
        ///     Gets the number of matches a team has.
        /// </summary>
        public int GetTeamMatchCount(string _teamId)
        {
            SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath);
            cn.CreateTable<Match>();
            return cn.Query<int>("SELECT COUNT(*) FROM Match").FirstOrDefault();
        }

        public List<Match> GetAllMatches()
        {
            SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath);
            cn.CreateTable<Match>();
            return cn.Table<Match>().ToList();
        }

        public List<Point> GetPointsFromMatch(string _matchId)
        {
            SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath);
            cn.CreateTable<Point>();
            return cn.Query<Point>("SELECT * FROM Point WHERE match_id = ?", _matchId);
        }

        public async Task<List<Robot>> GetSearchResultsForRobotIdAsync(string _query)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Robot>();
            return await cn.QueryAsync<Robot>("SELECT * FROM Robot WHERE robot_id Like ?", "%" + _query + "%");
        }

        public async Task<List<Robot>> GetAllRobotsAsync()
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Robot>();
            return await cn.Table<Robot>().ToListAsync();
        }

        public async Task SaveRobotToLocalDbASync(Robot _robot)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Robot>();
            await cn.InsertAsync(_robot);
        }

        /// <summary>
        ///     Removes a specified team from the database.
        /// </summary>
        //public async Task RemoveTeamFromLocalDBAsync(string _teamId)
        //{
        //    SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
        //    await cn.CreateTableAsync<Team>();
        //    await cn.QueryAsync<Team>("DELETE FROM Team WHERE team_id = ?", _teamId);
        //}



    }

    //public async Task<List<Team>> GetAllTeamsAsync()
    //{
    //    // Creating a connection to the db
    //    SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
    //    // Creates a table if it doesn't already exist
    //    await cn.CreateTableAsync<Team>();
    //    // Returns all the Teams from the db
    //    return await cn.Table<Team>().ToListAsync();
    //}

    /// <summary>
    ///     Queries the <see cref="Team"/> with the <paramref name="_query"/> parameter provided. 
    ///     Uses the '%' wildcard character in the query to increase the flexability of the query.
    /// </summary>
    //public async Task<List<Team>> QueryTeamsByName(string _query)
    //{
    //    SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);

    //    await cn.CreateTableAsync<Team>();
    //    // Using the '%' wildcard character to signify that we don't care about what is infront or behind depending on where we place it.
    //    // Just makes our query more flexable
    //    return await cn.QueryAsync<Team>("SELECT * FROM Team WHERE name LIKE ?", '%' + _query + '%');
    //}

    /// <summary>
    ///     Gets all the performances a team has and returns them in a list.
    /// </summary>
    //public async Task<List<Performance>> GetPerformancesForTeam(Team _team)
    //{
    //    SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
    //    await cn.CreateTableAsync<Performance>();
    //    return await cn.QueryAsync<Performance>("SELECT * FROM Performance WHERE team_id_fk = ?", _team.Name);
    //}
}