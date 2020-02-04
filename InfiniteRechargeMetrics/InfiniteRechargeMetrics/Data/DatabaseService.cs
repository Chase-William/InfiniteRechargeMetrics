using SQLite;

namespace InfiniteRechargeMetrics.Data
{
    public static class DataService
    {
        public static void WriteToDb()
        {
            // TODO ------------------------------------------------------------- Make path change based on operating system
            using (SQLiteConnection connection = new SQLiteConnection(""))
            {
                // Creates a table based off our entity
                connection.CreateTable<Type>();

                // Inserts an instance into the newly created table
                connection.Insert(instance);

                connection.Close();
            }
        }
    }
}