using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfiniteRechargeMetrics.Models
{
    [Table("Robot")]
    public class Robot
    {
        [Column("robot_id"), PrimaryKey, Unique]
        public string RobotId { get; set; }
        [Column("robot_alias")]
        public string RobotAlias { get; set; }
        [Column("robot_info")]  
        public string RobotInfo { get; set; }
        [Column("image_path")]
        public string ImagePath { get; set; }        
    }
}
