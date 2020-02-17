using SQLite;

namespace InfiniteRechargeMetrics.Models
{
    /// <summary>
    ///     A Match is a (class, table) that represents an instance of a game where two opposing teams
    ///         battled it out.
    /// </summary>
    [Table("Match")]
    public class Match
    {
        /// <summary>
        ///     The primary key for the team (class, table).
        /// </summary>
        [Column("id"), PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        /// <summary>
        ///     The name or title of match.
        /// </summary>
        [Column("title"), NotNull]
        public string Title { get; set; }
        /// <summary>
        ///     The identifer of the first team that took part in this match
        /// </summary>
        [Column("team_one_performance_fk")]
        public int TeamOnePerformance_FK { get; set; }
        /// <summary>
        ///     The identifer of the second team that took part in this match
        /// </summary>
        [Column("team_two_performance_fk")]
        public int TeamTwoPerformance_FK { get; set; }
    }
}
