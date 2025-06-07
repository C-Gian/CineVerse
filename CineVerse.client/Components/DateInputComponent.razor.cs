using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class DateInputComponent
{
    [Parameter] public int? Year { get; set; }
    [Parameter] public int? Month { get; set; }
    [Parameter] public int? Day { get; set; }
    [Parameter] public EventCallback<(int? year, int? month, int? day)> OnChange { get; set; }

    private bool IsOpen { get; set; }

    private string DisplayText
    {
        get
        {
            if (Year is null) return "yyyy / MM / dd";
            if (Month is null) return $"{Year} / MM / dd";
            if (Day is null) return $"{Year:D4} / {Month:D2} / dd";
            return $"{Year:D4} / {Month:D2} / {Day:D2}";
        }
    }

    private void Toggle() => IsOpen = !IsOpen;

    protected override async Task OnParametersSetAsync()
    {
        await OnChange.InvokeAsync((Year, Month, Day));
    }
}
