using api.Dtos.Comment;
using api.Models;
using CommentModel = api.Models.Comment;
namespace api.Dtos.Mappers
{
        public static class CommentMappers
    {
         public static CommentDto ToCommentDto(this CommentModel commentmodel) => new CommentDto
        {
            Id        = commentmodel.Id,
			Title     = commentmodel.Title,
			Content   = commentmodel.Content,
			CreatedOn = commentmodel.CreatedOn,
			StockId   = commentmodel.StockId
			
        };
    }

}
