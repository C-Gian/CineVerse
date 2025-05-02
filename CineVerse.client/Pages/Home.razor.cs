using CineVerse.client.Models;
using CineVerse.client.Services.Interfaces;
using CineVerse.Client.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Pages;

public partial class Home
{
    #region Properties
    public List<int> PagesLoaded { get; set; } = new();
    public List<Movie> Movies { get; set; } = new();
    public Queue<Movie> MovieBuffer { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();
    public bool IsLoading { get; set; } = false;
    public int TotalPages { get; set; }

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
        PagesLoaded.AddRange([1, 2]);
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

            if (!PagesLoaded.Contains(pageNumber))
            {
                //calling a non cached page.
                //example: currently on page 1, means page 1 and 2 are loaded, we call page 3 or further.
                MovieBuffer = [];

                var movieResponse = await GetPopularMoviesAsync(pageNumber);
                var movieResponse2 = await GetPopularMoviesAsync(pageNumber + 1);

                Movies.AddRange(movieResponse.Results);
                Movies.AddRange(movieResponse2.Results);

                PagesLoaded = [pageNumber, pageNumber + 1];

                if (Movies.Count > 21)
                {
                    MovieBuffer.EnqueueRange(Movies.Skip(21));
                    Movies = Movies.Take(21).ToList();
                }
            } 
            else
            {
                //calling the next page from the current one, so we need to load from the buffer.
                //example: currently on page 1, means page 1 and 2 are loaded, we call page 2.
                if (MovieBuffer.Any())
                {
                    Movies.AddRange(MovieBuffer.DequeueChunk(MovieBuffer.Count));
                    MovieBuffer = [];

                    var movieResponse = await GetPopularMoviesAsync(pageNumber + 1);
                    Movies.AddRange(movieResponse.Results);

                    PagesLoaded = [pageNumber, pageNumber + 1];

                    if (Movies.Count < 21)
                    {
                        var movieResponse2 = await GetPopularMoviesAsync(pageNumber + 2);
                        Movies.AddRange(movieResponse2.Results);

                        PagesLoaded.Add(pageNumber + 2);
                    }

                    if (Movies.Count > 21)
                    {
                        MovieBuffer.EnqueueRange(Movies.Skip(21));
                        Movies = Movies.Take(21).ToList();
                    }
                } 
                else
                {
                    //if for any reason buffer is empty even if we call the cached page,
                    //with this code we can prevent error and load correct data anyway.
                    var movieResponse = await GetPopularMoviesAsync(pageNumber);
                    var movieResponse2 = await GetPopularMoviesAsync(pageNumber+1);

                    Movies.AddRange(movieResponse.Results);
                    Movies.AddRange(movieResponse2.Results);

                    PagesLoaded = [pageNumber, pageNumber + 1];

                    if (Movies.Count > 21)
                    {
                        MovieBuffer.EnqueueRange(Movies.Skip(21));
                        Movies = Movies.Take(21).ToList();
                    }
                }
            }
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
        TotalPages = response.TotalPages;
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
