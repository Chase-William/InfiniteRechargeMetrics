using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace InfiniteRechargeMetrics.Models
{
    public class UserProfile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Uri Avatar { get; set; }
    }
}
