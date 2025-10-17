using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class UpdateStockDto
    {
        [Required, MaxLength(10)]
        public string Symbol { get; set; } = string.Empty;

        [Required, MaxLength(120)]
        public string CompanyName { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Purchase { get; set; }

        [Range(0, double.MaxValue)]
        public decimal LastDiv { get; set; }

        [Required, MaxLength(80)]
        public string Industry { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal MarketCap { get; set; }
    }
}
