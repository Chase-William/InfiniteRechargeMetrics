using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteRechargeMetrics.Models;

namespace InfiniteRechargeMetrics.Data
{
    public static class DatabaseService
    {
        public async static void SaveToDatabase(object _record)
        {
            await Task.Run(() =>
            {
                SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
                if (typeof(Team) == _record.GetType())
                {
                    //Creates a table based off our entity
                    cn.CreateTableAsync<Team>();
                    //Inserts an instance into the newly created table
                    cn.InsertAsync(_record);
                }
                else if (typeof(Performance) == _record.GetType())
                {
                    cn.CreateTableAsync<Performance>();
                    cn.InsertAsync(_record);
                }
                //else if (typeof(Match) == _record.GetType())
                //{
                //    cn.CreateTableAsync<Match>();
                //    cn.InsertAsync(_record);
                //}
                cn.CloseAsync();                
            });            
        }

        /// <summary>
        ///     Gets all the performances a team has and returns them in a list.
        /// </summary>
        public async static Task<List<Performance>> GetPerformancesForTeam(Team _team)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            await cn.CreateTableAsync<Performance>();
            return await cn.QueryAsync<Performance>("SELECT * FROM Performance WHERE team_id_fk = ?", _team.Name);            
        }

        /// <summary>
        /// 
        /// </summary>
        //public static List<Match> GetMatchesForTeam(int[] _performances)
        //{
        //    SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
        //    await cn.CreateTableAsync<Match>();
        //    return await cn.QueryAsync<Match>("SELECT * FROM Match WHERE team_one_performance_fk OR team_two_performance_fk IN (?)", _performances[0], _performances[1]);
            
        //}

        public async static Task<List<Team>> GetAllTeamsAsync()
        {
            // Creating a connection to the db
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);            
            // Creates a table if it doesn't already exist
            await cn.CreateTableAsync<Team>();
            // Returns all the Teams from the db
            return await cn.Table<Team>().ToListAsync();            
        }

        /// <summary>
        ///     Queries the <see cref="Team"/> with the <paramref name="_query"/> parameter provided. 
        ///     Uses the '%' wildcard character in the query to increase the flexability of the query.
        /// </summary>
        public async static Task<List<Team>> QueryTeamsByName(string _query)
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);

            await cn.CreateTableAsync<Team>();
            // Using the '%' wildcard character to signify that we don't care about what is infront or behind depending on where we place it.
            // Just makes our query more flexable
            return await cn.QueryAsync<Team>("SELECT * FROM Team WHERE name LIKE ?", '%' + _query + '%');
        }

        public async static Task<List<int>> GetNextId()
        {
            SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);
            return await cn.QueryAsync<int>("SELECT * FROM SQLITE_SEQUENCE WHERE name='TABLE'");
        }

        /// <summary>
        ///     Saves the past instance of a Performance and its various properties to the local database.
        /// </summary>
        public async static void SavePerformanceToDB(Performance _performance)
        {
            await Task.Run(() =>
            {
                SQLiteAsyncConnection cn = new SQLiteAsyncConnection(App.DatabaseFilePath);

                cn.InsertAsync(_performance);

                cn.CloseAsync();
            });
        }
    }
}