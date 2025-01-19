using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using RealTimeTaskManagement.Data.Entities;
using RealTimeTaskManagement.Data.Repositories;
using RealTimeTaskManagement.Models.DomainModels;
using RealTimeTaskManagement.Models.Dto;
using RealTimeTaskManagement.Services.Interfaces;
using System.Text.Json;

namespace RealTimeTaskManagement.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public TicketService(ITicketRepository taskRepository, IMapper mapper, IDistributedCache cache)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<TicketDto>> GetAllTasks()
        {
            var ticketEntity = await _taskRepository.GetAll();
            var ticketDM = _mapper.Map<IEnumerable<TicketDM>>(ticketEntity);
            var ticketDto = _mapper.Map<IEnumerable<TicketDto>>(ticketDM);
            return ticketDto;
        }

        public async Task<IEnumerable<TicketDto>> GetAllTasks(int taskId)
        {
            string cacheKey = $"task_{taskId}";
            string? cachedTask = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedTask))
            {
                IEnumerable<TicketDto> tickets = JsonSerializer.Deserialize<IEnumerable<TicketDto>>(cachedTask);
                return tickets!;
            }
            var ticketEntity = _taskRepository.GetAll();
            var ticketDM = _mapper.Map<IEnumerable<TicketDM>>(ticketEntity);
            var ticketDto = _mapper.Map<IEnumerable<TicketDto>>(ticketDM);
            string taskJson = JsonSerializer.Serialize(ticketDto);
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
            await _cache.SetStringAsync(cacheKey, taskJson, cacheOptions);
            return ticketDto;
        }

        public async Task CreateTask(TicketDto ticketDto)
        {
            var ticketDM = _mapper.Map<TicketDM>(ticketDto);
            var ticketEntity = _mapper.Map<TicketEntity>(ticketDM);
            _taskRepository.Add(ticketEntity);
        }
    }
}
