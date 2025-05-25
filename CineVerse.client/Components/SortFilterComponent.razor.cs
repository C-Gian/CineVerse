using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class SortFilterComponent
{
    #region Properties
    [Parameter] public string SortBy { get; set; } = "popularity.desc";
    [Parameter] public EventCallback<string> SortByChanged { get; set; }
    public bool IsOpen { get; set; } = false;

    #endregion


    #region Fields

    ElementReference MenuRef;

    private readonly List<(string Value, string Label)> Options =
        new()
        {
            ("popularity.desc",          "Popularity ↓"),
            ("popularity.asc",           "Popularity ↑"),
            ("primary_release_date.desc","Release date ↓"),
            ("primary_release_date.asc", "Release date ↑"),
            ("revenue.desc",             "Revenue ↓"),
            ("revenue.asc",              "Revenue ↑"),
            ("vote_average.desc",        "Rating ↓"),
            ("vote_average.asc",         "Rating ↑"),
            ("vote_count.desc",          "Vote count ↓"),
            ("vote_count.asc",           "Vote count ↑"),
            ("original_title.asc",       "Title A → Z"),
            ("original_title.desc",      "Title Z → A")
        };

    #endregion


    #region Methods
    async Task TogglePointer()
    {
        if (IsOpen)
        {
            IsOpen = false;
            return;
        }

        IsOpen = true;
        await MenuRef.FocusAsync();
    }

    async Task LostFocus(FocusEventArgs _)
    {
        IsOpen = false;
        await Task.Delay(100);
    }

    async Task GainedFocus(FocusEventArgs _)
    {
        if (!IsOpen)
        {
            IsOpen = true;
            await MenuRef.FocusAsync();
        }
    }

    async Task Pick(string v)
    {
        SortBy = v;
        IsOpen = false;
        await SortByChanged.InvokeAsync(v);
    }

    #endregion
}
