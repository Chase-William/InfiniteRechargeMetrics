using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.PerformancePages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     enum for determining wether the stage is in autonomous mode or manuals
    /// </summary>
    public enum StageState { Autononmous, Manual }

    /// <summary>
    ///     Base class for all stage viewmodels
    /// </summary>
    public abstract class StageViewModelBase : INotifyPropertyChanged
    {
        private const string ADD_POINTS = "+";

        private const int AUTO_LPP = 2;
        private const int AUTO_UPP = 4;
        private const int AUTO_SPP = 6;
        private const int MANUAL_LPP = 1;
        private const int MANUAL_UPP = 2;
        private const int MANUAL_SPP = 3;

        private Performance performance;
        public Performance Performance
        {
            get => performance;
            set => performance = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public StageViewModelBase(Performance _performance)
        {
            Performance = _performance;
        }

        protected void ChangePoints(StageViewModelBase _viewModel, string entireCode)
        {
            // if nothing was passed in the code, return
            if (entireCode == null) return;
            // Parsing to a number so we can use an enum to make this more readable
            if (!int.TryParse(entireCode.Substring(1, 1), out int portIndex)) return;
            // informs whether we shall add or subtract based off the code
            string portOperator = entireCode.Substring(0, 1);

            if (_viewModel is StageOneViewModel stageOneViewModel)
            {
                if (stageOneViewModel.StageState == StageState.Autononmous)
                {
                    switch (portIndex)
                    {
                        // Autonomous Mode Low Port,   2 points
                        case (int)PortIdentifier.AutoLowPort:
                            stageOneViewModel.AutoLowPortPoints = ChangePortPoints(stageOneViewModel.AutoLowPortPoints, AUTO_LPP);
                            break;
                        // Autonomous Mode Upper Port, 4 points
                        case (int)PortIdentifier.AutoUpperPort:
                            stageOneViewModel.AutoUpperPortPoints = ChangePortPoints(stageOneViewModel.AutoUpperPortPoints, AUTO_UPP);
                            break;
                        // Autonomous Mode Small Port, 6 points
                        case (int)PortIdentifier.AutoSmallPort:
                            stageOneViewModel.AutoSmallPortPoints = ChangePortPoints(stageOneViewModel.AutoSmallPortPoints, AUTO_SPP);
                            break;
                        default:
                            break;
                    }
                }                    
            }
            else if (_viewModel is StageTwoViewModel stageTwoViewModel)
            {
                
            }
            else if (_viewModel is StageThreeViewModel stageThreeViewModel)
            {

            }        

            // Modifies Performance port's points 
            int ChangePortPoints(int thisPort, int _points)
            {
                if (portOperator.Equals(ADD_POINTS))
                {
                    return thisPort += _points;
                }
                else
                {
                    return thisPort -= _points;
                }
            }
        }
    }

    enum PortIdentifier { AutoLowPort, AutoUpperPort, AutoSmallPort, ManualLowPort, ManualUpperPort, ManualSmallPort }
}
