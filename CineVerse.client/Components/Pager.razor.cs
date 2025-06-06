using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class Pager<TItem> : ComponentBase
{
    #region Properties

    [Parameter]
    public IEnumerable<TItem> Items { get; set; }

    [Parameter]
    public EventCallback<int> PageChanged { get; set; }

    [Parameter]
    public int CurrentPage { get; set; }

    #endregion

    #region Fields

    private int PageSize { get; set; } = 40;
    private int TotalPages { get; set; } = 500;
    private List<TItem> PaginatedData => Items.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
    private bool IsFirstPageDisabled => CurrentPage == 1;
    private bool IsPreviousDisabled => CurrentPage == 1;
    private bool IsNextDisabled => CurrentPage == TotalPages;
    private bool IsLastPageDisabled => CurrentPage == TotalPages;
    private const int MaxButtons = 10;
    private int _current => Math.Clamp(CurrentPage, 1, TotalPages);

    #endregion

    private void FirstPage()
    {
        CurrentPage = 1;

        PageChanged.InvokeAsync(CurrentPage);
    }

    private void NextPage()
    {
        if (CurrentPage < TotalPages)
        {
            CurrentPage++;
        }
        PageChanged.InvokeAsync(CurrentPage);
    }

    private void PreviousPage()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
        }

        PageChanged.InvokeAsync(CurrentPage);
    }

    private void LastPage()
    {
        CurrentPage = TotalPages;

        PageChanged.InvokeAsync(CurrentPage);
    }

    private void GoToPage(int pageNumber)
    {
        CurrentPage = pageNumber;

        PageChanged.InvokeAsync(pageNumber);
    }

    private IEnumerable<int> PageWindow
    {
        get
        {
            if (TotalPages <= MaxButtons)
                return Enumerable.Range(1, TotalPages);

            int half = MaxButtons / 2;
            int start = _current - half;
            int end = _current + half - 1;

            if (start < 1) { end += 1 - start; start = 1; }
            if (end > TotalPages) { start -= end - TotalPages; end = TotalPages; }

            start = Math.Max(1, start);
            return Enumerable.Range(start, MaxButtons);
        }
    }
}
