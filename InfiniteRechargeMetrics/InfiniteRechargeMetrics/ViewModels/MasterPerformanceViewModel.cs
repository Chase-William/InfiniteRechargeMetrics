using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.PerformanceContent.PerformanceViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class MasterPerformanceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public StageOneView StageOneView { get; set; }
        public StageTwoView StageTwoView { get; set; }
        public StageThreeView StageThreeView { get; set; }

        private int selectedIndex;
        public int SelectedIndex
        { 
            get => selectedIndex;            
            set
            {
                selectedIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedIndex)));
            }
        }       

        public MasterPerformanceViewModel(Performance _performance) 
        {
            StageOneView = new StageOneView(_performance);
            StageTwoView = new StageTwoView(_performance);
            StageThreeView = new StageThreeView(_performance);
        }
    }
}
