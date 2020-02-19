using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InfiniteRechargeMetrics.Models
{
    [Table("Performance")]
    public class Performance
    {
        /// <summary>
        ///     The identifier for this class / table
        /// </summary>
        [Column("Id"), PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        /// <summary>
        ///     The name of the peformance, does not need to be unique.
        /// </summary>
        [Column("performance_name")]
        public string PerformanceName { get; set; }
        /// <summary>
        ///     The foreign key of which a teams Performance belongs to
        /// </summary>
        [Column("team_id_fk")]
        public string TeamId_FK { get; set; }

        #region Stage One
        // --- Automonous ---

        /// <summary>
        ///     A lower port score in autonomous mode
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> AutoLowPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     A upper port score in autonomous mode
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> AutoUpperPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     A small port score in autonomous mode
        /// </summary
        [Ignore]
        public ObservableCollection<Point> AutoSmallPortPoints { get; set; } = new ObservableCollection<Point>();

        // --- Stage 1 Manual ---

        /// <summary>
        ///     A lower port point in stage one manual mode
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> StageOneLowPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     A upper port point in stage one manual mode
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> StageOneUpperPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     A small port point in stage one manual mode
        /// </summary
        [Ignore]
        public ObservableCollection<Point> StageOneSmallPortPoints { get; set; } = new ObservableCollection<Point>();

        /// <summary>
        ///     Holds the point values of the robots that moved off their spawn location.
        ///     Each robot that moves off spawn earns 5 points.
        /// </summary>
        [Column("robots_moved_from_spawn_points")]
        public int RobotsMovedFromSpawnPoints { get; set; }
        /// <summary>
        ///     Records whether the stage one control panel was finished or not        
        /// </summary>
        [Column("stage_one_control_panel")]
        public bool IsStageOneControlPanelFinished { get; set; }
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
        [Ignore]
        public ObservableCollection<Point> StageTwoLowPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     A upper port point in stage two 
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> StageTwoUpperPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     A small port point in stage two 
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> StageTwoSmallPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     Record whether the stage two control panel was finished or not
        /// </summary>
        [Column("stage_two_control_panel")]
        public bool IsStageTwoControlPanelFinished { get; set; }
        #endregion Stage Two End

        #region Stage Three

        /// <summary>
        ///     A lower port point in stage three 
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> StageThreeLowPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     A upper port point in stage three 
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> StageThreeUpperPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     A small port point in stage three 
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> StageThreeSmallPortPoints { get; set; } = new ObservableCollection<Point>();

        [Column("droid_one_randevu")]
        public bool DroidOneRandevu { get; set; }
        [Column("droid_two_randevu")]
        public bool DroidTwoRandevu { get; set; }
        [Column("droid_three_randevu")]
        public bool DroidThreeRandevu { get; set; }
        [Column("is_droid_randevu_level")]
        public bool IsRandevuLevel { get; set; }
        #endregion Stage Three End
    }
}
