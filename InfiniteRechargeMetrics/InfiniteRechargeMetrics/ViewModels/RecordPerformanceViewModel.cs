//using InfiniteRechargeMetrics.Models;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Windows.Input;
//using Xamarin.Forms;

//namespace InfiniteRechargeMetrics.ViewModels
//{
//    /// <summary>
//    ///     ViewModel that carries out the intermediate functions for the Team's Performance UI and Performance type
//    /// </summary>
//    class RecordPerformanceViewModel : INotifyPropertyChanged
//    {
//        private const string ADD_POINTS = "+";
//        private const string SUBTRACT_POINTS = "-";

//        private const int AUTO_LPP = 2;
//        private const int AUTO_UPP = 4;
//        private const int AUTO_SPP = 6;
//        private const int MANUAL_LPP = 1;
//        private const int MANUAL_UPP = 2;
//        private const int MANUAL_SPP = 3;

//        public event PropertyChangedEventHandler PropertyChanged;
//        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }

//        public Performance Performance { get; private set; } = new Performance();

//        public int AutoLowPortPoints
//        {
//            get => Performance.AutoLowPortPoints;
//            set
//            {
//                if (value < 0) return;
//                Performance.AutoLowPortPoints = value;
//                NotifyPropertyChanged(nameof(AutoLowPortPoints));
//            }
//        }
//        public int AutoUpperPortPoints
//        {
//            get => Performance.AutoUpperPortPoints;
//            set
//            {
//                if (value < 0) return;
//                Performance.AutoUpperPortPoints = value;
//                NotifyPropertyChanged(nameof(AutoUpperPortPoints));
//            }
//        }
//        public int AutoSmallPortPoints
//        {
//            get => Performance.AutoSmallPortPoints;
//            set
//            {
//                if (value < 0) return;
//                Performance.AutoSmallPortPoints = value;
//                NotifyPropertyChanged(nameof(AutoSmallPortPoints));
//            }
//        }
//        public int ManualLowPortPoints
//        {
//            get => Performance.StageOneLowPortPoints;
//            set
//            {
//                if (value < 0) return;
//                Performance.StageOneLowPortPoints = value;
//                NotifyPropertyChanged(nameof(ManualLowPortPoints));
//            }
//        }
//        public int ManualUpperPortPoints
//        {
//            get => Performance.StageOneUpperPortPoints;
//            set
//            {
//                if (value < 0) return;
//                Performance.StageOneUpperPortPoints = value;
//                NotifyPropertyChanged(nameof(ManualUpperPortPoints));
//            }
//        }
//        public int ManualSmallPortPoints
//        {
//            get => Performance.StageOneSmallPortPoints;
//            set
//            {
//                if (value < 0) return;
//                Performance.StageOneSmallPortPoints = value;
//                NotifyPropertyChanged(nameof(ManualSmallPortPoints));
//            }
//        }


//        public ICommand ChangePoints { get; private set; }

//        public RecordPerformanceViewModel()
//        {
//            ChangePoints = new Command(DetermineNextAction);
//        }

//        /// <summary>
//        ///     Determines the first step of changing the points process be evaluating the caller's param
//        /// </summary>
//        private void DetermineNextAction(object _portIdentifier)
//        {
//            if (_portIdentifier == null) return;

//            string portIdentifier = (string)_portIdentifier;

//            if (!int.TryParse(portIdentifier.Substring(1, 1), out int port)) return;

//            switch (port)
//            {
//                // Autonomous Mode Low Port,   2 points
//                case (int)PortIdentifier.AutoLowPort:
//                    AutoLowPortPoints = ChangePortPoints(AutoLowPortPoints, AUTO_LPP);
//                    break;
//                // Autonomous Mode Upper Port, 4 points
//                case (int)PortIdentifier.AutoUpperPort:
//                    AutoUpperPortPoints = ChangePortPoints(Performance.AutoUpperPortPoints, AUTO_UPP);
//                    break;
//                // Autonomous Mode Small Port, 6 points
//                case (int)PortIdentifier.AutoSmallPort:
//                    AutoSmallPortPoints = ChangePortPoints(Performance.AutoSmallPortPoints, AUTO_SPP);
//                    break;

//                // Manual Mode Lower Port,     1 points
//                case (int)PortIdentifier.ManualLowPort:
//                    ManualLowPortPoints = ChangePortPoints(Performance.StageOneLowPortPoints, MANUAL_LPP);
//                    break; 
//                // Manual Upper Port,          2 points
//                case (int)PortIdentifier.ManualUpperPort:
//                    ManualUpperPortPoints = ChangePortPoints(Performance.StageOneUpperPortPoints, MANUAL_UPP);
//                    break;
//                // Autonomous Mode Small Port, 3 points
//                case (int)PortIdentifier.ManualSmallPort:
//                    ManualSmallPortPoints = ChangePortPoints(Performance.StageOneSmallPortPoints, MANUAL_SPP);
//                    break;
//                default:
//                    break;
//            }

//            // Modifies Performance port's points 
//            int ChangePortPoints(int thisPort, int _points)
//            {
//                if (portIdentifier.Substring(0, 1).Equals(ADD_POINTS))
//                {
//                    return thisPort += _points;
//                }
//                else
//                {
//                    return thisPort -= _points;
//                }
//            }
//        } 
//    }

//    //enum PortIdentifier { AutoLowPort, AutoUpperPort, AutoSmallPort, ManualLowPort, ManualUpperPort, ManualSmallPort }
//}
