using Repositories.DomainModels;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class RiskRepository : IRiskRepository
    {
        public BetHistoryDomainModel GetBetStake()
        {
            return new BetHistoryDomainModel {Stake = 1000};
        }
    }
}