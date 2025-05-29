using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using static MudBlazor.Colors;

namespace CineVerse.client.Components;

public partial class SingleSelectComponent<TItem, TValue> : ComponentBase
{
    #region Properties
    [Parameter] public IEnumerable<TItem> Items { get; set; } = Enumerable.Empty<TItem>();
    [Parameter] public Func<TItem, string> TextSelector { get; set; } = default!;
    [Parameter] public Func<TItem, TValue> ValueSelector { get; set; } = default!;
    [Parameter] public TValue? SelectedValue { get; set; }
    [Parameter] public EventCallback<TValue?> SelectedValueChanged { get; set; }
    [Parameter] public bool Searchable { get; set; } = false;
    public bool IsOpen { get; set; } = false;
    string DisplayText =>
        Items.FirstOrDefault(i => Equals(ValueSelector(i), SelectedValue)) is { } sel
        ? TextSelector(sel)
        : "Select…";
    IEnumerable<TItem> Filtered =>
        string.IsNullOrWhiteSpace(SearchTerm)
            ? Items
            : Items.Where(i => TextSelector(i)
                        .Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));

    #endregion


    #region Fields

    private string SearchTerm = "";
    ElementReference MenuRef;

    #endregion


    #region Methods

    async Task Pick(TValue value)
    {
        SelectedValue = value;
        IsOpen = false;
        SearchTerm = "";
        await SelectedValueChanged.InvokeAsync(value);
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
