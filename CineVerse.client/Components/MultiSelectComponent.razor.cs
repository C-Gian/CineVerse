using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class MultiSelectComponent<TItem, TValue> : ComponentBase
{
    #region Properties 
    [Parameter] public IEnumerable<TItem> Items { get; set; } = Enumerable.Empty<TItem>();

    [Parameter] public Func<TItem, string> TextSelector { get; set; } = default!;

    [Parameter] public Func<TItem, TValue> ValueSelector { get; set; } = default!;

    [Parameter] public List<TValue> SelectedValues { get; set; } = new();
    [Parameter] public EventCallback<List<TValue>> SelectedValuesChanged { get; set; }
    [Parameter] public bool IsEnabled { get; set; } = true;

    public bool IsOpen { get; set; } = false;
    private string ToggleText => SelectedValues.Count == 0 ? "No Selection" : "Selected";
    private string SelectedLabel =>
        SelectedValues.Count == 0 ? "" : $"{SelectedValues.Count}";

    #endregion


    #region Fields

    ElementReference MenuRef;

    #endregion


    #region Methods

    async Task Toggle(TValue id)
    {
        Console.WriteLine("Toggle called");
        if (SelectedValues.Contains(id))
        {
            SelectedValues.Remove(id);
        }
        else
        {
            SelectedValues.Add(id);
        }
        await SelectedValuesChanged.InvokeAsync(SelectedValues);
    }

    async Task ClearAll()
    {
        SelectedValues.Clear();
        await SelectedValuesChanged.InvokeAsync(SelectedValues);
    }

    async Task TogglePointer()
    {
        Console.WriteLine("TogglePointer called");
        if (IsOpen)
        {
            Console.WriteLine("TogglePointer called2");
            IsOpen = false;
            return;
        }

        IsOpen = true;
        await MenuRef.FocusAsync();
    }

    async Task LostFocus(FocusEventArgs _)
    {
        Console.WriteLine("LostFocus");
        IsOpen = false;
        await Task.Delay(100);
    }

    async Task GainedFocus(FocusEventArgs _)
    {
        Console.WriteLine("GainedFocus");
        if (!IsOpen)
        {
            Console.WriteLine("GainedFocus2");
            IsOpen = true;
            await MenuRef.FocusAsync();
        }
    }

    #endregion
}
