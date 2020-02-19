using InfiniteRechargeMetrics.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteRechargeMetrics.Data
{
    interface IDatabaseContext
    {
        Task SavePerformanceToLocalDB(Performance _performance);
        Task<List<Performance>> GetAllPerformancesFromTeam(string _teamName);
    }
}
