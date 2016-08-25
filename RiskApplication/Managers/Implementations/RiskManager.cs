using System;
using AutoMapper;
using Repositories.DomainModels;
using Repositories.Interfaces;
using RiskApplication.Managers.Interfaces;
using RiskApplication.ViewModels;

namespace RiskApplication.Managers.Implementations
{
    public class RiskManager : IRiskManager
    {
        private readonly IRiskRepository _riskRepository;
        public RiskManager(IRiskRepository riskRepository)
        {
            _riskRepository = riskRepository;
        }

        public BetHistoryViewModel GetBetStake()
        {
            BetHistoryDomainModel domainModel = _riskRepository.GetBetStake();

            BetHistoryViewModel viewModel = Mapper.Map<BetHistoryDomainModel, BetHistoryViewModel>(domainModel);

            return viewModel;
        }
    }
}