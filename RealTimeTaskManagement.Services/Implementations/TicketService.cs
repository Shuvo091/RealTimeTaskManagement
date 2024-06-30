using AutoMapper;
using RealTimeTaskManagement.Data.Entities;
using RealTimeTaskManagement.Data.Repositories;
using RealTimeTaskManagement.Models.DomainModels;
using RealTimeTaskManagement.Models.Dto;
using RealTimeTaskManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _taskRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public IEnumerable<TicketDto> GetAllTasks()
        {
            var ticketEntity = _taskRepository.GetAll();
            var ticketDM = _mapper.Map<IEnumerable<TicketDM>>(ticketEntity);
            var ticketDto = _mapper.Map<IEnumerable<TicketDto>>(ticketDM);
            return ticketDto;
        }

        public void CreateTask(TicketDto ticketDto)
        {
            var ticketDM = _mapper.Map<IEnumerable<TicketDM>>(ticketDto);
            var ticketEntity = _mapper.Map<TicketEntity>(ticketDM);
            _taskRepository.Add(ticketEntity);
        }
    }
}
