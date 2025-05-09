using CineVerse.client.Models;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class SplitSlider : ComponentBase
{
    [Inject] public AppState AppState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Parameter] public List<Movie> Movies { get; set; } = new();
    [Parameter] public int MaxVisible { get; set; } = 5;
    [Parameter] public int AutoplayMs { get; set; } = 5000;

    private Movie? Selected;
    private int _index = 0;
    private Timer? _timer;

    protected override void OnInitialized()
    {
        if (Movies.Any())
        {
            Movies = Movies.Take(MaxVisible).ToList();
            Selected = Movies[0];
        }

        _timer = new Timer(_ =>
        {
            if (Movies.Count <= 1) return;
            _index = (_index + 1) % Movies.Count;
            Selected = Movies[_index];
            InvokeAsync(StateHasChanged);
        }, null, AutoplayMs, AutoplayMs);
    }

    private void Select(Movie movie)
    {
        Selected = movie;
        _index = Movies.IndexOf(movie);
        ResetTimer();
    }

    private void ResetTimer() => _timer?.Change(AutoplayMs, AutoplayMs);
    private void Pause() => _timer?.Change(Timeout.Infinite, Timeout.Infinite);
    private void Resume() => _timer?.Change(AutoplayMs, AutoplayMs);
    public void Dispose() => _timer?.Dispose();

    private static DateTime GetDate(string? date) =>
        DateTime.TryParse(date, out var d) ? d : DateTime.MinValue;

    private static string PosterUrl(string? path, string size = "w185") =>
        string.IsNullOrWhiteSpace(path) ? "/placeholder.png" : $"https://image.tmdb.org/t/p/{size}{path}";

    private static string BackdropUrl(string? path, string size = "w780") =>
        string.IsNullOrWhiteSpace(path) ? "/placeholder.png" : $"https://image.tmdb.org/t/p/{size}{path}";

    public void NavigateToDetails()
    {
        var url = $"/movie/{Selected.Id}";
        NavigationManager.NavigateTo(url);
    }
}
