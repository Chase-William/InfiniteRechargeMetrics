using SQLite;

namespace InfiniteRechargeMetrics.Data
{
    [Table("Match")]
    public class Match
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

        [Column("team_one_id")]
        public int TeamOneId { get; set; }
        [Column("team_two_id")]
        public int TeamTwoId { get; set; }
    }
}
