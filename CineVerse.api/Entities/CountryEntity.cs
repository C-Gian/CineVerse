using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineVerse.api.Entities;

public class CountryEntity
{
    public string Code { get; set; } = string.Empty;

    public string EnglishName { get; set; } = string.Empty;

    public string NativeName { get; set; } = string.Empty;
}
