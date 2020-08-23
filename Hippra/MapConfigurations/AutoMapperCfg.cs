using System;
using AutoMapper;
using Hippra.Models.POCO;

// reference: https://code-maze.com/automapper-net-core/

namespace Hippra.MapConfigurations
{
    public class AutoMapperCfg: AutoMapper.Profile
    {
        public AutoMapperCfg()
        {
            CreateMap<AutoMapTest, AutoMapTestVM>();
        }
    }
}
