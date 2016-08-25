using System.Collections.Generic;
using System.Linq;
using LINQtoCSV;
using NUnit.Framework;
using Repositories.DomainModels;

namespace Repositories.Tests.Repository
{
    [TestFixture]
    public class RiskRepositoryTests
    {
        [Test]
        public void RetrieveSettledBetsFromCsvFile()
        {
            CsvContext csvContext = new CsvContext();

            IList<SettledBetDomainModel> settledDomainModels = csvContext.Read<SettledBetDomainModel>("../../Settled.csv").ToList();
            
            Assert.AreEqual(50, settledDomainModels.Count);
        }

        [Test]
        public void RetrieveUnSettledBetsFromCsvFile()
        {
            CsvContext csvContext = new CsvContext();

            IList<UnSettledBetDomainModel> unsettledDomainModels = csvContext.Read<UnSettledBetDomainModel>("../../Unsettled.csv").ToList();

            Assert.AreEqual(22, unsettledDomainModels.Count);
        }

    }
}