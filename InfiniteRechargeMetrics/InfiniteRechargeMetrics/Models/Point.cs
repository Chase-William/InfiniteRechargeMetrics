using SQLite;
//using static InfiniteRechargeMetrics.Models.PointHelper;

namespace InfiniteRechargeMetrics.Models
{
    /// <summary>
    ///     This class represents a point a team get during a performance.
    ///     
    /// </summary>
    [Table("Point")]
    public class Point
    {
        #region Stage Codes
        private const string AUTONOMOUS = "autononmous";
        private const string STAGE_ONE = "stage_one";
        private const string STAGE_TWO = "stage_two";
        private const string STAGE_THREE = "stage_three";
        #endregion Stage Codes End

        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        ///     The id that matches the performance this click belonged to
        /// </summary>
        [Column("performance_id")]
        public int PerformanceId { get; set; }
        [Column("value")]
        public int Value { get; set; }
        [Column("point_type")]
        public string PointType { get; set; }
        /// <summary>
        ///     Point timer tracks the timing between either round start or the last point depending on context.
        ///     The value is in MILISECONDS.
        /// </summary>
        [Column("point_timer")]
        public int PointTimer { get; set; }

        /// <summary>
        ///     Method for setting the point type from an enum to the string
        /// </summary>
        public void SetPointType(PointType _type)
        {
            switch (_type)
            {
                case Models.PointType.Automonous:
                    PointType = AUTONOMOUS;
                    break;
                case Models.PointType.StageOne:
                    PointType = STAGE_ONE;
                    break;
                case Models.PointType.StageTwo:
                    PointType = STAGE_TWO;
                    break;
                case Models.PointType.StageThree:
                    PointType = STAGE_THREE;
                    break;
                default:
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
                case AUTONOMOUS:
                    return Models.PointType.Automonous;
                case STAGE_ONE:
                    return Models.PointType.StageOne;
                case STAGE_TWO:
                    return Models.PointType.StageTwo;
                case STAGE_THREE:
                    return Models.PointType.StageThree;                
            }
            return Models.PointType.Error;
        }
    }

    public enum PointType { Automonous, StageOne, StageTwo, StageThree, Error }
}
