using CineVerse.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CineVerse.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    #region Fields

    private readonly IGenreService _genreService;
    private readonly ILogger<GenreController> _logger;

    #endregion

    public GenreController(IGenreService genreService, ILogger<GenreController> logger)
    {
        _genreService = genreService;
        _logger = logger;
    }


    [HttpGet("all")]
    public async Task<IActionResult> GetAll(CancellationToken ct = default)
    {
        var result = await _genreService.GetMovieGenres(ct);
        return Ok(result);
    }
}
