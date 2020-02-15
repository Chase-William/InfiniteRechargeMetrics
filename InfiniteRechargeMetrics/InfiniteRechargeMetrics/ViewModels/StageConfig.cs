using System;
using System.Collections.Generic;
using System.Text;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     Struct containing data related to the value of ports.
    /// </summary>
    public struct StageConfig
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
    }
}
