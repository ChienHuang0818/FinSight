using api.Data;
using api.Interfaces;
using api.Models;
using api.Dtos.Stock;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _db;
        public StockRepository(ApplicationDbContext db) => _db = db;

        public async Task<List<Stock>> GetAllAsync() 
            => await _db.Stocks
            .Include(s => s.Comments)
            .ToListAsync();

        public async Task<Stock?> GetByIdAsync(int id) 
            => await _db.Stocks
            .Include(s => s.Comments)          // IMPORTANT: Include
            .FirstOrDefaultAsync(s => s.Id == id); 

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _db.Stocks.AddAsync(stockModel);
            await _db.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto)
        {
            var stock = await _db.Stocks
                .Include(s => s.Comments)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null) return null;

            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Purchase = stockDto.Purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industry = stockDto.Industry;
            stock.MarketCap = stockDto.MarketCap;

            await _db.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _db.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null) return null;

            _db.Stocks.Remove(stock);
            await _db.SaveChangesAsync();
            return stock;
        }

        public Task<bool> ExistsAsync(int id) =>
            _db.Stocks.AnyAsync(s => s.Id == id);
    }
}
