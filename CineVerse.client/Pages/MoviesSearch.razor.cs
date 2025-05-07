using CineVerse.client.Models;
using CineVerse.client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Pages;

public partial class MoviesSearch
{
    #region Properties
    public List<Movie> Movies { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();
    public bool IsLoading { get; set; } = false;
    public int CurrentPage { get; set; }

    [Inject]
    public IMovieService MovieService { get; set; }

    [Inject]
    public IGenreService GenreService { get; set; }

    #endregion

    #region Fields

    private string _query = string.Empty;

    private readonly SemaphoreSlim _gate = new(1, 1);

    #endregion

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        IsLoading = true;
        await LoadMoviesAsync(1);
        IsLoading = false;
    }

    private async Task LoadMoviesAsync(int pageNumber)
    {
        await _gate.WaitAsync();

        //problem with this solution: page + cached | cached + new page != from same page but clicked far away.
        //example: we are in 1, so 2 is cached, when we go 2, we load 19 cached and add 2 called
        //then we go 7, then we return 2, so 3 is cached, so we have 20 from 2nd page and 1 from third page
        //and this is not equal to the 2 page at the beginning: 2 page beginning = 19 cached + 2 from 3,
        //2 page now is 20 full from api + 1 cached from page 3, and this cause a bad behaviour.
        //2 solution: first one, placeholder in each page and so we can use 7 column with 3 rows, no buffer, no async
        //all synced with api. Second one, 2 call for each page, 8 columns, 5 rows, but this is a bit desync cause
        //for each page on our site, we have 2 page api side. 
        try
        {
            Movies = [];

            var movieResponse = await GetPopularMoviesAsync((pageNumber * 2) - 1);
            var movieResponse2 = await GetPopularMoviesAsync(pageNumber*2);

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

    private async Task<MoviesApiResponse> GetPopularMoviesAsync(int pageNumber)
    {
        var response = await MovieService.GetPopularMovies(pageNumber) ?? new MoviesApiResponse();
        return response;
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
}
