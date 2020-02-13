using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.SharedCustomViews
{
    class CustomEditor : Editor
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
