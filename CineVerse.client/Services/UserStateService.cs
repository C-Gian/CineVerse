using CineVerse.shared.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace CineVerse.client.Services;
public class UserStateService
{
    public SearchFiltersModel? LastSearch { get; private set; }
    public string? SelectedLanguage { get; private set; }
    public bool IsDarkMode { get; private set; }

    public Task SaveSearchAsync(SearchFiltersModel model, IJSRuntime js)
    {
        LastSearch = model;
        var json = JsonSerializer.Serialize(model);
        return js.InvokeVoidAsync("localStorage.setItem", "lastSearch", json).AsTask();
    }

    public async Task<SearchFiltersModel?> LoadSearchAsync(IJSRuntime js)
    {
        var json = await js.InvokeAsync<string>("localStorage.getItem", "lastSearch");
        LastSearch = string.IsNullOrWhiteSpace(json)
            ? null
            : JsonSerializer.Deserialize<SearchFiltersModel>(json);
        return LastSearch;
    }
}

