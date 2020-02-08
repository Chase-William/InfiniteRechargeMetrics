using SQLite;

namespace InfiniteRechargeMetrics.Data
{
    [Table("Team")]
    public class Team
    {     
        /// <summary>
        ///     The name or title of the team.
        /// </summary>
        [Column("name"), PrimaryKey, Unique]
        public string Name { get; set; }
    }
}
