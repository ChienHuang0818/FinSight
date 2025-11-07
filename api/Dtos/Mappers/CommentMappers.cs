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

        public static CommentDto ToDto(this CommentModel c) => new CommentDto
        {
            Id = c.Id,
            Title = c.Title,
            Content = c.Content,
            CreatedOn = c.CreatedOn,
            StockId = c.StockId
        };

        public static CommentModel ToCommentFromCreate(this CreateCommentRequestDto dto, int stockId)
            => new CommentModel
            {
                Title = dto.Title,
                Content = dto.Content,
                StockId = stockId,
                CreatedOn = DateTime.UtcNow
            };
        }

}
