using System.Collections.Generic;
using Repositories.DomainModels;

namespace Repositories.Interfaces
{
    public interface IRiskRepository
    {
        IList<SettledBetDomainModel> GetSettledBetHistoryDetails();

        IList<UnSettledBetDomainModel> GetUnSettledBetHistoryDetails();
    }
}