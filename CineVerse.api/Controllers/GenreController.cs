using CineVerse.api.ApiResponses;
using CineVerse.api.Services.Interfaces;
using CineVerse.shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CineVerse.api.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/[controller]")]
public class GenreController : ControllerBase
{
    #region Fields

    private readonly IGenreService _genreService;
    private readonly ILogger<GenreController> _logger;

    #endregion

    public GenreController(IGenreService genreService,
                           ILogger<GenreController> logger)
    {
        _genreService = genreService;
        _logger = logger;
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<GenreResultResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct = default) =>
        Ok(await _genreService.GetMovieGenres(ct));
}
