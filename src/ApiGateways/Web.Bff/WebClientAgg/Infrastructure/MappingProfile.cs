using ApartmentsApi.Proto;
using AutoMapper;
using OwnersAPI.Proto;
using WebClientAgg.Model;
using WebClientAgg.Model.DTO;

namespace WebClientAgg.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GrpcBedrooms, Bedrooms>();
            CreateMap<GrpcCountries, Countries>();
            CreateMap<GrpcFurnishings, Furnishings>();
            CreateMap<GrpcPeriods, Periods>();
            CreateMap<OwnerBasicResponse, BasicOwner>();
        }
    }
}
