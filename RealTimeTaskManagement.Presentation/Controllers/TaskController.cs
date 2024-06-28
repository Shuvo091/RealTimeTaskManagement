//using Microsoft.AspNetCore.Mvc;
//using RealTimeTaskManagement.Services;
//using RealTimeTaskManagement.Models.DTOs;

//namespace RealTimeTaskManagement.Presentation.Controllers
//{
//    public class TaskController : Controller
//    {
//        private readonly ITaskService _taskService;

//        public TaskController(ITaskService taskService)
//        {
//            _taskService = taskService;
//        }

//        public IActionResult Index()
//        {
//            var tasks = _taskService.GetAllTasks();
//            return View(tasks);
//        }

//        [HttpPost]
//        public IActionResult Create(TaskDto task)
//        {
//            _taskService.CreateTask(task);
//            return RedirectToAction("Index");
//        }
//    }
//}
