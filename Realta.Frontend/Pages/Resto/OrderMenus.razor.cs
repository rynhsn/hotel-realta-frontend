using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Resto;
using Realta.Frontend.Components.Resto;
using Realta.Domain.Entities;
using Realta.Domain.Dto;

namespace Realta.Frontend.Pages.Resto
{
    public partial class OrderMenus
    {
        [Inject]
        public IRestoMenusHttpRepository RestoMenusRepo { get; set; } //
        public List<RestoMenusDto> RestoMenusList { get; set; } = new List<RestoMenusDto>(); //

        private RestoMenusParameters _restoMenusParameters = new RestoMenusParameters();
        public List<RestoMenusDto> RestoMenusListPaging { get; set; } = new List<RestoMenusDto>();

        public MetaData MetaData { get; set; } = new MetaData();

        protected async override Task OnInitializedAsync()
        {
            //RestoMenusList = await RestoMenusRepo.GetRestoMenus();
            await GetRestoPaging();
        }


        private async Task GetRestoPaging()
        {
            var pagingResponse = await RestoMenusRepo.GetPaging(_restoMenusParameters);
            RestoMenusListPaging = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
        private async Task SelectedPageOrder(int page)
        {
            _restoMenusParameters.PageNumber = page;
            await GetRestoPaging();
        }

        private async Task SearchChange(string searchTerm)
        {

            Console.WriteLine(searchTerm);

            _restoMenusParameters.PageNumber = 1;
            _restoMenusParameters.SearchTerm = searchTerm;

            await GetRestoPaging();

        }

        private async Task SortChgane(string orderBy)

        {
            _restoMenusParameters.orderBy = orderBy;

            await GetRestoPaging();

        }



        private void AddToCart(RestoMenusDto product)
        {
            Cart.Instance.AddItem(product);
            OpenCart();
        }

        private void UpdateItemQuantity(CartItem item, int newQuantity)
        {
            if (newQuantity < 1)
            {
                newQuantity = 1;
            }

            if (item.RestoMenusOrder != null) Cart.Instance.UpdateQuantity(item.RestoMenusOrder.RemeId, newQuantity);

            // Refresh the cart view
            StateHasChanged();
        }

        private static void RemoveItem(int veproId)
        {
            Cart.Instance.RemoveItem(veproId);
        }

        [Parameter]
        public bool CartIsOpen { get; set; } = false;

        private void OpenCart()
        {
            CartIsOpen = true;
        }

        private void CloseCart()
        {
            CartIsOpen = !CartIsOpen;
        }

        [Inject]
        public IOrmeDetailHttpRepository OrmeRepo { get; set; }
        public List<OrmeDetailDto> OrmeList { get; set; } = new List<OrmeDetailDto>();
        private NewOrderMenusDto _tocreate = new();
    
        //public Cart? Cart { get; set; }
        private string SelectedPay { get; set; } = "B";
        private string OrmeCard { get; set; }
        public async Task OnCreate()
        {
            foreach (var i in Cart.Instance.Items)
            {

                //Console.WriteLine(i.RestoMenusOrder.RemeId);
                //Console.WriteLine(i.RestoMenusOrder.RemePrice);
                //Console.WriteLine(i.Quantity);
                //Console.WriteLine(5000);
                //Console.WriteLine(SelectedPay);
                //Console.WriteLine(OrmeCard);
                //Console.WriteLine("P");
                //Console.WriteLine(1);

                //Console.WriteLine(i.RestoMenusOrder.RemeId);
                _tocreate.OmdeRemeId = i.RestoMenusOrder.RemeId;
                _tocreate.OrmePrice = i.RestoMenusOrder.RemePrice;
                _tocreate.OrmeQty = (short)i.Quantity;
                _tocreate.OrmeDiscount = 0;
                _tocreate.OrmePayType = "-";
                _tocreate.OrmeCardnumber = "-";
                _tocreate.OrmeIsPaid = "-";
                _tocreate.OrmeUserId = 1;
                _tocreate.OrmeStatus = "Open";
                Console.WriteLine(i.RestoMenusOrder.RemePrice);

                await OrmeRepo.CreateDetail(_tocreate);
                await Task.Delay(500);
                NavigationManager.NavigateTo("/completeOrder");
            }
            //IEnumerator<int> o = null;
            // @omde_reme_id = 58
            //,@orme_price = 20000
            //,@orme_qty = 3
            //,@orme_discount = 5000
            //,@orme_pay_type = 'CA'
            //,@orme_cardnumber = 'kjk'
            //,@orme_is_paid = 'P'
            //,@orme_user_id = 1
            //,@orme_status = 'Ordered'
            //return o;

        }




    }


    public class CartItem
    {


        public RestoMenusDto RestoMenusOrder { get; set; }
        public int Quantity { get; set; }

        public decimal Subtotal
        {
            get { return RestoMenusOrder.RemePrice * Quantity; }
        }
    }





    public class Cart
    {

        // Singleton instance
        public static readonly Cart Instance = new Cart();

        // Private constructor to prevent direct instantiation
        private Cart() { }

        // List of cart items
        public List<CartItem> items = new List<CartItem>();

        // Add item to cart
        public void AddItem(RestoMenusDto product, int quantity = 1)
        {
            // Check if item already exists in cart
            var existingItem = items.FirstOrDefault(item => item.RestoMenusOrder.RemeId == product.RemeId);

            if (existingItem != null)
            {
                // Item already exists, update the quantity
                existingItem.Quantity += quantity;
            }
            else
            {
                // Item doesn't exist yet, add it to the cart
                items.Add(new CartItem
                {
                    RestoMenusOrder = product,
                    Quantity = quantity
                });
            }
        }

        // Remove item from cart
        public void RemoveItem(int remeId)
        {
            var itemToRemove = items.FirstOrDefault(item => item.RestoMenusOrder.RemeId == remeId);

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }

        // Update item quantity in cart
        public void UpdateQuantity(int remeId, int quantity)
        {
            var itemToUpdate = items.FirstOrDefault(item => item.RestoMenusOrder.RemeId == remeId);

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

        // Get all items in cart
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        // Get total number of items in cart
        public int TotalItems
        {
            get { return items.Sum(item => item.Quantity); }
        }

        // Get total price of items in cart (excluding tax)
        public decimal TotalPriceExcludingTax
        {
            get { return items.Sum(item => item.RestoMenusOrder.RemePrice * item.Quantity); }
        }

        // Get tax amount (10% of total price)
        public decimal TaxAmount
        {
            get { return TotalPriceExcludingTax * 0.1m; }
        }

        // Get total price of items in cart (including tax)
        public decimal TotalPrice
        {
            get { return TotalPriceExcludingTax + TaxAmount; }
        }

    }
}


    //public class Product
    //{
    //    public int VeproId { get; set; }
    //    public int StockId { get; set; }
    //    public string? VendorName { get; set; }
    //    public string? StockName { get; set; }
    //    public int Price { get; set; }
    //    public int EmpId { get; set; }
    //}

