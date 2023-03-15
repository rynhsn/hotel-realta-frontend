using System.ComponentModel;
using System.Runtime.Serialization;

namespace Realta.Frontend.Pages.Purchasing
{
    public partial class Stocks
    {
        public List<StocksDummy>? stocksList { get; set; }

        protected override void OnInitialized()
        {
            stocksList = new List<StocksDummy>
            {
                new StocksDummy
                {
                    StockId = 1,
                    StockName = "Handuk",
                    StockDescription = "Handuk Hangat 1",
                    StockQuantity = 0,
                    StockReorderPoint = 3,
                    StockUsed = 0,
                    StockScrap = 0,
                    StockSize = "A1",
                    StockColor = "Color 1",
                    StockModifiedDate = DateTime.Now
                },
                new StocksDummy
                {
                    StockId = 2,
                    StockName = "Seprai",
                    StockDescription = "Seprai Nyaman 2",
                    StockQuantity = 1,
                    StockReorderPoint = 3,
                    StockUsed = 1,
                    StockScrap = 0,
                    StockSize = "A2",
                    StockColor = "Color 2",
                    StockModifiedDate = DateTime.Now
                },
                new StocksDummy
                {
                    StockId = 3,
                    StockName = "Bantal",
                    StockDescription = "Bantal Aman 3",
                    StockQuantity = 2,
                    StockReorderPoint = 3,
                    StockUsed = 0,
                    StockScrap = 2,
                    StockSize = "A3",
                    StockColor = "Color 3",
                    StockModifiedDate = DateTime.Now
                },
                new StocksDummy
                {
                    StockId = 4,
                    StockName = "Guling",
                    StockDescription = "Guling Nyaman 4",
                    StockQuantity = 0,
                    StockReorderPoint = 3,
                    StockUsed = 0,
                    StockScrap = 0,
                    StockSize = "A4",
                    StockColor = "Color 4",
                    StockModifiedDate = DateTime.Now
                },
                new StocksDummy
                {
                    StockId = 5,
                    StockName = "Sabun",
                    StockDescription = "Sabun Wangy 5",
                    StockQuantity = 0,
                    StockReorderPoint = 3,
                    StockUsed = 0,
                    StockScrap = 0,
                    StockSize = "A5",
                    StockColor = "Color 5",
                    StockModifiedDate = DateTime.Now
                },
                new StocksDummy
                {
                    StockId = 6,
                    StockName = "Pasta Gigi",
                    StockDescription = "Tersedias 6",
                    StockQuantity = 0,
                    StockReorderPoint = 3,
                    StockUsed = 0,
                    StockScrap = 0,
                    StockSize = "A6",
                    StockColor = "Color 6",
                    StockModifiedDate = DateTime.Now
                },
            };
        }
    }

    public class StocksDummy
    {
        [DisplayName("Stock Id")]
        public int? StockId { get; set; }

        [DisplayName("Stock Name")]
        public string StockName { get; set; }

        [DisplayName("Stock Description")]
        public string? StockDescription { get; set; }

        [DisplayName("Stock Quantity")]
        public short StockQuantity { get; set; }

        [DisplayName("Stock Re-Order Point")]
        public short StockReorderPoint { get; set; }

        [DisplayName("Stock Used")]
        public short? StockUsed { get; set; }

        [DisplayName("Stock Scrap")]
        public short? StockScrap { get; set; }

        [DisplayName("Stock Size")]
        public string? StockSize { get; set; }

        [DisplayName("Stock Color")]
        public string? StockColor { get; set; }
        public DateTime? StockModifiedDate { get; set; }

    }
}
