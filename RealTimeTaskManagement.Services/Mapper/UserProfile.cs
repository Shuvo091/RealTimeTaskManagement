using AutoMapper;
using RealTimeTaskManagement.Data.Entities;
using RealTimeTaskManagement.Models.DomainModels;
using RealTimeTaskManagement.Models.Dto;
using RealTimeTaskManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Services.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserDM>()
            .ReverseMap();
            CreateMap<UserDM, UserDto>()
            .ReverseMap();
            CreateMap<UserDM, UserVM>()
            .ReverseMap();
        }
    }
}
