using CRUD.on.Order.Product.DTO;
using CRUD.on.Order.Product.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDonOrderProduct.Controllers
{
    [Route("api/tblOrder")]
    [ApiController]
    public class tblOrderController : ControllerBase
    {
        private readonly IOrderRepository _repo;
        public tblOrderController(IOrderRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var res = await _repo.GetOrders();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{oId}", Name = "OrderSelectById")]
        public async Task<IActionResult> GetOrder(int oId)
        {
            try
            {
                var res = await _repo.GetOrder(oId);
                if (res == null)
                    return NotFound();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto ord)
        {
            try
            {
                var res = await _repo.CreateOrder(ord);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{oId}")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto ord)
        {
            try
            {
                var res = await _repo.UpdateOrder(ord);
                if (res == 0)
                    return NotFound();
                else
                    return Ok("Data Updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

