using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteRechargeMetrics.Models;

namespace InfiniteRechargeMetrics.Data
{
    public static class DatabaseService
    {
        public static void SaveToDatabase(object _record)
        {
            using (SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath))
            {
                if (typeof(Team) == _record.GetType())
                {
                    //Creates a table based off our entity
                    cn.CreateTable<Team>();
                    //Inserts an instance into the newly created table
                    cn.Insert(_record);
                }
                else if (typeof(Performance) == _record.GetType())
                {
                    cn.CreateTable<Performance>();
                    cn.Insert(_record);
                }
                else if (typeof(Match) == _record.GetType())
                {
                    cn.CreateTable<Match>();
                    cn.Insert(_record);
                }
                cn.Close();
            }
        }

        /// <summary>
        ///     Gets all the performances a team has and returns them in a list.
        /// </summary>
        public static List<Performance> GetPerformancesForTeam(Team _team)
        {
            using (SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath))
            {
                cn.CreateTable<Performance>();
                return cn.Query<Performance>("SELECT * FROM Performance WHERE team_id_fk = ?", _team.Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<Match> GetMatchesForTeam(int[] _performances)
        {
            using (SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath))
            {
                cn.CreateTable<Match>();
                return cn.Query<Match>("SELECT * FROM Match WHERE team_one_performance_fk OR team_two_performance_fk IN (?)", _performances[0], _performances[1]); // <----------------- NEEDS FIXING
            }
        }

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
    }
}