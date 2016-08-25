using System.Collections.Generic;
using RiskApplication.Enums;
using RiskApplication.ViewModels;

namespace RiskApplication.Managers.Interfaces
{
    public interface IRiskManager
    {
        IList<SettledBetHistoryViewModel> GetSettledBetHistoryForUnusualRateWin(decimal unusualRate);

        IList<UnSettledBetHistoryViewModel> GetUnSettledRiskBets(RiskTypes riskType);
    }
}