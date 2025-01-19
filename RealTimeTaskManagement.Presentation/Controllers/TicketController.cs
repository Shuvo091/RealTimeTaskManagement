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

        // Using a Lazy object to defer the initialization of HttpClient's UserAgent headers
        private readonly Lazy<HttpClient> _lazyClient;

        public TicketController(ITicketService ticketService, HttpClient client, IConnectionMultiplexer muxer)
        {
            _ticketService = ticketService;
            _client = client;
            _redis = muxer.GetDatabase();

            _lazyClient = new Lazy<HttpClient>(() =>
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("RealTimeTaskManagement", "1.0"));
                return client;
            });
        }

        public async Task<IActionResult> Index()
        {
            var watch = Stopwatch.StartNew();
            const string keyName = "getTickets";

            // Use caching to avoid unnecessary redis calls
            var json = await _redis.StringGetAsync(keyName);
            if (string.IsNullOrEmpty(json))
            {
                json = await GetTasksStr();
                await Task.WhenAll(
                    _redis.StringSetAsync(keyName, json),
                    _redis.KeyExpireAsync(keyName, TimeSpan.FromHours(1))
                );
            }

            // Use lazy deserialization to optimize performance
            var tasks = JsonSerializer.Deserialize<IEnumerable<TicketDto>>(json);
            watch.Stop();

            // Logging performance for debugging purposes
            Debug.WriteLine($"Index action executed in {watch.ElapsedMilliseconds} ms.");

            return View(tasks);
        }

        public IActionResult Create()
        {
            // Ensure minimal resource usage for basic actions
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketDto ticketDto)
        {
            if (ModelState.IsValid)
            {
                // Using try-catch to avoid unhandled exceptions
                try
                {
                    await _ticketService.CreateTask(ticketDto);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error creating task: {ex.Message}");
                }
            }

            // Return the view with the same model to display validation errors
            return View(ticketDto);
        }

        [Authorize(Roles = $"{Role.Admin},{Role.Manager}")]
        public IActionResult AdminAndManagerOnly()
        {
            // Use explicit method calls to reduce ambiguity
            return View();
        }

        [Authorize(Roles = Role.User)]
        public IActionResult UserOnly()
        {
            // Reduce unnecessary complexity in simple actions
            return View();
        }

        private async Task<string> GetTasksStr()
        {
            // Fetch and serialize data efficiently
            var tasks = await _ticketService.GetAllTasks();
            return JsonSerializer.Serialize(tasks);
        }

        protected override void Dispose(bool disposing)
        {
            // Explicitly release resources to avoid memory leaks
            if (disposing)
            {
                _lazyClient?.Value?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
