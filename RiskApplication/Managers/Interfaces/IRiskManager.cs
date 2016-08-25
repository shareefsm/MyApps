using RiskApplication.ViewModels;

namespace RiskApplication.Managers.Interfaces
{
    public interface IRiskManager
    {
        BetHistoryViewModel GetBetStake();
    }
}