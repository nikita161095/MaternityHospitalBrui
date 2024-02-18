using AutoMapper;
using MaternityHospitalBrui.DB.Entities;
using MaternityHospitalBrui.Enums;
using MaternityHospitalBrui.States;
using System;
using System.Linq;

namespace MaternityHospitalBrui.DB.Common.Mappings
{
    internal class ConfiguratorMapping
    {
        internal static MapperConfiguration GetMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                //maps
                CreateMapPatientToPatientState(cfg);
                CreateMapNameToNameState(cfg);
            });
            return config;
        }

        private static void CreateMapPatientToPatientState(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Patient, PatientState>()
              .ForMember(dest => dest.Gender, act => act.MapFrom(src => (Gender)src.Gender))
              .ReverseMap();
        }

        private static void CreateMapNameToNameState(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Name, NameState>()
              .ReverseMap();
        }


    }
}