using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.SessionStorage;
using Danilvar.Components;
using DanilvarKanji.Client;
using DanilvarKanji.Client.Extensions;
using DanilvarKanji.Client.Handlers;
using DanilvarKanji.Client.JsWrapper;
using DanilvarKanji.Client.Localization;
using DanilvarKanji.Client.Services;
using DanilvarKanji.Client.Services.Auth;
using DanilvarKanji.Client.Services.Characters;
using DanilvarKanji.Client.Services.Dictionary;
using DanilvarKanji.Client.Services.Flashcards;
using DanilvarKanji.Client.Services.KanjiApiDev;
using DanilvarKanji.Client.Services.OCR;
using DanilvarKanji.Client.Services.Review;
using DanilvarKanji.Client.State;
using DanilvarKanji.Shared.Responses.Character;
using KristofferStrube.Blazor.FileSystemAccess;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7106/") });

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder
    .Services.AddHttpClient("ServerApi")
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServerUrl"] ?? ""))
    .AddHttpMessageHandler<AuthHandler>();

builder
    .Services.AddHttpClient("KanjiApiDev")
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["KanjiApiDev"] ?? ""))
    .AddHttpMessageHandler<AuthHandler>();

builder
    .Services.AddHttpClient("JMdict")
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["JMdict"] ?? ""))
    .AddHttpMessageHandler<AuthHandler>();

builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddTransient<AuthHandler>();
builder.Services.AddSingleton<ICharacterService, CharacterService>();
builder.Services.AddSingleton<IKanjiService, KanjiService_KAD>();
builder.Services.AddScoped<IDictionaryService, DictionaryService>();
builder.Services.AddScoped<ICharacterLearningApiService, CharacterLearningApiService>();
builder.Services.AddScoped<CharacterLearningService>();
builder.Services.AddScoped<
    IBaseQueryService<CharacterResponseResponseFull>,
    BaseQueryService<CharacterResponseResponseFull>
>();
builder.Services.AddScoped<ILocalizationService, LocalizationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<Js>();

builder.Services.AddScoped<AppState>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IFlashcardApiService, FlashcardApiService>();
builder.Services.AddSingleton<IOCRService, OCRService>();
builder.Services.AddSingleton<HotkeyService>();

builder.Services.AddComponents();

builder.Services.AddBlazoredSessionStorageAsSingleton();
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddBlazoredModal();
builder.Services.AddFileSystemAccessService();
builder.Services.AddFileSystemAccessServiceInProcess(options =>
{
    // The file at this path in this example is manually copied to wwwroot folder
    // options.BasePath = "content/";
    // options.ScriptPath = $"custom-path/{FileSystemAccessOptions.DefaultNamespace}.js";
});

builder.Services.AddAuthorizationCore();

WebAssemblyHost host = builder.Build();
await host.SetDefaultCulture();
await host.RunAsync();