using AutoMapper;
using Data.Dtos;
using Data.Entities;

namespace WetFeetAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<SubscriptionPlan, SubscriptionPlanDto>().ReverseMap();
            }
            );

            return mappingConfig;

        }
    }
}
