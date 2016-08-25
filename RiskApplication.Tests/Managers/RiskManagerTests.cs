using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Repositories.DomainModels;
using Repositories.Interfaces;
using RiskApplication.Managers.Implementations;
using RiskApplication.ViewModels;
using AppConstants = RiskApplication.Constants.Constants;

namespace RiskApplication.Tests.Managers
{
    [TestFixture]
    public class RiskManagerTests
    {
         [Test]
         public void CanCreateRiskManagerWithConstructor()
         {
             Mock<IRiskRepository> riskRepository = new Mock<IRiskRepository>();

             RiskManager riskManager = new RiskManager(riskRepository.Object);

             Assert.IsNotNull(riskManager);
         }

         [Test]
         public void CanRetrieveSettledBetHistory()
         {
             IList<SettledBetDomainModel> settledBetDomainModels = GetSetttlBetTestDomainModels();

             IList<SettledBetHistoryViewModel> settledBetHistoryViewModels = MapToSettledBetHistoryViewModels(settledBetDomainModels);
             
             Assert.AreEqual(10, settledBetHistoryViewModels.Count);
         }

         [Test]
         public void CanRetrieveSettledBetHistoryForUnusualRateWin()
         {
             IList<SettledBetDomainModel> settledBetDomainModels = GetSetttlBetTestDomainModels();

             IList<SettledBetHistoryViewModel> settledBetHistoryViewModels = MapToSettledBetHistoryViewModels(settledBetDomainModels);

             IList<SettledBetHistoryViewModel> settledBetHistoryForUnusualrate =
                 settledBetHistoryViewModels.Where(s => s.WinPercentage > AppConstants.UnusualRate).ToList();

             Assert.AreEqual(5, settledBetHistoryForUnusualrate.Count);
         }

        [Test]
        public void CanRetrieveUnSettledBetHistory()
        {
            IList<UnSettledBetDomainModel> unsettledBetDomainModels = GetUnSettledBetTestDomainModels();

            IList<UnSettledBetHistoryViewModel> unsettledBetHistoryViewModels =
                MapToUnSettledBetHistoryViewModels(unsettledBetDomainModels);

            Assert.AreEqual(10, unsettledBetHistoryViewModels.Count);
        }

        [Test]
        public void CanRetreiveUnSettledRiskyBets()
        {
            IList<UnSettledBetDomainModel> unsettledBetDomainModels = GetUnSettledBetTestDomainModels();

            IList<UnSettledBetHistoryViewModel> unsettledBetHistoryViewModels = MapToUnSettledBetHistoryViewModels(unsettledBetDomainModels);

            IList<UnSettledBetHistoryViewModel> unsettledBetHistoryForUnusualRate =
                unsettledBetHistoryViewModels.Where(s => s.ToWinPercentage > AppConstants.UnusualRate).ToList();

            Assert.AreEqual(10, unsettledBetHistoryForUnusualRate.Count);
        }

        [Test]
        public void CanGetAverateBetForACustomer()
        {
            const int customerid = 1;

            IList<SettledBetDomainModel> settledBetDomainModels = GetSetttlBetTestDomainModels();

            IList<SettledBetHistoryViewModel> settledBetHistoryViewModels = MapToSettledBetHistoryViewModels(settledBetDomainModels);

            var betHistoryAverage = settledBetHistoryViewModels.GroupBy(t => new { t.Customer })
                .Select(g => new
                {
                    Average = g.Average(p => p.Stake),
                    ID = g.Key.Customer
                });

            var customerAvg = betHistoryAverage.FirstOrDefault(s => s.ID.Equals(customerid));

            double average = customerAvg != null ? customerAvg.Average : 0;

            Assert.AreEqual(42.5, average);
            
        }

        [Test]
        public void CanRetreiveUnSettledUnusualRiskyBets()
        {
            IList<UnSettledBetDomainModel> unsettledBetDomainModels = GetUnSettledBetTestDomainModels();

            IList<UnSettledBetHistoryViewModel> unsettledBetHistoryViewModels = MapToUnSettledBetHistoryViewModels(unsettledBetDomainModels);

            IList<UnSettledBetHistoryViewModel> unusualUnsettledBets =
                unsettledBetHistoryViewModels.Where(u => u.Stake > (AppConstants.UnusualStakeRate*GetAverageBetForCustomer(u.Customer)))
                    .ToList();
            
            Assert.AreEqual(2, unusualUnsettledBets.Count);
        }

        [Test]
        public void CanRetrieveUnsettledHighlyUnusualRiskyBets()
        {
            IList<UnSettledBetDomainModel> unsettledBetDomainModels = GetUnSettledBetTestDomainModels();

            IList<UnSettledBetHistoryViewModel> unsettledBetHistoryViewModels = MapToUnSettledBetHistoryViewModels(unsettledBetDomainModels);

            IList<UnSettledBetHistoryViewModel> highlyUnusualUnsettledBets =
                unsettledBetHistoryViewModels.Where(u => u.Stake > (AppConstants.HighlyUnusualStakeRate*GetAverageBetForCustomer(u.Customer)))
                    .ToList();

            Assert.AreEqual(1, highlyUnusualUnsettledBets.Count);
        }

        [Test]
        public void CanRetrieveUnsettledHighRiskyBets()
        {
            IList<UnSettledBetDomainModel> unsettledBetDomainModels = GetUnSettledBetTestDomainModels();

            IList<UnSettledBetHistoryViewModel> unsettledBetHistoryViewModels = MapToUnSettledBetHistoryViewModels(unsettledBetDomainModels);

            IList<UnSettledBetHistoryViewModel> highRiskyUnsettledBets = unsettledBetHistoryViewModels.Where(u => u.ToWin >= AppConstants.HighRiskWinAmountLimit).ToList();

            Assert.AreEqual(3, highRiskyUnsettledBets.Count);
        }

        private IList<SettledBetDomainModel> GetSetttlBetTestDomainModels()
        {
            IList<SettledBetDomainModel> settledBetDomainModels = new List<SettledBetDomainModel>()
            {
                new SettledBetDomainModel() {Customer = 1, Event = 1, Participant = 6, Stake = 50, Win = 250},
                new SettledBetDomainModel() {Customer = 2, Event = 1, Participant = 3, Stake = 5, Win = 0},
                new SettledBetDomainModel() {Customer = 3, Event = 1, Participant = 3, Stake = 20, Win = 0},
                new SettledBetDomainModel() {Customer = 4, Event = 1, Participant = 6, Stake = 200, Win = 1000},
                new SettledBetDomainModel() {Customer = 1, Event = 2, Participant = 1, Stake = 20, Win = 60},
                new SettledBetDomainModel() {Customer = 2, Event = 2, Participant = 1, Stake = 5, Win = 15},
                new SettledBetDomainModel() {Customer = 1, Event = 5, Participant = 10, Stake = 50, Win = 400},
                new SettledBetDomainModel() {Customer = 5, Event = 3, Participant = 6, Stake = 20, Win = 0},
                new SettledBetDomainModel() {Customer = 3, Event = 3, Participant = 5, Stake = 20, Win = 0},
                new SettledBetDomainModel() {Customer = 1, Event = 3, Participant = 5, Stake = 50, Win = 0},
            };

            return settledBetDomainModels;
        }

        private IList<SettledBetHistoryViewModel> MapToSettledBetHistoryViewModels(IList<SettledBetDomainModel> domainModels)  
        {
            return domainModels.Select(item => new SettledBetHistoryViewModel
            {
                Customer = item.Customer, Event = item.Event, Participant = item.Participant, Stake = item.Stake, Win = item.Win
            }).ToList();
        }

        private IList<UnSettledBetDomainModel> GetUnSettledBetTestDomainModels()
        {
            IList<UnSettledBetDomainModel> unSettledBetDomainModels = new List<UnSettledBetDomainModel>()
            {
                new UnSettledBetDomainModel() {Customer = 1, Event = 11, Participant = 4, Stake = 50, ToWin = 500},
                new UnSettledBetDomainModel() {Customer = 3, Event = 11, Participant = 6, Stake = 50, ToWin = 400},
                new UnSettledBetDomainModel() {Customer = 4, Event = 11, Participant = 7, Stake = 300, ToWin = 1200},
                new UnSettledBetDomainModel() {Customer = 5, Event = 11, Participant = 2, Stake = 20, ToWin = 80},
                new UnSettledBetDomainModel() {Customer = 1, Event = 12, Participant = 4, Stake = 500, ToWin = 5000},
                new UnSettledBetDomainModel() {Customer = 2, Event = 12, Participant = 4, Stake = 20, ToWin = 200},
                new UnSettledBetDomainModel() {Customer = 3, Event = 12, Participant = 1, Stake = 50, ToWin = 400},
                new UnSettledBetDomainModel() {Customer = 4, Event = 12, Participant = 1, Stake = 250, ToWin = 1000},
                new UnSettledBetDomainModel() {Customer = 5, Event = 12, Participant = 5, Stake = 100, ToWin = 800},
                new UnSettledBetDomainModel() {Customer = 6, Event = 12, Participant = 3, Stake = 50, ToWin = 400}
            };

            return unSettledBetDomainModels;
        }

        private IList<UnSettledBetHistoryViewModel> MapToUnSettledBetHistoryViewModels(IList<UnSettledBetDomainModel> domainModels)
        {
            return domainModels.Select(item => new UnSettledBetHistoryViewModel
            {
                Customer = item.Customer,
                Event = item.Event,
                Participant = item.Participant,
                Stake = item.Stake,
                ToWin = item.ToWin
            }).ToList();
        }

        private double GetAverageBetForCustomer(int customer)
        {
            IList<SettledBetDomainModel> settledBetDomainModels = GetSetttlBetTestDomainModels();

            IList<SettledBetHistoryViewModel> settledBetHistoryViewModels = MapToSettledBetHistoryViewModels(settledBetDomainModels);

            var betHistoryAverage = settledBetHistoryViewModels.GroupBy(t => new { t.Customer })
                .Select(g => new
                {
                    Average = g.Average(p => p.Stake),
                    ID = g.Key.Customer
                });

            var customerAvg = betHistoryAverage.FirstOrDefault(s => s.ID.Equals(customer));

            return customerAvg != null ? customerAvg.Average : 0;
        }
    }
}
