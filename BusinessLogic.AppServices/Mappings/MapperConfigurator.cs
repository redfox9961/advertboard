using AutoMapper;
using BusinessLogic.Model.Dtos;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mappings
{
    /// <summary>
    /// Конфигурация маппинга, выполняющегося при помощи библиотеки AutoMapper.
    /// </summary>
    public static class MapperConfigurator
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<City, CityDto>();
                cfg.CreateMap<CityDto, City>();

                cfg.CreateMap<Advert, AdvertDto>();
                cfg.CreateMap<AdvertDto, Advert>();

                cfg.CreateMap<RegisterUserDto, ApplicationUser>()
                    .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                    .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Email))
                    .ForMember(d => d.PhoneNumber, opt => opt.Ignore())
                    .ForMember(d => d.AccessFailedCount, opt => opt.Ignore())
                    .ForMember(d => d.EmailConfirmed, opt => opt.Ignore())
                    .ForMember(d => d.LockoutEnabled, opt => opt.Ignore())
                    .ForMember(d => d.PasswordHash, opt => opt.Ignore())
                    .ForMember(d => d.SecurityStamp, opt => opt.Ignore())
                    .ForMember(d => d.Id, opt => opt.Ignore())
                    .ForMember(d => d.Adverts, opt => opt.Ignore());
            });
        }
    }
}
