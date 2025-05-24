using CineVerse.api.Services.Interfaces;
using CineVerse.api.Options;
using Microsoft.Extensions.Options;
using CineVerse.api.Services;
using System.Net.Http.Headers;
using CineVerse.api.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TmdbOptions>(builder.Configuration.GetSection("Tmdb"));

builder.Services.AddHttpClient("tmdb", (sp, client) =>
{
    var opt = sp.GetRequiredService<IOptions<TmdbOptions>>().Value;
    client.BaseAddress = new Uri(opt.BaseUrl); 
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});


builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ICountryService, CountryService>();


builder.Services.AddDbContext<CineverseDb>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
