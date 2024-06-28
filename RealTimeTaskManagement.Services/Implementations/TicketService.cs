using AutoMapper;
using RealTimeTaskManagement.Data.Entities;
using RealTimeTaskManagement.Data.Repositories;
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
            var ticket = _taskRepository.GetAll();
            var ticketDto = _mapper.Map<IEnumerable<TicketDto>>(ticket);
            return ticketDto;
        }

        public void CreateTask(TicketDto ticketDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);
            _taskRepository.Add(ticket);
        }
    }
}
