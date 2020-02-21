using System;
using System.Collections.Generic;
using System.Text;

namespace InfiniteRechargeMetrics.ViewModels.HomeVM
{
    public class HomeTeamViewModelBase : NotifyClass
    {
        /// <summary>
        ///     Determines whether the toolbar item for editting a team should be visible.
        /// </summary>
        private bool isTeamEdittingPossible;
        public bool IsTeamEdittingPossible { 
            get => isTeamEdittingPossible; 
            set
            {                
                isTeamEdittingPossible = value;
                NotifyPropertyChanged();
            } 
        }
    }
}
