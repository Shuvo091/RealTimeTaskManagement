using RealTimeTaskManagement.Models.Dto;
using RealTimeTaskManagement.Models.Enums;
using RealTimeTaskManagement.Services.Interfaces;

public class StubTicketService : ITicketService
{
    public Task<IEnumerable<TicketDto>> GetAllTasks()
    {
        return Task.FromResult(new List<TicketDto>
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
        }.AsEnumerable());
    }

    public Task CreateTask(TicketDto ticketDto)
    {
        // Simulate task creation, such as logging or storing in a temporary collection
        Console.WriteLine($"Task created with Title: {ticketDto.Id}");
        return Task.CompletedTask;
    }

    public Task<IEnumerable<TicketDto>> GetAllTasks(int taskId)
    {
        // Return a single task by taskId
        return Task.FromResult(new List<TicketDto>
        {
            new TicketDto
            {
                Id = taskId,
                Priority = Priority.Medium,
                Assignee = Guid.NewGuid(),
                Reporter = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.UtcNow,
                Deadline = DateTimeOffset.UtcNow.AddDays(7),
                LoggedMinutes = TimeSpan.FromHours(3),
                ParentTicketId = null
            }
        }.AsEnumerable());
    }
}
