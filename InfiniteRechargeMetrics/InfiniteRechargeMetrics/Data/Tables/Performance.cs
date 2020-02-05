using SQLite;

namespace InfiniteRechargeMetrics.Data
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
        [Column("auto_low_point")]
        public int AutoLowPoint { get; set; }
        /// <summary>
        ///     A upper port score in autonomous mode
        /// </summary>
        [Column("auto_med_point")]
        public int AutoMedPoint { get; set; }
        /// <summary>
        ///     A small port score in autonomous mode
        /// </summary>
        [Column("auto_high_point")]
        public int AutoHighPoint { get; set; }


        // --- Manual ---

        /// <summary>
        ///     A lower port point in manual mode
        /// </summary>
        [Column("manual_low_point")]
        public int ManualLowPoint { get; set; }
        /// <summary>
        ///     A upper port point in manual mode
        /// </summary>
        [Column("manual_med_point")]
        public int ManualMedPoint { get; set; }
        /// <summary>
        ///     A small port point in manual mode
        /// </summary>
        [Column("manual_high_point")]
        public int ManualHighPoint { get; set; }
    }
}
