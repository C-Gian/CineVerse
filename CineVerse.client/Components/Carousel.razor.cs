using Microsoft.AspNetCore.Components;
using System.Timers;

namespace CineVerse.client.Components;

public partial class Carousel<TItem> : ComponentBase, IDisposable
{
    [Parameter] public List<TItem> Items { get; set; } = new();
    [Parameter] public RenderFragment<TItem> SlideTemplate { get; set; } = default!;
    [Parameter] public int VisibleCount { get; set; } = 5;
    [Parameter] public int SlideStep { get; set; } = 1;
    [Parameter] public int AutoplayMs { get; set; } = 4000;

    private int _index = 0;
    private System.Timers.Timer? _timer;

    protected override void OnInitialized()
    {
        if (AutoplayMs > 0)
        {
            _timer = new System.Timers.Timer(AutoplayMs);
            _timer.Elapsed += (_, _) => InvokeAsync(Next);
            _timer.Start();
        }
    }

    private double SlideWidth => 100.0 / VisibleCount;

    private string TrackStyle =>
        $"transform: translateX(-{_index * SlideWidth}%); " +
        $"width: {Items.Count * SlideWidth}%;";

    private void Next()
    {
        if (_index + VisibleCount >= Items.Count)
            return;

        _index = Math.Min(_index + SlideStep, Items.Count - VisibleCount);
        StateHasChanged();
    }

    private void Prev()
    {
        _index = Math.Max(_index - SlideStep, 0);
        StateHasChanged();
    }

    private void Pause() => _timer?.Stop();
    private void Resume() => _timer?.Start();
    public void Dispose() => _timer?.Dispose();
}
