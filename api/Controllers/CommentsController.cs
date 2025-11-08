using Microsoft.AspNetCore.Mvc;
using api.Dtos.Comment;
using api.Dtos.Mappers;
using api.Interfaces;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _repo;
        private readonly IStockRepository _stockRepo;
        
        public CommentsController(ICommentRepository commentRepo, IStockRepository stockRepo){
			_repo = commentRepo;
			_stockRepo = stockRepo;
		}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(items.Select(c => c.ToCommentDto()));
        }

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var comment = await _repo.GetByIdAsync(id);
			if (comment is null) return NotFound();
			return Ok(comment.ToCommentDto()); 
		}
        [HttpPost]
        public async Task<IActionResult> Create(int stockId, [FromBody] CreateCommentRequestDto dto)
        {
            // Validate parent
            if (!await _stockRepo.StockExistsAsync(stockId))
                return BadRequest(new { message = "Stock does not exist." });

            // Map & persist
            var entity = dto.ToCommentFromCreate(stockId);
            var created = await _repo.CreateAsync(entity);

            // Return 201 with Location to GET /api/comments/{id}
            return CreatedAtAction(
                actionName: nameof(CommentsController.GetById),
                controllerName: "Comments",
                routeValues: new { id = created.Id },
                value: created.ToCommentDto()
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentRequestDto dto)
        {
            var comment = await _repo.GetByIdAsync(id);
            if (comment is null) return NotFound();
            comment.UpdateFromDto(dto);
            await _repo.UpdateAsync(comment);
            return Ok(comment.ToCommentDto());
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _repo.GetByIdAsync(id);
            if (comment is null) return NotFound();
            await _repo.DeleteAsync(comment);
            return Ok(comment.ToCommentDto());
        }
    }
}
