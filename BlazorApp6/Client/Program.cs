global using BlazorApp6.Client.Services.ItemService;
global using BlazorApp6.Shared.Models;
global using Microsoft.AspNetCore.Components.Authorization;
using BlazorApp6.Client;
using BlazorApp6.Client.Services;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthStateProvidedr >();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredSessionStorage();

await builder.Build().RunAsync();