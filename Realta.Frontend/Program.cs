using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Realta.Contract.Models;
using Realta.Frontend;
using Realta.Frontend.HttpRepository.Purchasing;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7068/api/") });

// booking

// hotel
// builder.Services.AddScoped<IHotelsHttpRepository, HotelsHttpRepository>();

// hr

// master

// payment

// purchasing
// builder.Services.AddScoped<IPurchaseOrderHttpRepository, PurchaseOrderHttpRepository>();
builder.Services.AddScoped<IVendorHttpRepository, VendorHttpRepository>();
builder.Services.AddScoped<IVendorProductHttpRepository, VendorProductHttpRepository>();

// resto

// users


await builder.Build().RunAsync();