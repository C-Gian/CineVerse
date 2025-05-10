using CineVerse.api.ApiResponses;
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

    [HttpGet("images")]
    [ProducesResponseType(typeof(IEnumerable<DetailImagesResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetImagesMovieDetail([FromQuery] int movieId, CancellationToken ct = default)
    {
        var movie = await _movieService.GetImagesMovieDetail(movieId, ct);
        return Ok(movie);
    }

    [HttpGet("recommendations")]
    [ProducesResponseType(typeof(IEnumerable<MovieResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRecommendationsMovieDetail([FromQuery] int movieId, CancellationToken ct = default)
    {
        var movie = await _movieService.GetRecommendationsMovieDetail(movieId, ct);
        return Ok(movie);
    }

    [HttpGet("providers")]
    [ProducesResponseType(typeof(IEnumerable<DetailWatchProvidersResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProvidersMovieDetail([FromQuery] int movieId, CancellationToken ct = default)
    {
        var movie = await _movieService.GetProvidersMovieDetail(movieId, ct);
        return Ok(movie);
    }

    [HttpGet("cast")]
    [ProducesResponseType(typeof(IEnumerable<DetailCastApiResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCastMovieDetail([FromQuery] int movieId, CancellationToken ct = default)
    {
        var movie = await _movieService.GetCastMovieDetail(movieId, ct);
        return Ok(movie);
    }

    [HttpGet("detail")]
    [ProducesResponseType(typeof(IEnumerable<MovieDetailResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMovieDetail([FromQuery] int movieId, CancellationToken ct = default)
    {
        var movie = await _movieService.GetMovieDetail(movieId, ct);
        return Ok(movie);
    }

    [HttpGet("now_playing")]
    [ProducesResponseType(typeof(IEnumerable<MovieResultResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNowPlayingMovies([FromQuery] int page = 1, CancellationToken ct = default)
    {
        var movies = await _movieService.GetNowPlayingMovies(page, ct);
        return Ok(movies);
    }

    [HttpGet("popular")]
    [ProducesResponseType(typeof(IEnumerable<MovieResultResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPopularMovies([FromQuery] int page = 1, CancellationToken ct = default)
    {
        var movies = await _movieService.GetPopularMovies(page, ct);
        return Ok(movies);
    }

    [HttpGet("upcoming")]
    [ProducesResponseType(typeof(IEnumerable<MovieResultResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUpcomingMovies([FromQuery] int page = 1, CancellationToken ct = default)
    {
        var movies = await _movieService.GetUpcomingMovies(page, ct);
        return Ok(movies);
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<MovieResultResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchMovie([FromQuery] string query, [FromQuery] int page = 1, CancellationToken ct = default)
    {
        var movies = await _movieService.SearchMovie(query, page, ct);
        return Ok(movies);
    }
}
