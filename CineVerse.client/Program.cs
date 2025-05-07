using CineVerse.client;
using CineVerse.client.Options;
using CineVerse.client.Services;
using CineVerse.client.Services.Interfaces;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddSingleton<AppState>();

builder.Services.AddSingleton(AppTheme.Dark);

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection("Api"));

builder.Services.AddSingleton(sp =>
{
    var opt = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
    var rest = new RestClient(new RestClientOptions(opt.BaseUrl)
    {
        ThrowOnAnyError = false
    });

    return rest;
});


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
