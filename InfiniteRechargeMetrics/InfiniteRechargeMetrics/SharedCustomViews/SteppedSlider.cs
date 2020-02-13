using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.SharedCustomViews
{
    class SteppedSlider : Slider
    {
        public SteppedSlider()
        {
            this.ValueChanged += SteppedSlider_ValueChanged;
        }

        private void SteppedSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {            
            this.Value = Math.Round(e.NewValue);
        }
    }
}
