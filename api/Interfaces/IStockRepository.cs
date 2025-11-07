using api.Models;
using api.Dtos.Stock;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
		Task<Stock?> GetByIdAsync(int id);
		Task<Stock> CreateAsync(Stock stockModel);
		Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);
		Task<Stock?> DeleteAsync(int id);
		Task<bool> ExistsAsync(int id);
		Task<bool> StockExistsAsync(int id);
    }
}
