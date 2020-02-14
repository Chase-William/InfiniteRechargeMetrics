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
        void HandleChangePointsBtnClicked(object keyChar);
        ICommand ChangePointsCMD { get; set; }
    }
}
