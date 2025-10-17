
using api.Dtos.Stock;


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

        public static void UpdateFromDto(this Models.Stock model, UpdateStockDto dto)
        {
            model.Symbol = dto.Symbol;
            model.CompanyName = dto.CompanyName;
            model.Purchase = dto.Purchase;
            model.LastDiv = dto.LastDiv;
            model.Industry = dto.Industry;
            model.MarketCap = dto.MarketCap;
        }
    }
}
