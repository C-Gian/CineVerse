using CineVerse.api.ApiResponses;
using CineVerse.api.Models;
using CineVerse.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CineVerse.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    #region Fields

    private readonly IMovieService _movieService;
    private readonly ILogger<MovieController> _logger;

    #endregion

    public MovieController(IMovieService movieService,
                           ILogger<MovieController> logger)
    {
        _movieService = movieService;
        _logger = logger;
    }

    [HttpGet("discover")]
    public async Task<IActionResult> DiscoverMovies([FromBody] SearchFiltersModel filters, [FromQuery] int page = 1, CancellationToken ct = default)
    {
        var movies = await _movieService.DiscoverMoviesAsync(filters, page, ct);
        return Ok(movies);
    }

    [HttpGet("movie_certifications")]
    public async Task<IActionResult> GetMovieCertifications(CancellationToken ct = default)
    {
        var certifications = await _movieService.GetMoviesCertifications(ct);
        return Ok(certifications);
    }

    [HttpGet("general_providers")]
    public async Task<IActionResult> GetGeneralWatchProviders([FromQuery] string language, [FromQuery] string region, CancellationToken ct = default)
    {
        var providers = await _movieService.GetGeneralWatchProviders(language, region, ct);
        return Ok(providers);
    }

    [HttpGet("videos")]
    public async Task<IActionResult> GetVideoMovieDetail([FromQuery] int movieId, CancellationToken ct = default)
    {
        var movie = await _movieService.GetVideoMovieDetail(movieId, ct);
        return Ok(movie);
    }

    [HttpGet("images")]
    public async Task<IActionResult> GetImagesMovieDetail([FromQuery] int movieId, CancellationToken ct = default)
    {
        var movie = await _movieService.GetImagesMovieDetail(movieId, ct);
        return Ok(movie);
    }

    [HttpGet("recommendations")]
    public async Task<IActionResult> GetRecommendationsMovieDetail([FromQuery] int movieId, CancellationToken ct = default)
    {
        var movie = await _movieService.GetRecommendationsMovieDetail(movieId, ct);
        return Ok(movie);
    }

    [HttpGet("providers")]
    public async Task<IActionResult> GetProvidersMovieDetail([FromQuery] int movieId, CancellationToken ct = default)
    {
        var movie = await _movieService.GetProvidersMovieDetail(movieId, ct);
        return Ok(movie);
    }

    [HttpGet("cast")]
    public async Task<IActionResult> GetCastMovieDetail([FromQuery] int movieId, CancellationToken ct = default)
    {
        var movie = await _movieService.GetCastMovieDetail(movieId, ct);
        return Ok(movie);
    }

    [HttpGet("detail")]
    public async Task<IActionResult> GetMovieDetail([FromQuery] int movieId, CancellationToken ct = default)
    {
        var movie = await _movieService.GetMovieDetail(movieId, ct);
        return Ok(movie);
    }

    [HttpGet("now_playing")]
    public async Task<IActionResult> GetNowPlayingMovies([FromQuery] int page = 1, CancellationToken ct = default)
    {
        var movies = await _movieService.GetNowPlayingMovies(page, ct);
        return Ok(movies);
    }

    [HttpGet("popular")]
    public async Task<IActionResult> GetPopularMovies([FromQuery] int page = 1, CancellationToken ct = default)
    {
        var movies = await _movieService.GetPopularMovies(page, ct);
        return Ok(movies);
    }

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingMovies([FromQuery] int page = 1, CancellationToken ct = default)
    {
        var movies = await _movieService.GetUpcomingMovies(page, ct);
        return Ok(movies);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchMovie([FromQuery] string query, [FromQuery] int page = 1, CancellationToken ct = default)
    {
        var movies = await _movieService.SearchMovie(query, page, ct);
        return Ok(movies);
    }
}
