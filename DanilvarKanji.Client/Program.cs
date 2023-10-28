using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DanilvarKanji.Client;
using DanilvarKanji.Client.Extensions;
using DanilvarKanji.Client.Localization;
using DanilvarKanji.Client.Services.Characters;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7106/") });

builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<ILocalizationService, LocalizationService>();

builder.Services.AddBlazoredLocalStorage();

//await builder.Build().RunAsync();

WebAssemblyHost host = builder.Build();
await host.SetDefaultCulture(); // Retrieves local storage value and sets the thread's current culture.
await host.RunAsync();