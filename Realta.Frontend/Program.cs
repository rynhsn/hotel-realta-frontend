using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Realta.Frontend;
using Realta.Frontend.HttpRepository.Payment;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7140/api/") });

// booking

// hotel
// builder.Services.AddScoped<IHotelsHttpRepository, HotelsHttpRepository>();

// hr

// master

// payment
builder.Services.AddScoped<IBankHttpRepository, BankHttpRepository>();

// purchasing
// builder.Services.AddScoped<IPurchaseOrderHttpRepository, PurchaseOrderHttpRepository>();

// resto

// users


await builder.Build().RunAsync();