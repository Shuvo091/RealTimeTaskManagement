using AutoMapper;
using RealTimeTaskManagement.Data.Entities;
using RealTimeTaskManagement.Models.Dto;
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
            CreateMap<Ticket, TicketDto>()
            .ReverseMap();
        }
    }
}
