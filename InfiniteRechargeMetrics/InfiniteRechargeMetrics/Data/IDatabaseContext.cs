using InfiniteRechargeMetrics.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteRechargeMetrics.Data
{
    interface IDatabaseContext
    {
        Task SaveTeamToLocalDBAsync(Team _team);
        Task SaveMatchToLocalDBAsync(Match _performance);
        Task<List<Match>> GetAllMatchesForTeamAsync(string _teamName);
        Task<string[]> GetAllTeamsIdPlusName();
        Team GetHomeTeam();
    }
}
