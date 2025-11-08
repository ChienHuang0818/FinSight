using System.Linq;
using System.Threading;
using api.Data;
using api.Helpers;
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

        public async Task<List<Stock>> GetAllAsync(QueryObject query, CancellationToken ct)
        {
            var stocks = _db.Stocks.AsQueryable();   

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));

            if (!string.IsNullOrWhiteSpace(query.Symbol))
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));

            return await stocks.ToListAsync(ct);            
        }

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

        public async Task<bool> StockExistsAsync(int id)
            => await _db.Stocks.AnyAsync(s => s.Id == id);
    }
}
