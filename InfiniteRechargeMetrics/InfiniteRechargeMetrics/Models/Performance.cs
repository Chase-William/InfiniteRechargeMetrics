using SQLite;

namespace InfiniteRechargeMetrics.Models
{
    [Table("Performance")]
    public class Performance
    {
        /// <summary>
        ///     The identifier for this class or table
        /// </summary>
        [Column("id"), PrimaryKey, AutoIncrement, Unique]        
        public int Id { get; set; }
        /// <summary>
        ///     The foreign key of which a teams Performance belongs to
        /// </summary>
        [Column("team_id_fk"), NotNull]
        public string TeamId_FK { get; set; }
        

        // --- Automonous ---

        /// <summary>
        ///     A lower port score in autonomous mode
        /// </summary>
        [Column("stage_one_low_port_points")]
        public int AutoLowPortPoints { get; set; }
        /// <summary>
        ///     A upper port score in autonomous mode
        /// </summary>
        [Column("auto_upper_port_points")]
        public int AutoUpperPortPoints { get; set; }
        /// <summary>
        ///     A small port score in autonomous mode
        /// </summary>
        [Column("auto_small_port_points")]
        public int AutoSmallPortPoints { get; set; }


        // --- Manual ---

        /// <summary>
        ///     A lower port point in manual mode
        /// </summary>
        [Column("manual_low_port_points")]
        public int ManualLowPortPoints { get; set; }
        /// <summary>
        ///     A upper port point in manual mode
        /// </summary>
        [Column("manual_upper_port_points")]
        public int ManualUpperPortPoints { get; set; }
        /// <summary>
        ///     A small port point in manual mode
        /// </summary>
        [Column("manual_small_port_points")]
        public int ManualSmallPortPoints { get; set; }
    }
}
