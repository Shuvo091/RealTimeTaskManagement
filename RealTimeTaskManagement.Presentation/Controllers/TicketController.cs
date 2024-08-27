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

        public async Task<IActionResult> Index()
        {
            var tasks = _ticketService.GetAllTasks();
            return View(tasks);
        }

        // Action to display the create task form
        public IActionResult Create()
        {
            return View();
        }

        // Action to handle the form submission for creating a new task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketDto ticketDto)
        {
            if (ModelState.IsValid)
            {
                // Create the new task
                _ticketService.CreateTask(ticketDto);

                // Redirect to the index action after creating the task
                return RedirectToAction("Index");
            }

            // If model state is not valid, return to the Create view with validation errors
            return View(ticketDto);
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
