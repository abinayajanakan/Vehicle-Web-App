using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vehicle_Web_App.Dtos;
using Vehicle_Web_App.Models;

namespace Vehicle_Web_App.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Vehicle, VehicleDto>();
            Mapper.CreateMap<VehicleDto, Vehicle>().ForMember(c => c.Id, opt => opt.Ignore());
        }


    }
}