﻿using AutoMapper;
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
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<TicketEntity, TicketDM>()
            .ReverseMap();
            CreateMap<TicketDM, TicketDto>()
            .ReverseMap();
            CreateMap<TicketDM, TicketVM>()
            .ReverseMap();
        }
    }
}
