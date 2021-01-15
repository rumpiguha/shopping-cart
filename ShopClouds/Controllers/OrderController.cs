using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.API.Interfaces;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> SubmitOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Controller: {nameof(OrderController)}, Action: {nameof(SubmitOrder)}, Message: Invalid Request");

                return BadRequest(ModelState);
            }

            return new OkObjectResult(await _orderService.SubmitOrder(order));
        }
    }
}