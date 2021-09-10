using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider provider;

        public OrdersController(IOrdersProvider provider)
        {
            this.provider = provider;
        }

       [HttpGet("{customerId}")]
       public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await provider.GetOrdersAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}
