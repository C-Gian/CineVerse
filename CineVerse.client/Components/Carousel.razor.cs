using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class Carousel<TItem> : ComponentBase, IDisposable
{
    [Parameter] public List<TItem> Items { get; set; } = new();
    [Parameter] public RenderFragment<TItem> SlideTemplate { get; set; } = default!;
    [Parameter] public int VisibleCount { get; set; } = 5;
    [Parameter] public int AutoplayMs { get; set; } = 5000;

    private int _page = 0;
    private Timer? _timer;
    private int MaxPage => Math.Max(0, (int)Math.Ceiling(Items.Count / (double)VisibleCount) - 1);
    private int TotalPages => (int)Math.Ceiling(Items.Count / (double)VisibleCount);
    private bool IsLastPage => _page >= TotalPages - 1;

    private string TrackStyle =>
    $"transform: translateX(-{_page * (VisibleCount * 175)}px);";



    protected override void OnInitialized()
    {
        if (AutoplayMs > 0)
        {
            _timer = new Timer(_ => InvokeAsync(Next), null, AutoplayMs, AutoplayMs);
        }
    }

    private void Next() => _page = Math.Min(_page + 1, TotalPages - 1);
    private void Prev() => _page = Math.Max(_page - 1, 0);

    private void Pause() => _timer?.Change(Timeout.Infinite, Timeout.Infinite);
    private void Resume() => _timer?.Change(AutoplayMs, AutoplayMs);
    public void Dispose() => _timer?.Dispose();

}
