using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class Carousel<TItem> : ComponentBase, IDisposable
{
    [Parameter] public List<TItem> Items { get; set; } = new();
    [Parameter] public RenderFragment<TItem> SlideTemplate { get; set; } = default!;
    [Parameter] public int VisibleCount { get; set; }
    [Parameter] public int AutoplayMs { get; set; } = 5000;
    [Parameter] public string SectionName { get; set; }
    [Parameter] public int CardWidth { get; set; }
    [Parameter] public int Margin { get; set; }
    [Parameter] public bool IsLink { get; set; }
    private int TotalPages => (int)Math.Ceiling(Items.Count / (double)VisibleCount);
    private string TrackStyle => $"transform: translateX(-{_page * (VisibleCount * CardWidth + Margin)}px);";


    #region Fields

    private int _page = 0;
    private Timer? _timer;

    #endregion


    protected override void OnInitialized()
    {
        if (AutoplayMs > 0)
        {
            _timer = new Timer(_ => InvokeAsync(Next), null, AutoplayMs, AutoplayMs);
        }
    }

    private void Next() {
        ResetTimer();
        _page = _page < TotalPages - 1 ? ++_page : 0; 
    }
    private void Prev()
    {
        ResetTimer();
        _page = Math.Max(_page - 1, 0);
    }
    private void Pause() => _timer?.Change(Timeout.Infinite, Timeout.Infinite);
    private void Resume() => _timer?.Change(AutoplayMs, AutoplayMs);

    private void ResetTimer()
    {
        if (_timer is not null && AutoplayMs > 0)
        {
            _timer.Change(AutoplayMs, AutoplayMs);
        }
    }
    public void Dispose() => _timer?.Dispose();
}
