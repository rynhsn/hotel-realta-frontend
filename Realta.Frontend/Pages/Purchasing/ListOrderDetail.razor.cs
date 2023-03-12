
namespace Realta.Frontend.Pages.Purchasing;

public class PurchaseOrderDetail
{

    public int PodeId { get; set; }
    public int PodePoheId { get; set; }
    public string? StockName { get; set; }
    public int Qty { get; set; }
    public decimal Price { get; set; }
    public int Received { get; set; }
    public int Rejected { get; set; }

    public decimal Total => Qty * Price;
}
