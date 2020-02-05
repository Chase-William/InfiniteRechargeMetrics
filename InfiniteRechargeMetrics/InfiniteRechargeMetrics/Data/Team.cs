using SQLite;

namespace InfiniteRechargeMetrics.Data
{
    [Table("Team")]
    public class Team
    {
        /// <summary>
        ///     The primary key for the team (class, table).
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        ///     The name or title of the team.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
    }
}
