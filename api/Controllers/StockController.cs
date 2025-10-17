using Microsoft.AspNetCore.Mvc;
using api.Data;      
using api.Dtos.Mappers;
using api.Dtos.Stock;
using api.Models;


namespace api.Controllers{
	[ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public StocksController(ApplicationDbContext db) => _db = db;

        // POST: /api/stocks
        [HttpPost]
        [ProducesResponseType(typeof(StockDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StockDto>> Create([FromBody] CreateStockDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var model = dto.ToStockModel();          // DTO → Entity
            _db.Stocks.Add(model);                   // EF starts tracking (not saved yet)
            await _db.SaveChangesAsync();            // now it’s persisted (ID generated)

            // Returns 201 + Location header pointing to GET by id
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model.ToStockDto());
        }

        // GET: /api/stocks/{id}  (needed for CreatedAtAction)
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StockDto>> GetById(int id)
        {
            var stock = await _db.Stocks.FindAsync(id);
            return stock is null ? NotFound() : Ok(stock.ToStockDto());
        }
    }
}