using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteRechargeMetrics.Models;
using System.Linq;
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
            await Task.Run(() =>
            {
                SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
                cn.CreateTableAsync<Match>();
                return cn.QueryAsync<Match>("SELECT * FROM Team WHERE team_id = ?", _teamId);
            });           
            return null;
        }

        /// <summary>
        ///     Gets all the identifiers for each team in the localdb
        /// </summary>
        public async Task<string[]> GetAllTeamsIdPlusName()
        {            
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Team>();
            // Getting the instance without await, therefore we store it wrapped in the Task
            Task<List<Team>> team = cn.Table<Team>().ToListAsync();
            // Getting there result and returning it as an string array
            return team.Result.Select(x => $"Id: {x.TeamId} || Alias: {x.Alias}").ToArray();
        }

        /// <summary>
        ///     Queries the local database for the team which is set as the home team.
        /// </summary>
        public Team GetHomeTeam()
        {
            SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath);
            cn.CreateTable<Team>();            
            return cn.Query<Team>("SELECT team_id FROM Team WHERE is_home_team = ?", true).FirstOrDefault();
        }

        /// <summary>
        ///     Saves the past instance of a Performance and its various properties to the local database.
        /// </summary>
        public async Task SaveMatchToLocalDBAsync(Match _performance)
        {

            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);

            // Creating the tables if they do not exist.
            await cn.CreateTablesAsync<Match, Point>();

            // Inserting our performance and gettings it identifier
            int Id = await cn.InsertAsync(_performance);

            // In the following loops we attach the performanceId

            // Autonomous Points
            foreach (var point in _performance.AutonomousPortPoints) { point.PerformanceId = Id; }
            await cn.InsertAllAsync(_performance.AutonomousPortPoints);

            // Normal Stage Points
            foreach (var point in _performance.StageOnePortPoints) { point.PerformanceId = Id; }
            await cn.InsertAllAsync(_performance.StageOnePortPoints);
            foreach (var point in _performance.StageTwoPortPoints) { point.PerformanceId = Id; }
            await cn.InsertAllAsync(_performance.StageTwoPortPoints);
            foreach (var point in _performance.StageThreePortPoints) { point.PerformanceId = Id; }
            await cn.InsertAllAsync(_performance.StageThreePortPoints);                       
        }

        /// <summary>
        ///     Saves a team instance to the local sqlite database.
        /// </summary>
        public async Task SaveTeamToLocalDBAsync(Team _team)
        {
            await Task.Run(() =>
            {
                using (SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath))
                {
                    cn.CreateTable<Team>();
                    cn.Insert(_team);
                    cn.Close();
                }
            });
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