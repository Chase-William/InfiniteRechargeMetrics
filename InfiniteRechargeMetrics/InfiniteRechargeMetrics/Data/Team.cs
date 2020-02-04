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
        public int Name { get; set; }
        /// <summary>
        ///     Points  -------------------------------------- Shouldn't these be in like match table.. the points belong to a match not the team itself... they got the points during a match against another team
        /// </summary>
        [Column("auto_lower_point")]
        public int AutoLowPoint { get; set; }
        [Column("auto_med_point")]
        public int AutoMedPoint { get; set; }
        [Column("auto_high_point")]
        public int AutoHighPoint { get; set; }
        [Column("auto_low_point")]
        public int ManualLowPoint { get; set; }
        [Column("auto_med_point")]
        public int ManualMedPoint { get; set; }
        [Column("auto_high_point")]
        public int ManualHighPoint { get; set; }
    }
}
