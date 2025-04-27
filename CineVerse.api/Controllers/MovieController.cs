using CineVerse.api.ApiResponses;
using CineVerse.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CineVerse.api.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/[controller]")]
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

    [HttpGet("popular")]
    [ProducesResponseType(typeof(IEnumerable<MovieResultResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPopularMovies([FromQuery] int page = 1, CancellationToken ct = default)
    {
        var movies = await _movieService.GetPopularMovies(page, ct);
        return Ok(movies);
    }
}
