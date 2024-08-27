using AutoMapper;
using HouseUnitAPI.Models;
using HouseUnitAPI.ViewModels;
using static HouseUnitAPI.Helpers.EnumHelper;

namespace HouseUnitAPI.Mappers
{
    public class HouseUnitMappingProfile : Profile
    {
        public HouseUnitMappingProfile()
        {
            // Mapper to map view models from entity model
            CreateMap<HouseUnit, BaseHouseUnit>();
            CreateMap<HouseUnit, ViewHouseUnit>();
            CreateMap<HouseUnit, ResponseViewHouseUnit>();

            CreateMap<HouseUnit, HouseUnitDetails>().
                ForMember(dest=>dest.UnitType,opt=>opt.MapFrom(src=>Enum.Parse<HouseUnitType>(src.UnitType))).
                ForMember(dest=>dest.Features, opt=>opt.MapFrom(src=>src.Features.Select(f=> Enum.Parse<FeatureType>(f)).ToArray()));

            //Mapper to map view model to entity model
            CreateMap<HouseUnitDetails, HouseUnit>().
                ForMember(dest => dest.UnitType, opt => opt.MapFrom(src => src.UnitType.ToString())).
                ForMember(dest=>dest.Features, opt=>opt.MapFrom(src=>src.Features.Select(f=>f.ToString()).ToArray()));
            CreateMap<BaseHouseUnit, HouseUnit>();
        }
    }
}
