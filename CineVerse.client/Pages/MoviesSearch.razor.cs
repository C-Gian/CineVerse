using CineVerse.client.ApiResponses;
using CineVerse.client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Globalization;

namespace CineVerse.client.Pages;

public partial class MoviesSearch
{
    #region Properties

    [Inject] public IMovieService MovieService { get; set; }

    [Inject] public IGenreService GenreService { get; set; }
    [Inject] public AppState AppState { get; set; }

    public List<MovieResultResponse> Movies { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();
    public bool IsLoading { get; set; } = false;
    public int CurrentPage { get; set; }

    #endregion


    #region Fields

    private string _query = string.Empty;

    private readonly SemaphoreSlim _gate = new(1, 1);

    private List<int> SelectedGenres = new();

    #endregion


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        IsLoading = true;
        await LoadMoviesAsync(1);
        await LoadGenresAsync();
        IsLoading = false;
    }

    private async Task LoadMoviesAsync(int pageNumber)
    {
        await _gate.WaitAsync();

        try
        {
            Movies = [];

            var movieResponse = await MovieService.GetPopularMovies((pageNumber * 2) - 1) ?? new MoviesApiResponse();
            var movieResponse2 = await MovieService.GetPopularMovies(pageNumber * 2) ?? new MoviesApiResponse();

            Movies.AddRange(movieResponse.Results);
            Movies.AddRange(movieResponse2.Results);

            CurrentPage = pageNumber;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            _gate.Release();
        }
    }

    private async Task LoadGenresAsync()
    {
        Genres = await GenreService.GetGenres() ?? new List<Genre>();
    }

    private async Task SearchAsync()
    {
        Movies = await MovieService.SearchMovie(_query, 1);
    }

    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key is "Enter")
        {
            await SearchAsync();
        }
    }

    private void UpdateGenres(List<int> genres)
    {
        SelectedGenres = genres;
    }

}
