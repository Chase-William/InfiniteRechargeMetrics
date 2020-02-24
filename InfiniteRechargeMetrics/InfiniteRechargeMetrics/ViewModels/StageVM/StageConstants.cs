namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     Struct containing data related to the value of ports.
    /// </summary>
    public struct StageConstants
    {
        /// <summary>
        ///     The value of the Low Power Port during autononmous mode.
        /// </summary>
        public const int AUTO_LPP = 2;
        /// <summary>
        ///     The value of the Upper Power Port during autononmous mode.
        /// </summary>
        public const int AUTO_UPP = 4;
        /// <summary>
        ///     The value of the Small Power Port during autononmous mode.
        /// </summary>
        public const int AUTO_SPP = 6;
        /// <summary>
        ///     The value of the Low Power Port during manual mode.
        /// </summary>
        public const int MANUAL_LPP = 1;
        /// <summary>
        ///     The value of the Upper Power Port during manual mode.
        /// </summary>
        public const int MANUAL_UPP = 2;
        /// <summary>
        ///     The value of the Small Power Port during manual mode.
        /// </summary>
        public const int MANUAL_SPP = 3;
        /// <summary>
        ///     Text to be displayed when the stage is in a manual mode and can vary.
        /// </summary>
        public const string STAGE_STATE_MANUAL = "Manual Mode Active";
        /// <summary>
        ///     Text to be displated when the stage is in autonomous mode and can vary.
        /// </summary>
        public const string STAGE_STAGE_AUTONOMOUS = "Autononmous Mode Active";
        /// <summary>
        ///     Header of text to be dispalyed with the label for adjusting how many robots moved off their starting position. 
        ///     Default == 3
        /// </summary>
        public const string ROBOTS_MOVED_FROM_SPAWN_HEADER = "Robots Moved From Spawn:";
        /// <summary>
        ///     Is the multiplier used when determining how many points were earned from the amount of robots that successfully moved off their spawn.
        /// </summary>
        public const int ROBOTS_MOVED_FROM_SPAWN_MULTIPLIER = 5;
        /// <summary>
        ///     Is the default value assigned to the properties related to keeping track of the points earned from the robots moving off their spawn.
        /// </summary>
        public const int ROBOTS_MOVED_FROM_SPAWN_DEFAULT_VALUE = 15;
        /// <summary>
        ///     String used for displaying the state of the stage as complete;
        /// </summary>
        public const string STAGE_COMPLETE = "Complete";
        /// <summary>
        ///     String used for display the state of the stage as incomplete;
        /// </summary>
        public const string STAGE_INCOMPLETE = "Incomplete";
        /// <summary>
        ///     Amount of time in miliseconds the timer for autonomous mode should have for its interval property.
        /// </summary>
        public const int AUTONOMOUS_TIMER_INTERVAL = 50;
        /// <summary>
        ///     Is added to the autonomous progess bar every 50 miliseconds
        /// </summary>
        public const double AUTONOMOUS_PROGRESSBAR_UPDATE = 0.00333333333;
        /// <summary>
        ///     The amount of time in milliseconds a timer should run when in a relationship with autonomous mode
        /// </summary>
        public const int AUTONOMOUS_DURATION_IN_MILLISECONDS = 15000;
        /// <summary>
        ///     The amount of time in miliseconds for the manual part of the match or performance to be finished. (time limit)
        /// </summary>
        public const int MANUAL_MODE_MAX_TIME = 135000;
        /// <summary>
        ///     The min value of points required to complete stage one.
        /// </summary>
        public const int MIN_VALUE_FOR_COMPLETED_STAGE_ONE = 9;
        /// <summary>
        ///     The min value of points required to complete stage two.
        /// </summary>
        public const int MIN_VALUE_FOR_COMPLETED_STAGE_TWO = 20;
        /// <summary>
        ///     The mai value of points required to complete stage three.
        /// </summary>
        public const int MIN_VALUE_FOR_COMPLETED_STAGE_THREE = 20;

        public const string RED_REBEL = "red_rebel_icon.png";
        public const string BLUE_REBEL = "blue_rebel_icon.png";
        public const string DEFAULT_ROBOT_IMAGEPATH = "default_robot_icon.png";

        #region Index Const
        public const int ROBOT_ONE_INDEX = 0;
        public const int ROBOT_TWO_INDEX = 1;
        public const int ROBOT_THREE_INDEX = 2;
        public const int ROBOT_FOUR_INDEX = 3;
        public const int ROBOT_FIVE_INDEX = 4;
        public const int ROBOT_SIX_INDEX = 5;
        #endregion
    }
}
