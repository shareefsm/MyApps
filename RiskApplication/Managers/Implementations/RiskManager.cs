using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Repositories.DomainModels;
using Repositories.Interfaces;
using RiskApplication.Enums;
using RiskApplication.Managers.Interfaces;
using RiskApplication.ViewModels;
using AppConstants = RiskApplication.Constants.Constants;

namespace RiskApplication.Managers.Implementations
{
    public class RiskManager : IRiskManager
    {
        private readonly IRiskRepository _riskRepository;
        public RiskManager(IRiskRepository riskRepository)
        {
            _riskRepository = riskRepository;
        }

        public IList<SettledBetHistoryViewModel> GetSettledBetHistoryForUnusualRateWin(decimal unusualRate)
        {
            IList<SettledBetDomainModel> settledBetDomainModels = _riskRepository.GetSettledBetHistoryDetails();

            IList<SettledBetHistoryViewModel> settledBetHistoryViewModels = Mapper.Map<IList<SettledBetDomainModel>, IList<SettledBetHistoryViewModel>>(settledBetDomainModels);

            return settledBetHistoryViewModels.Where(s => s.WinPercentage > unusualRate).ToList();
        }

        public IList<UnSettledBetHistoryViewModel> GetUnSettledRiskBets(RiskTypes riskType)
        {
            IList<UnSettledBetHistoryViewModel> unSettledRiskBets = null;

            switch (riskType)
            {
                case RiskTypes.Risky:
                    unSettledRiskBets = GetUnSettledBetHistoryForUnusualRateToWin(AppConstants.UnusualRate);
                    break;
                case RiskTypes.Unusual:
                    unSettledRiskBets = GetUnusualUnsettledBets(AppConstants.UnusualStakeRate);
                    break;
                case RiskTypes.HighlyUnusual:
                    unSettledRiskBets = GetUnusualUnsettledBets(AppConstants.HighlyUnusualStakeRate);
                    break;
                case RiskTypes.HighRisk:
                    unSettledRiskBets = GetUnSettledBetsFilterByWinAmount(AppConstants.HighRiskWinAmountLimit);
                    break;
            }

            return unSettledRiskBets;
        }

        private IList<UnSettledBetHistoryViewModel> GetUnSettledBetHistoryForUnusualRateToWin(decimal unusualRate)
        {
            IList<UnSettledBetDomainModel> unSettledBetDomainModels = _riskRepository.GetUnSettledBetHistoryDetails();

            IList<UnSettledBetHistoryViewModel> unSettledBetHistoryViewModels = Mapper.Map<IList<UnSettledBetDomainModel>,
                                            IList<UnSettledBetHistoryViewModel>>(unSettledBetDomainModels);

            return unSettledBetHistoryViewModels.Where(u => u.ToWinPercentage > unusualRate).ToList();
        }

        private IList<UnSettledBetHistoryViewModel> GetUnusualUnsettledBets(int stakeRate)
        {
            IList<UnSettledBetDomainModel> unSettledBetDomainModels = _riskRepository.GetUnSettledBetHistoryDetails();

            IList<UnSettledBetHistoryViewModel> unSettledBetHistoryViewModels = Mapper.Map<IList<UnSettledBetDomainModel>,
                                            IList<UnSettledBetHistoryViewModel>>(unSettledBetDomainModels);

            return unSettledBetHistoryViewModels.Where(u => u.Stake > (stakeRate * GetAverageBetForCustomer(u.Customer))).ToList();
        }

        private IList<UnSettledBetHistoryViewModel> GetUnSettledBetsFilterByWinAmount(int amountToWin)
        {
            IList<UnSettledBetDomainModel> unSettledBetDomainModels = _riskRepository.GetUnSettledBetHistoryDetails();

            IList<UnSettledBetHistoryViewModel> unSettledBetHistoryViewModels = Mapper.Map<IList<UnSettledBetDomainModel>,
                                            IList<UnSettledBetHistoryViewModel>>(unSettledBetDomainModels);

            return unSettledBetHistoryViewModels.Where(u => u.ToWin >= amountToWin).ToList();
        }

        private double GetAverageBetForCustomer(int customer)
        {
            IList<SettledBetDomainModel> settledBetDomainModels = _riskRepository.GetSettledBetHistoryDetails();

            IList<SettledBetHistoryViewModel> settledBetHistoryViewModels = Mapper.Map<IList<SettledBetDomainModel>, IList<SettledBetHistoryViewModel>>(settledBetDomainModels);

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