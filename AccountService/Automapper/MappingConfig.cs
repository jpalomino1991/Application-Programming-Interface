using AccountService.Models;
using AccountService.Models.Dtos;
using AutoMapper;

namespace AccountService.Automapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AccountDto, Account>().ReverseMap();
            });

            return mapperConfig;
        }
    }
}
