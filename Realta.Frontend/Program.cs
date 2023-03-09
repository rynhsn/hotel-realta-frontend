using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Realta.Frontend;
<<<<<<< HEAD
=======
using Realta.Frontend.HttpRepository;
using Realta.Frontend.HttpRepository.Hotel;
>>>>>>> 8e2c3fdfe8ae09866aeae4c53e1563fa9817fbeb

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7068/api/") });

// booking

// hotel
builder.Services.AddScoped<IHotelsHttpRepository, HotelsHttpRepository>();

// hr

// master

// payment

// purchasing
<<<<<<< HEAD
// builder.Services.AddScoped<IPurchaseOrderHttpRepository, PurchaseOrderHttpRepository>();
=======
>>>>>>> 8e2c3fdfe8ae09866aeae4c53e1563fa9817fbeb

// resto

// users


await builder.Build().RunAsync();