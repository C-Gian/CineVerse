using CineVerse.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CineVerse.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    #region Fields

    private readonly ICountryService _countryService;
    private readonly ILogger<CountryController> _logger;

    #endregion

    public CountryController(ICountryService countryService, ILogger<CountryController> logger)
    {
        _countryService = countryService;
        _logger = logger;
    }


    [HttpGet("all")]
    public async Task<IActionResult> GetAll(CancellationToken ct = default)
    {
        var result = await _countryService.GetCountriesAsync(ct);
        return Ok(result);
    }
}
