

using AutoMapper;
using RealTimeTaskManagement.Data.Entities;
using RealTimeTaskManagement.Models.DomainModels;
using RealTimeTaskManagement.Models.Dto;
using RealTimeTaskManagement.Models.ViewModels;

namespace RealTimeTaskManagement.Services.Mapper
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<ChatEntity, ChatMessageViewModel>()
            .ReverseMap();
        }
    }
}
