using Microsoft.AspNetCore.Mvc;
using RealTimeTaskManagement.Services.Interfaces;
using RealTimeTaskManagement.Models.Dto;

namespace RealTimeTaskManagement.Presentation.Controllers
{
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
    }
}
