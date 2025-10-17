using api.Data;
using api.Interfaces;
using api.Models;
using api.Dtos.Comment;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .ToListAsync();
        }
    }
}
