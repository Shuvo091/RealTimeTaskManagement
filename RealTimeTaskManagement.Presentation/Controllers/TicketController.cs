using Microsoft.AspNetCore.Mvc;
using RealTimeTaskManagement.Services.Interfaces;
using RealTimeTaskManagement.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using RealTimeTaskManagement.Models.Enums;
using RealTimeTaskManagement.Common.Extensions;
using RealTimeTaskManagement.Common.Utilities;
using StackExchange.Redis;
using System.Net.Http.Headers;
using Role = RealTimeTaskManagement.Common.Utilities.Role;
using System.Diagnostics;
using System.Text.Json;

namespace RealTimeTaskManagement.Presentation.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly HttpClient _client;
        private readonly IDatabase _redis;

        public TicketController(ITicketService ticketService, HttpClient client, IConnectionMultiplexer muxer)
        {
            _ticketService = ticketService;
            _client = client;
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("RealTimeTaskManagement", "1.0"));
            _redis = muxer.GetDatabase();
        }

        public async Task<IActionResult> Index()
        {
            string json;
            var watch = Stopwatch.StartNew();
            var keyName = $"getTickets";
            json = await _redis.StringGetAsync(keyName);
            if (string.IsNullOrEmpty(json))
            {
                json = await GetTasksStr();
                var setTask = _redis.StringSetAsync(keyName, json);
                var expireTask = _redis.KeyExpireAsync(keyName, TimeSpan.FromSeconds(3600));
                await Task.WhenAll(setTask, expireTask);
            }

            var tasks =
                JsonSerializer.Deserialize<IEnumerable<TicketDto>>(json);
            watch.Stop();
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

        private async Task<string> GetTasksStr()
        {
            var tasks = await _ticketService.GetAllTasks();
            return JsonSerializer.Serialize(tasks);
        }
    }
}
