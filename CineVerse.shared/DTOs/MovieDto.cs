using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerse.shared.DTOs;
public class MovieDto
{
    public int Id { get; init; }
    public string Title { get; init; } = default!;
    public string? Overview { get; init; }
    public string? PosterPath { get; init; }
    public DateOnly? Release { get; init; }
}
