using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.Repositories;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;

namespace Realta.Frontend.Pages.Purchasing;

public partial class Gallery
{
    private int empId = 10;
    [Inject] public IVendorProductHttpRepository RepoProduct { get; set; } 
    [Inject] public ICartHttpRepository RepoCart { get; set; } 
    public List<VendorProductDto> Products { get; set; } = new();
    public List<CartDto> Cart { get; set; } = new();
    public MetaData MetaData { get; set; } = new();
    private VenproParameters _param = new();
    protected async override void OnInitialized()
    {
        await GetProducts();
        await GetCart();
        Console.WriteLine(Products.Count);
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
    private async Task UpdateItemQuantity(CartDto product, int newQuantity)
    {
        if (newQuantity < 1)
        {
            newQuantity = 1;
        }

        if (Products.Any()) await RepoCart.Update(product);
        await GetCart();
    }

    private async Task RemoveItem(CartDto product)
    {
        await RepoCart.Delete(product);
        await GetCart();
    }

    private bool _isCartOpen = false;
}

public class CartItem
{
    public VendorProductDto? Product { get; set; }
    public int Quantity { get; set; }

    public decimal Subtotal
    {
        get { return Product.VeproPrice * Quantity; }
    }
}

// public class Cart
// {
//     // Singleton instance
//     public static readonly Cart Instance = new Cart();
//
//     // Private constructor to prevent direct instantiation
//     private Cart()
//     {
//     }
//
//     // List of cart items
//     private List<CartItem> items = new List<CartItem>();
//
//     // Add item to cart
//     public void AddItem(VendorProductDto product, int quantity = 1)
//     {
//         // Check if item already exists in cart
//         var existingItem = items.FirstOrDefault(item => item.Product.VeproId == product.VeproId);
//
//         if (existingItem != null)
//         {
//             // Item already exists, update the quantity
//             existingItem.Quantity += quantity;
//         }
//         else
//         {
//             // Item doesn't exist yet, add it to the cart
//             items.Add(new CartItem
//             {
//                 Product = product,
//                 Quantity = quantity
//             });
//         }
//     }
//
//     // Remove item from cart
//     public void RemoveItem(int veproId)
//     {
//         var itemToRemove = items.FirstOrDefault(item => item.Product.VeproId == veproId);
//
//         if (itemToRemove != null)
//         {
//             items.Remove(itemToRemove);
//         }
//     }
//
//     // Update item quantity in cart
//     public void UpdateQuantity(int veproId, int quantity)
//     {
//         var itemToUpdate = items.FirstOrDefault(item => item.Product.VeproId == veproId);
//
//         if (itemToUpdate != null)
//         {
//             itemToUpdate.Quantity = quantity;
//         }
//     }
//
//     // Clear all items from cart
//     public void Clear()
//     {
//         items.Clear();
//     }
//
//     // Get all items in cart
//     public IEnumerable<CartItem> Items
//     {
//         get { return items; }
//     }
//
//     // Get total number of items in cart
//     public int TotalItems
//     {
//         get { return items.Sum(item => item.Quantity); }
//     }
//
//     // Get total price of items in cart (excluding tax)
//     public decimal TotalPriceExcludingTax
//     {
//         get { return items.Sum(item => item.Product.VeproPrice * item.Quantity); }
//     }
//
//     // Get tax amount (10% of total price)
//     public decimal TaxAmount
//     {
//         get { return TotalPriceExcludingTax * 0.1m; }
//     }
//
//     // Get total price of items in cart (including tax)
//     public decimal TotalPrice
//     {
//         get { return TotalPriceExcludingTax + TaxAmount; }
//     }
// }
