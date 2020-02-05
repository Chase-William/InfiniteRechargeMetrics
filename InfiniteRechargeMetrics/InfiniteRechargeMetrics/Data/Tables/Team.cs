using SQLite;

namespace InfiniteRechargeMetrics.Data
{
    [Table("Team")]
    public class Team
    {
        /// <summary>
        ///     The primary key for the team (class, table).
        /// </summary>
        //[Column("id"), PrimaryKey, AutoIncrement, Unique]
        //public int Id { get; set; }        

        /// <summary>
        ///     The name or title of the team.
        /// </summary>
        [Column("name"), PrimaryKey, Unique]
        public string Name { get; set; }

        // 0ad7sad9876fasd908f -- using Name prop as primary key and getting everything to load into the sqlite db3 properly... change DataServer.add method to be smarter
    }
}
