using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.SharedCustomViews
{
    public class CustomEditor : Editor
    {
        public CustomEditor()
        {
            this.TextChanged += delegate
            {
                this.InvalidateMeasure();
            };
        }
    }
}
