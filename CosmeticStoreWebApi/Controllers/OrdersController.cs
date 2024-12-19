using CosmeticStoreLibrary.Data;
using CosmeticStoreLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CosmeticStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("createOrder")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (request?.ProductIds == null || request.ProductIds.Count == 0)
            {
                return BadRequest("Список товаров не может быть пустым");
            }
            var products = _context.Products.Where(p => request.ProductIds.Contains(p.ProductArticleNumber)).ToList();
            if (products.Count != request.ProductIds.Count)
            {
                return BadRequest("Один или несколько товаров не найдены");
            }
            var order = new Order() { OrderDate = DateTime.Now, OrderDeliveryDate = DateTime.Now.AddDays(3), OrderPickupCode = new Random().Next(1000, 9999), OrderPickupPointId = 1, OrderStatus = "Создан" };
            _context.Orders.Add(order);
            _context.SaveChanges();
            foreach (var product in products)
            {
                var orderProduct = new OrderProduct() { OrderId = order.OrderId, ProductArticleNumber = product.ProductArticleNumber, ProductAmount = 1 };
                _context.OrderProducts.Add(orderProduct);
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("getOrders")]
        public IActionResult GetUsersOrders()
        {
            var orders = _context.Orders.Include(o => o.OrderProducts).ThenInclude(op => op.ProductArticleNumberNavigation).ToList();
            return Ok(orders);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
public class CreateOrderRequest
{
    public List<string> ProductIds { get; set; }
}


