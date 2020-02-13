using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.PerformancePages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace InfiniteRechargeMetrics.ViewModels
{
    public abstract class StageViewModelBase : INotifyPropertyChanged
    {
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

        /// <summary>
        ///     Determines the first step of changing the points process be evaluating the caller's param
        /// </summary>
        private void DetermineNextAction(object _portIdentifier)
        {
            if (_portIdentifier == null) return;

            string portIdentifier = (string)_portIdentifier;

            if (!int.TryParse(portIdentifier.Substring(1, 1), out int port)) return;

            switch (port)
            {
                // Autonomous Mode Low Port,   2 points
                case (int)PortIdentifier.AutoLowPort:
                    AutoLowPortPoints = ChangePortPoints(AutoLowPortPoints, AUTO_LPP);
                    break;
                // Autonomous Mode Upper Port, 4 points
                case (int)PortIdentifier.AutoUpperPort:
                    AutoUpperPortPoints = ChangePortPoints(Performance.AutoUpperPortPoints, AUTO_UPP);
                    break;
                // Autonomous Mode Small Port, 6 points
                case (int)PortIdentifier.AutoSmallPort:
                    AutoSmallPortPoints = ChangePortPoints(Performance.AutoSmallPortPoints, AUTO_SPP);
                    break;

                // Manual Mode Lower Port,     1 points
                case (int)PortIdentifier.ManualLowPort:
                    ManualLowPortPoints = ChangePortPoints(Performance.ManualLowPortPoints, MANUAL_LPP);
                    break;
                // Manual Upper Port,          2 points
                case (int)PortIdentifier.ManualUpperPort:
                    ManualUpperPortPoints = ChangePortPoints(Performance.ManualUpperPortPoints, MANUAL_UPP);
                    break;
                // Autonomous Mode Small Port, 3 points
                case (int)PortIdentifier.ManualSmallPort:
                    ManualSmallPortPoints = ChangePortPoints(Performance.ManualSmallPortPoints, MANUAL_SPP);
                    break;
                default:
                    break;
            }

            // Modifies Performance port's points 
            int ChangePortPoints(int thisPort, int _points)
            {
                if (portIdentifier.Substring(0, 1).Equals(ADD_POINTS))
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
