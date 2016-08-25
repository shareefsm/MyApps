using System.Collections.Generic;
using System.Linq;
using System.Web;
using LINQtoCSV;
using Repositories.DomainModels;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class RiskRepository : IRiskRepository
    {
        public IList<SettledBetDomainModel> GetSettledBetHistoryDetails()
        {
            CsvContext csvContext = new CsvContext();

            IList<SettledBetDomainModel> settledDomainModels = csvContext.Read<SettledBetDomainModel>(
                                            HttpContext.Current.Server.MapPath("~/App_Data/Settled.csv")).ToList();

            return settledDomainModels;
        }

        public IList<UnSettledBetDomainModel> GetUnSettledBetHistoryDetails()
        {
            CsvContext csvContext = new CsvContext();

            IList<UnSettledBetDomainModel> unSettledDomainModels = csvContext.Read<UnSettledBetDomainModel>(
                                            HttpContext.Current.Server.MapPath("~/App_Data/Unsettled.csv")).ToList();

            return unSettledDomainModels;
        }
    }
}