﻿using RealTimeTaskManagement.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDto>> GetAllTasks();
        Task<IEnumerable<TicketDto>> GetAllTasks(int taskId);
        Task CreateTask(TicketDto task);
    }
}
