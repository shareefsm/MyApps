using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Moq;
using NUnit.Framework;
using RiskApplication.Controllers;
using RiskApplication.Enums;
using RiskApplication.Managers.Interfaces;
using RiskApplication.ViewModels;

namespace RiskApplication.Tests.Controllers
{
    [TestFixture]
    public class ControllerTestClass
    {
        [Test]
        public void TestRiskIndex()
        {
            Mock<IRiskManager> riskManager = new Mock<IRiskManager>();
            RiskController riskController = new RiskController(riskManager.Object);

            ViewResult actionResult = riskController.Index() as ViewResult;

            Assert.That(actionResult.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void TestGetSettledBetHistoryForUnusualRateWin()
        {
            IList<SettledBetHistoryViewModel> settledBetHistory = new List<SettledBetHistoryViewModel>
            {
                new SettledBetHistoryViewModel {Customer = 1, Event = 1, Participant = 6, Stake = 50, Win = 250}
            };

            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            string result = jsonSerialiser.Serialize(settledBetHistory);


            Assert.AreEqual("[{\"Customer\":1,\"Event\":1,\"Participant\":6,\"Stake\":50,\"Win\":250,\"WinPercentage\":500}]", result);
        }

        [Test]
        public void TestGetUnSettledBetsForRiskyCase()
        {
            IList<UnSettledBetHistoryViewModel> unSettledRiskBets = GetUnSettledRiskBets(RiskTypes.Risky);

            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            string result = jsonSerialiser.Serialize(unSettledRiskBets);

            Assert.AreEqual("[{\"Customer\":1,\"Event\":1,\"Participant\":6,\"Stake\":100,\"ToWin\":500,\"ToWinPercentage\":500}]", result);
        }

        [Test]
        public void TestGetUnSettledBetsForUnusualCase()
        {
            IList<UnSettledBetHistoryViewModel> unSettledRiskBets = GetUnSettledRiskBets(RiskTypes.Unusual);

            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            string result = jsonSerialiser.Serialize(unSettledRiskBets);

            Assert.AreEqual("[{\"Customer\":2,\"Event\":1,\"Participant\":5,\"Stake\":100,\"ToWin\":350,\"ToWinPercentage\":300}]", result);
        }

        [Test]
        public void TestGetUnSettledBetsForHighlyUnusualCase()
        {
            IList<UnSettledBetHistoryViewModel> unSettledRiskBets = GetUnSettledRiskBets(RiskTypes.HighlyUnusual);

            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            string result = jsonSerialiser.Serialize(unSettledRiskBets);

            Assert.AreEqual("[{\"Customer\":3,\"Event\":2,\"Participant\":6,\"Stake\":200,\"ToWin\":250,\"ToWinPercentage\":100}]", result);
        }

        [Test]
        public void TestGetUnSettledBetsForHighlyHighRiskCase()
        {
            IList<UnSettledBetHistoryViewModel> unSettledRiskBets = GetUnSettledRiskBets(RiskTypes.HighRisk);

            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            string result = jsonSerialiser.Serialize(unSettledRiskBets);

            Assert.AreEqual("[{\"Customer\":1,\"Event\":3,\"Participant\":5,\"Stake\":100,\"ToWin\":550,\"ToWinPercentage\":500}]", result);
        }

        private IList<UnSettledBetHistoryViewModel> GetUnSettledRiskBets(RiskTypes riskType)
        {
            IList<UnSettledBetHistoryViewModel> unSettledRiskBets = null;

            switch (riskType)
            {
                case RiskTypes.Risky:
                    unSettledRiskBets = new List<UnSettledBetHistoryViewModel>
                        {
                            new UnSettledBetHistoryViewModel {Customer = 1, Event = 1, Participant = 6, Stake = 100, ToWin = 500}
                        };
                    break;
                case RiskTypes.Unusual:
                    unSettledRiskBets = new List<UnSettledBetHistoryViewModel>
                        {
                            new UnSettledBetHistoryViewModel {Customer = 2, Event = 1, Participant = 5, Stake = 100, ToWin = 350}
                        };
                    break;
                case RiskTypes.HighlyUnusual:
                    unSettledRiskBets = new List<UnSettledBetHistoryViewModel>
                        {
                            new UnSettledBetHistoryViewModel {Customer = 3, Event = 2, Participant = 6, Stake = 200, ToWin = 250}
                        };
                    break;
                case RiskTypes.HighRisk:
                    unSettledRiskBets = new List<UnSettledBetHistoryViewModel>
                        {
                            new UnSettledBetHistoryViewModel {Customer = 1, Event = 3, Participant = 5, Stake = 100, ToWin = 550}
                        };
                    break;
            }

            return unSettledRiskBets;
        }

    }
}
