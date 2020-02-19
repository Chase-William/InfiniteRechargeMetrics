using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteRechargeMetrics.Models;
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
        public async Task<List<Performance>> GetAllPerformancesFromTeam(string _teamName)
        {
            List<Performance> collection = new List<Performance>();
            await Task.Run(() =>
            {
                using (SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath))
                {
                    cn.CreateTable<Performance>();
                    collection = cn.Query<Performance>("SELECT * FROM Team WHERE Name = ?", _teamName);
                    cn.Close();                    
                }
                return collection;
            });
            return collection;
        }

        /// <summary>
        ///     Saves the past instance of a Performance and its various properties to the local database.
        /// </summary>
        public async Task SavePerformanceToLocalDB(Performance _performance)
        {
            await Task.Run(() =>
            {
                using (SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath))
                {
                    // Creating the tables if they do not exist.
                    cn.CreateTables<Performance, Point>();

                    // Inserting our performance and gettings it identifier
                    int Id = cn.Insert(_performance);

                    // In the following loops we attach the performanceId

                    // Autonomous Points
                    foreach (var point in _performance.AutonomousPortPoints) { point.PerformanceId = Id; }
                    cn.InsertAll(_performance.AutonomousPortPoints);

                    // Normal Stage Points
                    foreach (var point in _performance.StageOnePortPoints) { point.PerformanceId = Id; }
                    cn.InsertAll(_performance.StageOnePortPoints);
                    foreach (var point in _performance.StageTwoPortPoints) { point.PerformanceId = Id; }
                    cn.InsertAll(_performance.StageTwoPortPoints);
                    foreach (var point in _performance.StageThreePortPoints) { point.PerformanceId = Id; }
                    cn.InsertAll(_performance.StageThreePortPoints);

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