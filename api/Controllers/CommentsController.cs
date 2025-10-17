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
    }
}
