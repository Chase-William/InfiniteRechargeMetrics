using SQLite;
using System.Collections.Generic;

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

        public static List<Match> GetMatchesForTeam(int[] _performances)
        {
            using (SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath))
            {
                cn.CreateTable<Match>();             
                return cn.Query<Match>("SELECT * FROM Match WHERE team_one_performance_fk OR team_two_performance_fk IN (?)", _performances[0], _performances[1]);               
            }
        }
    }
}