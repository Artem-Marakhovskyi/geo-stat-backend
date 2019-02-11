using AutoMapper;
using GeoStat.DTO;
using GeoStat.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoStat.DI
{
    static class MapperConfig
    {
        public static void InitializeMapper()
        {
            Mapper.Initialize(c => CreateMaps(c));
        }

        private static void CreateMaps(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<LocationDto, Location>().ReverseMap();
            expression.CreateMap<GeoStatUserDto, GeoStatUser>().ReverseMap();
            expression.CreateMap<GroupDto, Group>().ReverseMap();
            expression.CreateMap<GroupUserDto, GroupUser>().ReverseMap();
        }
    }
}
