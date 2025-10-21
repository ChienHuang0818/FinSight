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
        public CommentsController(ICommentRepository Commentrepo){
			_repo = Commentrepo;
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
    }
}
