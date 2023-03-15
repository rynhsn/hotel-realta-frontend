using System.Collections;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Booking;
using Microsoft.AspNetCore.Components;
using Realta.Domain.RequestFeatures;

namespace Realta.Frontend.Pages.Booking;

public class SpofItem
{
    public List<SpecialOffersDto> SpecialOffersList { get; set; } = new List<SpecialOffersDto>();
    [Inject] 
    public ISpecialOfferHttpRepository SpecialOfferHttpRepository { get; set; }
    protected async  Task OnInitializedAsync()
    {
        SpecialOffersList= await SpecialOfferHttpRepository.GetSpecialOffers();
    }
    
}


public class CartItem
{
    public RoomDesc? Room { get; set; }
    public int Quantity { get; set; }

    public double Subtotal
    {
        get { return (Room.RoomPrice * Quantity); }
    }
}

public class Cart
{
    // Singleton instance
    public static readonly Cart Instance = new Cart();
    // Private constructor to prevent direct instantiation
    private Cart() { }
    // List of cart items
    private List<CartItem> items = new List<CartItem>();
    
    // Add item to cart
    public void AddItem(RoomDesc desc, int qty = 1)
    {
        // Check if item already exists in cart
        var existingItem = items.FirstOrDefault(item => item.Room.RoomName == desc.RoomName);

        if (existingItem != null)
        {
            // Item already exists, update the quantity
            existingItem.Quantity += qty;
        }
        else
        {
            // Item doesn't exist yet, add it to the cart
            items.Add(new CartItem
            {
                Room = desc,
                Quantity = qty,
            });
        }
    }
    
    public void RemoveItem(string roomName)
    {
        var itemToRemove = items.FirstOrDefault(item => item.Room.RoomName == roomName);

        if (itemToRemove != null)
        {
            items.Remove(itemToRemove);
        }
    }
    
    public void UpdateQuantity(string roomName, int quantity)
    {
        var itemToUpdate = items.FirstOrDefault(item => item.Room.RoomName == roomName);

        if (itemToUpdate != null)
        {
            itemToUpdate.Quantity = quantity;
        }
    }

    // Clear all items from cart
    public void Clear()
    {
        items.Clear();
    }
    
    //Get All items from cart
    public IEnumerable<CartItem> getItems
    {
        get { return items; }
    }

    public int TotalItems
    {
        get { return items.Sum(item => item.Quantity); }
    }

    // Get total price of items in cart (excluding tax)
    public double TotalPriceExcludingTax
    {
        get { return items.Sum(item => item.Room.RoomPrice * item.Quantity); }
    }
    public double TaxAmount
    {
        get { return TotalPriceExcludingTax * 0.1d; }
    }

    // Get total price of items in cart (including tax)
    public double TotalPrice
    {
        get { return TotalPriceExcludingTax + TaxAmount; }
    }
    
}
public class RoomDesc
{
    public string? RoomName { get; set; }
    public double RoomPrice { get; set; }
    public double RoomMaxPrice { get; set; }
    public int RoomCapacity { get; set; }
    public string? RoomPicLink { get; set; }

}

public class HotelDesc
{

    public string? HotelName { get; set; }
    public string? HotelLocation { get; set; }
    public double RatingStar { get; set; }
    public string? MemberStatus { get; set; }
    public string? Description { get; set; }
    public string? RatingDescription { get; set; }
    public int TotalRating { get; set; }
}



