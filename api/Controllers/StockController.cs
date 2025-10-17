using Microsoft.AspNetCore.Mvc;
using api.Dtos.Stock;
using api.Dtos.Mappers;       
using api.Interfaces;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository _repo;
        public StocksController(IStockRepository repo) => _repo = repo;

        // GET: /api/stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetAll(CancellationToken ct)
        {
            var items = await _repo.GetAllAsync();
            return Ok(items.Select(s => s.ToStockDto()));
        }

        // GET: /api/stocks/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StockDto>> GetById(int id, CancellationToken ct)
        {
            var stock = await _repo.GetByIdAsync(id);
            return stock is null ? NotFound() : Ok(stock.ToStockDto());
        }

        // POST: /api/stocks
        [HttpPost]
        [ProducesResponseType(typeof(StockDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<StockDto>> Create([FromBody] CreateStockDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var created = await _repo.CreateAsync(dto.ToStockModel());
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToStockDto());
        }

        // PUT: /api/stocks/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(StockDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockDto>> Update(int id, [FromBody] UpdateStockDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            if (!await _repo.ExistsAsync(id)) return NotFound();

            var updatedStock = await _repo.UpdateAsync(id, dto);
            return updatedStock is null ? NotFound() : Ok(updatedStock.ToStockDto());
        }

        // DELETE: /api/stocks/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var deletedStock = await _repo.DeleteAsync(id);
            return deletedStock is null ? NotFound() : NoContent();
        }
    }
}
