using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     This is a class to just implement the INotifyPropertyFunctions for any drived class..
    ///       
    ///     -- This could maybe make more sense in C# 8.0 because we can provide default Interface method implementation instead of this.    
    /// 
    /// </summary>
    public abstract class NotifyClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     To use this method; YOU MUST SPECIFY PROPERTY NAME.
        /// </summary>
        protected void NotifyPropertiesChanged(params string[] propertyNames)
        {
            foreach (string propName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
