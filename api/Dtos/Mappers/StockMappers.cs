
using api.Dtos.Stock;
using api.Models;

namespace api.Dtos.Mappers
{
    public static class StockMappers
    {
         public static Models.Stock ToStockModel(this CreateStockDto dto) => new Models.Stock
        {
            Symbol = dto.Symbol,
            CompanyName = dto.CompanyName,
            Purchase = dto.Purchase,
            LastDiv = dto.LastDiv,
            Industry = dto.Industry,
            MarketCap = dto.MarketCap
        };

        public static StockDto ToStockDto(this Models.Stock model) => new StockDto
        {
            Id = model.Id,
            Symbol = model.Symbol,
            CompanyName = model.CompanyName,
            Purchase = model.Purchase,
            LastDiv = model.LastDiv,
            Industry = model.Industry,
            MarketCap = model.MarketCap
        };
    }
}
