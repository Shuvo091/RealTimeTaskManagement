using RealTimeTaskManagement.Models.Dto;
using RealTimeTaskManagement.Models.Enums;
using RealTimeTaskManagement.Services.Interfaces;

public class FakeTicketService : ITicketService
{
    private readonly List<TicketDto> _tickets = new()
        {
            new TicketDto
            {
                Id = 1,
                Priority = Priority.High,
                Assignee = Guid.NewGuid(),
                Reporter = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.UtcNow.AddDays(-2),
                Deadline = DateTimeOffset.UtcNow.AddDays(5),
                LoggedMinutes = TimeSpan.FromHours(2),
                ParentTicketId = null
            },
            new TicketDto
            {
                Id = 2,
                Priority = Priority.Low,
                Assignee = Guid.NewGuid(),
                Reporter = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.UtcNow.AddDays(-1),
                Deadline = DateTimeOffset.UtcNow.AddDays(10),
                LoggedMinutes = TimeSpan.FromMinutes(90),
                ParentTicketId = 1
            }
        };

    public Task<IEnumerable<TicketDto>> GetAllTasks()
    {
        return Task.FromResult(_tickets.AsEnumerable());
    }

    public Task CreateTask(TicketDto ticketDto)
    {
        _tickets.Add(ticketDto); // Simulate saving to in-memory list
        return Task.CompletedTask;
    }

    public Task<IEnumerable<TicketDto>> GetAllTasks(int taskId)
    {
        var ticket = _tickets.Where(t => t.Id == taskId);
        return Task.FromResult(ticket);
    }
}
