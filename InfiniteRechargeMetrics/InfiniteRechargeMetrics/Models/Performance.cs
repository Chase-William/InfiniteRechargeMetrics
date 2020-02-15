using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        #region Stage One
        // --- Automonous ---

        /// <summary>
        ///     A lower port score in autonomous mode
        /// </summary>
        [Column("auto_low_port_points")]
        public ObservableCollection<Point> AutoLowPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     A upper port score in autonomous mode
        /// </summary>
        [Column("auto_upper_port_points")]
        public ObservableCollection<Point> AutoUpperPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     A small port score in autonomous mode
        /// </summary>
        [Column("auto_small_port_points")]
        public ObservableCollection<Point> AutoSmallPortPoints { get; set; } = new ObservableCollection<Point>();

        // --- Stage 1 Manual ---

        /// <summary>
        ///     A lower port point in stage one manual mode
        /// </summary>
        [Column("stage_one_low_port_points")]
        public ObservableCollection<Point> StageOneLowPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     A upper port point in stage one manual mode
        /// </summary>
        [Column("stage_one_upper_port_points")]
        public ObservableCollection<Point> StageOneUpperPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     A small port point in stage one manual mode
        /// </summary>
        [Column("stage_one_small_port_points")]
        public ObservableCollection<Point> StageOneSmallPortPoints { get; set; } = new ObservableCollection<Point>();

        [Column("moved_off_start_line_points")]
        public int MovedOffStartLinePoints { get; set; }

        // Control Panel
        [Column("control_panel")]
        public bool IsControlPanelFinished { get; set; }
        /// <summary>
        ///     Records the miliseconds from the round start at which the control panel was finished
        /// </summary>
        [Column("control_panel_time")]
        public bool TimeControlPanelFinished { get; set; }

        #endregion Stage One End

        #region Stage Two

        /// <summary>
        ///     A lower port point in stage two 
        /// </summary>
        [Column("stage_two_low_port_points")]
        public int StageTwoLowPortPoints { get; set; }
        /// <summary>
        ///     A upper port point in stage two 
        /// </summary>
        [Column("stage_two_upper_port_points")]
        public int StageTwoUpperPortPoints { get; set; }
        /// <summary>
        ///     A small port point in stage two 
        /// </summary>
        [Column("stage_two_small_port_points")]
        public int StageTwoSmallPortPoints { get; set; }

        #endregion Stage Two End

        #region Stage Three

        /// <summary>
        ///     A lower port point in stage three 
        /// </summary>
        [Column("stage_three_low_port_points")]
        public int StageThreeLowPortPoints { get; set; }
        /// <summary>
        ///     A upper port point in stage three 
        /// </summary>
        [Column("stage_three_upper_port_points")]
        public int StageThreeUpperPortPoints { get; set; }
        /// <summary>
        ///     A small port point in stage three 
        /// </summary>
        [Column("stage_three_small_port_points")]
        public int StageThreeSmallPortPoints { get; set; }
        #endregion Stage Three End
    }
}
