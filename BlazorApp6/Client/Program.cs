global using BlazorApp6.Client.Services.ItemService;
global using BlazorApp6.Shared.Models;
global using Microsoft.AspNetCore.Components.Authorization;
using BlazorApp6.Client;
using BlazorApp6.Client.Services;
using BlazorApp6.Client.Services.MatterialService;
using BlazorApp6.Client.Services.AuthService;
using BlazorApp6.ViewModels;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp6.Client.Services.NoteService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMatterialService, MatterialService>();
builder.Services.AddScoped<INoteService,NoteService>();
builder.Services.AddScoped<IClipboardService, ClipboardService>();
builder.Services.AddSingleton<UserViewModel>();
builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthStateProvidedr >();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredSessionStorage();

await builder.Build().RunAsync();