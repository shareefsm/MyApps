using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.DomainModels;

namespace Repositories.Interfaces
{
    public interface IRiskRepository
    {
        BetHistoryDomainModel GetBetStake();
    }
}
