using AutoMapper;
using MovementService.Models;
using MovementService.Models.Dtos;

namespace MovementService.Automapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<MovementDto, Movement>().ReverseMap();
            });

            return mapperConfig;
        }
    }
}
