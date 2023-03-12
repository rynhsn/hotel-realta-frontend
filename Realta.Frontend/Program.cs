using Realta.Frontend;
using Realta.Frontend.HttpRepository.Purchasing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7068/api/") });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// booking

// hotel
// builder.Services.AddScoped<IHotelsHttpRepository, HotelsHttpRepository>();

// hr

// master

// payment

// purchasing
builder.Services.AddScoped<IPurchaseOrderHttpRepository, PurchaseOrderHttpRepository>();

// resto

// users


await builder.Build().RunAsync();