using Microsoft.AspNetCore.Mvc;
using RealTimeTaskManagement.Services.Interfaces;
using RealTimeTaskManagement.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using RealTimeTaskManagement.Models.Enums;
using RealTimeTaskManagement.Common.Extensions;
using RealTimeTaskManagement.Common.Utilities;

namespace RealTimeTaskManagement.Presentation.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public IActionResult Index()
        {
            var tasks = _ticketService.GetAllTasks();
            return View(tasks);
        }

        [HttpPost]
        public IActionResult Create(TicketDto task)
        {
            _ticketService.CreateTask(task);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = $"{Role.Admin},{Role.Manager}")]
        public IActionResult AdminAndManagerOnly()
        {
            return View();
        }

        [Authorize(Roles = Role.User)]
        public IActionResult UserOnly()
        {
            return View();
        }
    }
}
