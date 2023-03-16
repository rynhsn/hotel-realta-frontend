using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Realta.Frontend;
using Realta.Frontend.HttpRepository.Booking;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7068/api/") });

// booking
builder.Services.AddScoped<IHotelHttpRepository, HotelHttpRepository>();
builder.Services.AddScoped<ISpecialOfferHttpRepository, SpecialOfferHttpRepository>();



// hotel

// hr

// master

// payment

// purchasing
// builder.Services.AddScoped<IPurchaseOrderHttpRepository, PurchaseOrderHttpRepository>();

// resto

// users


await builder.Build().RunAsync();