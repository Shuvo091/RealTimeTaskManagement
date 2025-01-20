using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using RealTimeTaskManagement.Models.Dto;
using RealTimeTaskManagement.Models.Enums;
using RealTimeTaskManagement.Presentation.Controllers;
using RealTimeTaskManagement.Services.Interfaces;
using StackExchange.Redis;

namespace RealTimeTaskManagement.Tests.Controllers
{
    public class TicketControllerTest
    {
        private readonly StubTicketService _stubService;

        public TicketControllerTest()
        {
            // Initialize the StubTicketService and inject it into the controller
            _stubService = new StubTicketService();
        }


        [Fact]
        public async Task GetAllTasks_ReturnsStubData()
        {
            // Arrange
            var stubService = new StubTicketService();

            // Act
            var result = await stubService.GetAllTasks();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.First().Id);
            Assert.Equal(Priority.High, result.First().Priority);
        }

        [Fact]
        public async Task CreateTask_AddsToFakeService()
        {
            // Arrange
            var fakeService = new FakeTicketService();
            var newTicket = new TicketDto
            {
                Id = 3,
                Priority = Priority.High,
                Assignee = Guid.NewGuid(),
                Reporter = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.UtcNow.AddDays(-2),
                Deadline = DateTimeOffset.UtcNow.AddDays(5),
                LoggedMinutes = TimeSpan.FromHours(2),
                ParentTicketId = null
            };

            // Act
            await fakeService.CreateTask(newTicket);
            var allTasks = await fakeService.GetAllTasks();

            // Assert
            allTasks.Should().HaveCount(3);
            allTasks.Last().Id.Should().Be(3);
            allTasks.Last().Priority.Should().Be(Priority.High);
        }

        [Fact]
        public async Task Index_UsesCachedData_WhenAvailable()
        {
            // Arrange
            var mockTicketService = new Mock<ITicketService>();
            var assigneeGuid = Guid.NewGuid();
            var ReporterGuid = Guid.NewGuid();
            var createdOn = DateTimeOffset.UtcNow;
            var deadline = DateTimeOffset.UtcNow.AddDays(7);
            var loggedMinutes = TimeSpan.FromHours(2);
            mockTicketService.Setup(s => s.GetAllTasks())
                .ReturnsAsync(new List<TicketDto>
                {
                    new TicketDto
                    {
                        Id = 3,
                        Priority = Priority.High,
                        Assignee = assigneeGuid,
                        Reporter = ReporterGuid,
                        CreatedOn = createdOn,
                        Deadline = deadline,
                        LoggedMinutes = loggedMinutes,
                        ParentTicketId = null
                    }
                });

            var redisMock = new Mock<IDatabase>();
            redisMock.Setup(r => r.StringGetAsync("getTickets", It.IsAny<CommandFlags>()))
                .ReturnsAsync("[{\"Id\":1,\"Priority\":3}]");

            var muxerMock = new Mock<IConnectionMultiplexer>();
            muxerMock.Setup(m => m.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(redisMock.Object);

            // Mock HttpMessageHandler to return a custom HttpResponseMessage
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("{\"Message\":\"Success\"}")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            // Create controller with mocked dependencies
            var controller = new TicketController(mockTicketService.Object, httpClient, muxerMock.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TicketDto>>(viewResult.Model);

            model.Should().HaveCount(1);
            model.First().Priority.Should().Be(Priority.High);

            redisMock.Verify(r => r.StringGetAsync("getTickets", It.IsAny<CommandFlags>()), Times.Once);
            mockTicketService.Verify(s => s.GetAllTasks(), Times.Never);
            mockHttpMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Never(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }

    }
}
