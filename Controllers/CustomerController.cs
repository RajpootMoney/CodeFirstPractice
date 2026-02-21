using AutoMapper;
using CodeFirstPractice.Data;
using CodeFirstPractice.DTOs.Request;
using CodeFirstPractice.DTOs.Response;
using CodeFirstPractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers.Include(c => c.Profile).ToListAsync();

            var result = _mapper.Map<List<CustomerResponse>>(customers);

            return Ok(result);
        }

        // GET: api/customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _context
                .Customers.Include(c => c.Profile)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(_mapper.Map<CustomerResponse>(customer));
        }

        // POST: api/customers
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            var customer = _mapper.Map<Customer>(request);

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<CustomerResponse>(customer);

            return CreatedAtAction(nameof(Get), new { id = customer.Id }, response);
        }

        // PUT: api/customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateCustomerRequest request)
        {
            var customer = await _context
                .Customers.Include(c => c.Profile)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return NotFound();

            _mapper.Map(request, customer);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
