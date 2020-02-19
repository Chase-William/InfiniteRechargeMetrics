using SQLite;
//using static InfiniteRechargeMetrics.Models.PointHelper;

namespace InfiniteRechargeMetrics.Models
{
    /// <summary>
    ///     Enum of the types of way we decribe points
    /// </summary>
    public enum PointType { AutomonousLow, AutomonousUpper, AutomonousSmall, StageOneLow, StageOneUpper, StageOneSmall, StageTwoLow, StageTwoUpper, StageTwoSmall, StageThreeLow, StageThreeUpper, StageThreeSmall, Error }

    /// <summary>
    ///     This class represents a point a team get during a performance.
    /// </summary>
    [Table("Point")]
    public class Point
    {
        #region Stage Codes
        private const string AUTONOMOUS_LOW    = "autononmous_low";
        private const string AUTONOMOUS_UPPER  = "autononmous_upper";
        private const string AUTONOMOUS_SMALL  = "autononmous_small";

        private const string STAGE_ONE_LOW     = "stage_one_low";
        private const string STAGE_ONE_UPPER   = "stage_one_upper";
        private const string STAGE_ONE_SMALL   = "stage_one_small";

        private const string STAGE_TWO_LOW     = "stage_two_low";
        private const string STAGE_TWO_UPPER   = "stage_two_upper";
        private const string STAGE_TWO_SMALL   = "stage_two_small";

        private const string STAGE_THREE_LOW   = "stage_three_low";
        private const string STAGE_THREE_UPPER = "stage_three_upper";
        private const string STAGE_THREE_SMALL = "stage_three_small";

        #endregion Stage Codes End

        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        ///     The id that matches the performance this click belonged to
        /// </summary>
        [Column("performance_id")]
        public int PerformanceId { get; set; }
        [Column("point_type")]
        public string PointType { get; set; }
        /// <summary>
        ///     Point timer tracks the timing between either round start or the last point depending on context.
        ///     The value is in MILLISECONDS.
        /// </summary>
        [Column("time_clicked_from_start")]
        public int TimeClickedFromStart { get; set; }

        /// <summary>
        ///     Method for setting the point type from an enum to the string
        /// </summary>
        public void SetPointType(PointType _type)
        {
            switch (_type)
            {
                    // Autononmous Mode / Stage One
                case Models.PointType.AutomonousLow:
                    PointType = AUTONOMOUS_LOW;
                    break;
                case Models.PointType.AutomonousUpper:
                    PointType = AUTONOMOUS_UPPER;
                    break;
                case Models.PointType.AutomonousSmall:
                    PointType = AUTONOMOUS_SMALL;
                    break;
                    // Stage One
                case Models.PointType.StageOneLow:
                    PointType = STAGE_ONE_LOW;
                    break;
                case Models.PointType.StageOneUpper:
                    PointType = STAGE_ONE_UPPER;
                    break;
                case Models.PointType.StageOneSmall:
                    PointType = STAGE_ONE_SMALL;
                    break;
                    // Stage Two
                case Models.PointType.StageTwoLow:
                    PointType = STAGE_TWO_LOW;
                    break;
                case Models.PointType.StageTwoUpper:
                    PointType = STAGE_TWO_UPPER;
                    break;
                case Models.PointType.StageTwoSmall:
                    PointType = STAGE_TWO_SMALL;
                    break;
                    // Stage Three
                case Models.PointType.StageThreeLow:
                    PointType = STAGE_THREE_LOW;
                    break;
                case Models.PointType.StageThreeUpper:
                    PointType = STAGE_THREE_UPPER;
                    break;
                case Models.PointType.StageThreeSmall:
                    PointType = STAGE_THREE_SMALL;
                    break;
            }
        }

        /// <summary>
        ///     Method for getting point type as an enum
        /// </summary>
        public PointType GetPointType()
        {
            switch (PointType)
            {
                // Autononmous Mode / Stage One
                case AUTONOMOUS_LOW:
                    return Models.PointType.AutomonousLow;
                case AUTONOMOUS_UPPER:
                    return Models.PointType.AutomonousUpper;
                case AUTONOMOUS_SMALL:
                    return Models.PointType.AutomonousSmall;
                // Stage One
                case STAGE_ONE_LOW:
                    return Models.PointType.StageOneLow;
                case STAGE_ONE_UPPER:
                    return Models.PointType.StageOneUpper;
                case STAGE_ONE_SMALL:
                    return Models.PointType.StageOneSmall;
                // Stage Two
                case STAGE_TWO_LOW:
                    return Models.PointType.StageTwoLow;
                case STAGE_TWO_UPPER:
                    return Models.PointType.StageTwoUpper;
                case STAGE_TWO_SMALL:
                    return Models.PointType.StageTwoSmall;
                // Stage Three
                case STAGE_THREE_LOW:
                    return Models.PointType.StageThreeLow;
                case STAGE_THREE_UPPER:
                    return Models.PointType.StageThreeUpper;
                case STAGE_THREE_SMALL:
                    return Models.PointType.StageThreeSmall;
            }
            return Models.PointType.Error;
        }
    }
}
