using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PCStore.UI;
using PCStore.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44393/") });

await builder.Build().RunAsync();
