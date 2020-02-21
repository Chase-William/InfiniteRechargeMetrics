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
        public async Task<List<Match>> GetAllMatchesForTeamAsync(string _teamId)
        {                              
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Match>();            
            return await cn.QueryAsync<Match>("SELECT * FROM Team WHERE team_id = ?", _teamId);
        }

        /// <summary>
        ///     Gets all the identifiers for each team in the localdb
        /// </summary>
        public async Task<string[]> GetAllTeamsIdPlusNameAsync()
        {            
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Team>();
            // Getting the instance without await, therefore we store it wrapped in the Task
            Task<List<Team>> teams = cn.Table<Team>().ToListAsync();
            // Getting there result and returning it as an string array
            return teams.Result.Select(x => $"Id: {x.TeamId} || Alias: {x.Alias}").ToArray();
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

        /// <summary>
        ///     Saves the past instance of a Match and its various properties to the local database.
        /// </summary>
        public async Task SaveMatchToLocalDBAsync(Match _match)
        {

            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);

            // Creating the tables if they do not exist.
            await cn.CreateTablesAsync<Match, Point>();

            // Inserting our performance and gettings it identifier
            int Id = await cn.InsertAsync(_match);

            // In the following loops we attach the matchId

            // Autonomous Points
            foreach (var point in _match.AutonomousPortPoints) { point.MatchId = Id; }
            await cn.InsertAllAsync(_match.AutonomousPortPoints);

            // Normal Stage Points
            foreach (var point in _match.StageOnePortPoints) { point.MatchId = Id; }
            await cn.InsertAllAsync(_match.StageOnePortPoints);
            foreach (var point in _match.StageTwoPortPoints) { point.MatchId = Id; }
            await cn.InsertAllAsync(_match.StageTwoPortPoints);
            foreach (var point in _match.StageThreePortPoints) { point.MatchId = Id; }
            await cn.InsertAllAsync(_match.StageThreePortPoints);                       
        }

        /// <summary>
        ///     Saves a team instance to the local sqlite database.
        /// </summary>
        public async Task SaveTeamToLocalDBAsync(Team _team)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);            
            await cn.CreateTableAsync<Team>();
            await cn.InsertAsync(_team);                                       
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
        public async Task<List<Point>> GetPointsFromMatches(List<Match> _matches)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Point>();

            List<Point> points = new List<Point>();

            foreach (var match in _matches)
            {
                points.AddRange(await cn.QueryAsync<Point>("SELECT * FROM Point WHERE match_id = ?", match.MatchId));
            }

            return points;          
        }
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