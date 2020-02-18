using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     Interface allows for the use of the parent class to call a specific child's function
    /// </summary>s
    public interface IStageViewModel
    {
        /// <summary>
        ///     Based of implementation; should check if the conditions are met to declare the current stage as complete.
        /// </summary>
        void CheckIfStageIsComplete();
    }
}
