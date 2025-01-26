using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeTaskManagement.Data.Context;
using RealTimeTaskManagement.Models.ViewModels;

namespace RealTimeTaskManagement.Presentation.Controllers
{
    public class ChatController : Controller
    {
        private readonly RealTimeTaskManagementDbContext _context;
        private readonly IMapper _mapper;

        public ChatController(RealTimeTaskManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var messages = _context.Chat
                .ProjectTo<ChatMessageViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(m => m.Timestamp)
                .ToList();
            var viewModel = new ChatViewModel
            {
                Messages = messages,
            };

            return View(viewModel);
        }
    }
}
