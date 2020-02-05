using SQLite;

namespace InfiniteRechargeMetrics.Data
{
    public static class DataService
    {
        public static void SaveToDatabase(object _record, InfiniteRechargeType _type)
        {
            

            switch (_type)
            {
                case InfiniteRechargeType.Team:
                   using (SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath)) {
                        //Creates a table based off our entity
                        cn.CreateTable<Team>();
                        //Inserts an instance into the newly created table
                        cn.Insert(_record);
                        cn.Close();
                   }                        
                    break;
                case InfiniteRechargeType.Match:
                    using (SQLiteConnection cn = new SQLiteConnection(App.DatabaseFilePath))
                    {
                        //Creates a table based off our entity
                        cn.CreateTable<Match>();
                        //Inserts an instance into the newly created table
                        cn.Insert(_record);
                        cn.Close();
                    }
                    break;
                default:
                    break;
            }            
        }

        static SQLiteConnection ConnectLocalDatabase()
        {
            return new SQLiteConnection(App.DatabaseFilePath);
        }

       // Creates a table based off our entity
       //connection.CreateTable<Team>();

       // Inserts an instance into the newly created table
       //         connection.Insert(_team);

       //         connection.Close();
    }

    public enum InfiniteRechargeType { Team, Match }
}