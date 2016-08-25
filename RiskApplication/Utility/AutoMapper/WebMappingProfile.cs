using AutoMapper;
using Repositories.DomainModels;
using RiskApplication.ViewModels;

namespace RiskApplication.Utility.AutoMapper
{
    public class WebMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "WebProfile"; }
        }

        protected override void Configure()
        {
            CreateMap<BetHistoryDomainModel, BetHistoryViewModel>();
        }
    }
}