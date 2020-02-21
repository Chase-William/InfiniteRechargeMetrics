using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InfiniteRechargeMetrics.Models
{
    [Table("Match")]
    public class Match
    {
        public const byte MAX_ROBOTS_IN_MATCH = 6;

        #region Match Data
        /// <summary>
        ///     The identifier for this class / table
        /// </summary>
        [Column("match_id"), PrimaryKey, Unique]
        public string MatchId { get; set; }
        /// <summary>
        ///     The name of the peformance, does not need to be unique.
        /// </summary>
        [Column("match_name")]
        public string MatchName { get; set; }
        /// <summary>
        ///     The foreign key of which a teams Performance belongs to
        /// </summary>
        [Column("team_id_fk")]
        public string TeamId_FK { get; set; }
        [Column("comments")]
        public string Comments { get; set; }
        #endregion

        #region Stage One
        // --- Automonous ---

        /// <summary>
        ///     Stores the points relate to stage one autonomous.
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> AutonomousPortPoints { get; set; } = new ObservableCollection<Point>();

        // --- Stage 1 Manual ---

        /// <summary>
        ///     Stores the points relate to stage one manual.
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> StageOnePortPoints { get; set; } = new ObservableCollection<Point>();

        /// <summary>
        ///     Holds the point values of the robots that moved off their spawn location.
        ///     Each robot that moves off spawn earns 5 points.
        /// </summary>
        [Column("robots_moved_from_spawn_points")]
        public int RobotsMovedFromSpawnPoints { get; set; }        
        #endregion

        #region Stage Two
        /// <summary>
        ///     Stores the points relate to stage tw0.
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> StageTwoPortPoints { get; set; } = new ObservableCollection<Point>();
        /// <summary>
        ///     Record whether the stage two control panel was finished or not
        /// </summary>
        [Column("stage_two_control_panel")]
        public bool IsStageTwoControlPanelFinished { get; set; }
        #endregion 

        #region Stage Three
        /// <summary>
        ///     Stores the points relate to stage three.
        /// </summary>
        [Ignore]
        public ObservableCollection<Point> StageThreePortPoints { get; set; } = new ObservableCollection<Point>();

        /// <summary>
        ///     Records whether the stage one control panel was finished or not        
        /// </summary>
        [Column("stage_three_control_panel")]
        public bool IsStageThreeControlPanelFinished { get; set; }
        /// <summary>
        ///     Records the miliseconds from the round start at which the control panel was finished
        /// </summary>

        [Column("droid_one_randevu")]
        public bool DroidOneRandevu { get; set; }
        [Column("droid_two_randevu")]
        public bool DroidTwoRandevu { get; set; }
        [Column("droid_three_randevu")]
        public bool DroidThreeRandevu { get; set; }
        [Column("is_droid_randevu_level")]
        public bool IsRandevuLevel { get; set; }
        #endregion Stage Three End

        #region Robot Info
        /// <summary>
        ///     Information about the robots used during this performance.
        ///     
        ///     - Idealy in the future robot should be its own class and table inside the database.
        ///         For now it will simply remain as part of the Match directly.
        /// 
        /// </summary>
        [Ignore]
        public Robot[] Robots { get; set; } = new Robot[MAX_ROBOTS_IN_MATCH];

        //[Column("robot_one_id")]
        //public string RobotOneId { get; set; }
        //[Column("robot_one_info")]
        //public string RobotOneInfo { get; set; }

        //[Column("robot_two_id")]
        //public string RobotTwoId { get; set; }
        //[Column("robot_two_info")]
        //public string RobotTwoInfo { get; set; }

        //[Column("robot_three_id")]
        //public string RobotThreeId { get; set; }
        //[Column("robot_three_info")]
        //public string RobotThreeInfo { get; set; }

        //[Column("robot_four_id")]
        //public string RobotFourId { get; set; }
        //[Column("robot_four_info")]
        //public string RobotFourInfo { get; set; }

        //[Column("robot_five_id")]
        //public string RobotFiveId { get; set; }
        //[Column("robot_five_info")]
        //public string RobotFiveInfo { get; set; }

        //[Column("robot_six_id")]
        //public string RobotSixId { get; set; }
        //[Column("robot_three_info")]
        //public string RobotSixInfo { get; set; }
        #endregion
    }
}
