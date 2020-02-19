using SQLite;

namespace InfiniteRechargeMetrics.Models
{
    [Table("Team")]
    public class Team
    {     
        /// <summary>
        ///     The name or title of the team.
        /// </summary>
        [Column("name"), PrimaryKey, Unique]
        public string Name { get; set; }
        [Column("home_team")]
        public bool IsHomeTeam { get; set; }
        [Column("image_path")]
        public string ImagePath { get; set; }
    }
}
