using System.Collections.Generic;
using AutoMapper;

namespace RiskApplication.Utility.AutoMapper
{
    public static class AutoMaps
    {
        public static void Configure(IList<Profile> profiles)
        {
            foreach (var profile in profiles)
            {
                Configure(profile);
            }
        }

        public static void Configure(Profile profile)
        {
            Mapper.AddProfile(profile);
        }
    }
}