using System.ComponentModel;

namespace Realta.Frontend.Pages.Purchasing;

public class PurchaseOrderHeader
{
    [DisplayName("PO Number")]
    public string PoNumber { get; set; }

    [DisplayName("PO Date")]
    public DateTime PoDate { get; set; }

    [DisplayName("Vendor Target")]
    public string VendorTarget { get; set; }

    [DisplayName("Line Items")]
    public int LineItems { get; set; }

    [DisplayName("Total Amount")]
    public decimal TotalAmount { get; set; }

    [DisplayName("Status")]
    public string Status { get; set; }

    public int PoheId { get; set; }
}
