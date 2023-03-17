using System.Collections;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Booking;
using Microsoft.AspNetCore.Components;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;

namespace Realta.Frontend.Pages.Booking;

public partial class HotelListFacilities
{
    [Parameter]
    public int Id { get; set; }

    public HotelParameters HotelParameters { get; set; }

    public class RoomItem
    {
        public HotelsDto? Room { get; set; }
        public int Quantity { get; set; }

        public int NumberOfRoom
        {
            get { return this.Room.FaciMaxNumber; }
        }

        public double Subtotal
        {
            get { return (double)(Room.FaciPrice * Quantity); }
        }
    }

    public class Cart
    {
        // Singleton instance
        public static readonly Cart Instance = new Cart();
        // Private constructor to prevent direct instantiation
        private Cart() { }
        // List of cart items
        public List<RoomItem> items = new List<RoomItem>();

        // Flag to indicate if cart is full
        public bool IsCartFull { get; set; } = false;
        
        // Add item to cart
        public void AddItem(HotelsDto desc, int qty = 1)
        {
            // Check if cart is already full
            if (IsCartFull)
            {
                return;
            }
            
            // Check if item already exists in cart
            var existingItem = items.FirstOrDefault(item => item.Room.FaciName == desc.FaciName);

            if (existingItem != null)
            {
                // Item already exists, update the quantity
                existingItem.Quantity += qty;
            }
            else
            {
                // Item doesn't exist yet, add it to the cart
                items.Add(new RoomItem
                {
                    Room = desc,
                    Quantity = qty,
                });
            }
        }
        public bool IsAddedToCart(HotelsDto desc)
        {
            if (Cart.Instance.IsCartFull)
            {
                return true;
            }

            var item = Cart.Instance.items.FirstOrDefault(i => i.Room.FaciName == desc.FaciName);
            return item != null;
        }
        
        public void RemoveItem(string roomName)
        {
            var itemToRemove = items.FirstOrDefault(item => item.Room.FaciName == roomName);

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }
        
        public void UpdateQuantity(string roomName, int quantity)
        {
            var itemToUpdate = items.FirstOrDefault(item => item.Room.FaciName == roomName);

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
        public IEnumerable<RoomItem> getItems
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
            get { return (double)items.Sum(item => item.Room.FaciPrice * item.Quantity); }
        }
        public double TaxAmount
        {
            get { return TotalPriceExcludingTax * 0.1d; }
        }

        // Get total price of items in cart (including tax)
        public double TotalPrice
        {
            get { return TotalPriceExcludingTax  /*+ TaxAmount*/; }
        }
        
    }
    
}



