using CineVerse.client.ApiResponses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class MultiSelectComponent<TItem, TValue> : ComponentBase
{
    [Parameter] public string? Label { get; set; }

    [Parameter] public IEnumerable<TItem> Items { get; set; } = Enumerable.Empty<TItem>();

    [Parameter] public Func<TItem, string> TextSelector { get; set; } = default!;

    [Parameter] public Func<TItem, TValue> ValueSelector { get; set; } = default!;

    [Parameter] public List<TValue> SelectedValues { get; set; } = new();
    [Parameter] public EventCallback<List<TValue>> SelectedValuesChanged { get; set; }

    public bool IsOpen { get; set; } = false;
    ElementReference MenuRef;

    string ToggleText => SelectedValues.Count == 0 ? "Nessuna" : "Selezionate";

    async Task Toggle(TValue id)
    {
        if (SelectedValues.Contains(id))
            SelectedValues.Remove(id);
        else
            SelectedValues.Add(id);

        await SelectedValuesChanged.InvokeAsync(SelectedValues);
    }

    async Task ClearAll()
    {
        SelectedValues.Clear();
        await SelectedValuesChanged.InvokeAsync(SelectedValues);
    }

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
}
