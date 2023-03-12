using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Realta.Frontend.Pages.Purchasing
{
    public partial class StockDetail
    {
        [Parameter] public int Id { get; set; }

        public List<StocksDetailDummy> stocksDetailList { get; set; }

        protected override void OnInitialized()
        {
            stocksDetailList = new List<StocksDetailDummy>
            {
                new StocksDetailDummy
                {
                    StodId = 1,
                    StockName = "Bantal",
                    StodBarcodeNumber = "BC123123139912",
                    StodStatus = "1 ",
                    StodPoNumber = "PO-001",
                },
                new StocksDetailDummy
                {
                    StodId = 2,
                    StockName = "Bantal",
                    StodBarcodeNumber = "BC123123139913",
                    StodStatus = "1 ",
                    StodPoNumber = "PO-001",
                },
                new StocksDetailDummy
                {
                    StodId = 3,
                    StockName = "Bantal",
                    StodBarcodeNumber = "BC123123139914",
                    StodNotes = "Kamar 03",
                    StodStatus = "2 ",
                    StodPoNumber = "PO-001",
                    FaciRoomNumber = "ROOM 1"
                },
                new StocksDetailDummy
                {
                    StodId = 3,
                    StockName = "Bantal",
                    StodBarcodeNumber = "BC123123139915",
                    StodStatus = "3 ",
                    StodPoNumber = "PO-001",
                },
                new StocksDetailDummy
                {
                    StodId = 4,
                    StockName = "Bantal",
                    StodBarcodeNumber = "BC123123139916",
                    StodStatus = "4 ",
                    StodPoNumber = "PO-001",
                },
                new StocksDetailDummy
                {
                    StodId = 5,
                    StockName = "Bantal",
                    StodBarcodeNumber = "BC123123139917",
                    StodNotes = "Kamar 06",
                    StodStatus = "2 ",
                    StodPoNumber = "PO-001",
                    FaciRoomNumber = "ROOM 2"
                }
            };
        }

    }

    public class StocksDetailDummy
    {
        public int? StodId { get; set; }
        public string? StockName { get; set; }
        public string? StodBarcodeNumber { get; set; }
        public string? StodStatus { get; set; }
        public string? StodNotes { get; set; }
        public string? FaciRoomNumber { get; set; }
        public string? StodPoNumber { get; set; }
    }
}
