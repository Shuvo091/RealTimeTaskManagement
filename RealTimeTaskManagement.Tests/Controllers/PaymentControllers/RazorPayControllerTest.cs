using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealTimeTaskManagement.Payment.RazorPay;
using RealTimeTaskManagement.Presentation.Controllers.PaymentControllers;
namespace RealTimeTaskManagement.Tests.Controllers
{
    public class RazorPayControllerTest
    {
        private readonly RazorPayController _controller;
        public RazorPayControllerTest()
        {
            _controller = new RazorPayController();
        }

        [Fact]
        public void RazorPayController_Index_ReturnsView()
        {
            // Arrange

            // Act
            var result = _controller.Index();

            //Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void RazorPayController_RazorPay_ReturnsJsonResult_WithOrder()
        {
            // Arrange
            var paymentAmount = "1000";

            // Act
            var result = _controller.RazorPay(paymentAmount) as JsonResult;

            // Assert
            result.Value.Should().NotBeNull();

            var response = JsonConvert.DeserializeObject<Response>(JsonConvert.SerializeObject(result.Value));
            response.Should().NotBeNull();
            response.Should().BeOfType<Response>();
        }
    }
}
