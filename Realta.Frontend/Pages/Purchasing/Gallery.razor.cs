using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Purchasing;

public partial class Gallery
{
    private int empId = 10;
    [Inject] public IPurchaseOrderHttpRepository RepoPurchaseOrder { get; set; } 
    [Inject] public IVendorProductHttpRepository RepoProduct { get; set; } 
    [Inject] public ICartHttpRepository RepoCart { get; set; }
    public List<VendorProductDto> Products { get; set; } = new();
    public List<CartDto> Cart { get; set; } = new();
    public MetaData MetaData { get; set; } = new();
    private VenproParameters _param = new();
    private SuccessNotification _notif;
    protected async override void OnInitialized()
    {
        await GetProducts();
        await GetCart();
    }
    
    private async Task GetProducts()
    {
        var response = await RepoProduct.GetAll(_param);
        Products = response.Items;
        MetaData = response.MetaData;
        StateHasChanged();
    }

    private async Task GetCart()
    {
        Cart = await RepoCart.Get(empId);
        StateHasChanged();
    }

    private async Task OnRequestOrder()
    {
        var data = Cart.Select(i=> new PurchaseOrderTransfer
        {
            PoCartId = i.CartId,
            PoPayType = "TR"
        }).ToList();
        _isCartOpen = false;
        await RepoPurchaseOrder.Create(data);
        _notif.Show(NavigationManager.Uri, "Request has been created.");
        await GetCart();
    }
    private async Task AddToCart(int id)
    {
        var item = new CartDto()
        {
            CartEmpId = empId,
            CartOrderQty = 1,
            CartVeproId = id
        };
        await RepoCart.Create(item);
        await GetCart();
        _isCartOpen = true;
    }
    private async Task UpdateItemQuantity(CartDto product, int newQty)
    {
        if (newQty < 1)
        {
            RemoveItem(product);
        }
        else
        {
            product.CartOrderQty = (short)newQty;
            await RepoCart.Update(product);    
        }
        await GetCart();
    }

    private async Task RemoveItem(CartDto product)
    {
        await RepoCart.Delete(product);
        await GetCart();
    }
    
    private bool _isCartOpen = false;
    public int TotalItems => Cart.Sum(item => item.CartOrderQty);
    public decimal SubTotal => Cart.Sum(item => item.Subtotal);
    public decimal TaxAmount => SubTotal * 0.1m;
    public decimal TotalPrice => SubTotal + TaxAmount;
    
    private async Task SelectedPage(int page)
    {
        _param.PageNumber = page;
        await GetProducts();
    }
    private async Task SearchChanged(string keyword)
    {
        _param.PageNumber = 1;
        _param.Keyword = keyword;
        await GetProducts();
    }
    private async Task PageSizeChanged(int page)
    {
        _param.PageSize = page;
        await GetProducts();
    }
}