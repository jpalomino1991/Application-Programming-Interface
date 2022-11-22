using AutoMapper;
using ClientService.Models;
using ClientService.Models.Dtos;

namespace ClientService.Automapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ClientDto, Client>();
                config.CreateMap<Client, ClientDto>()
                    .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Person.PersonName))
                    .ForMember(dest => dest.PersonDirection, opt => opt.MapFrom(src => src.Person.PersonDirection))
                    .ForMember(dest => dest.PersonPhone, opt => opt.MapFrom(src => src.Person.PersonPhone));
                config.CreateMap<ClientDto, Person>();
            });

            return mapperConfig;
        }
    }
}
