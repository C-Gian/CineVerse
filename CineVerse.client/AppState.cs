﻿using CineVerse.client.Utils;
using CineVerse.shared.ApiResponses;
using CineVerse.shared.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace CineVerse.client;

public class AppState
{
    public SectionType SectionType { get; set; }
    public List<Genre> Genres { get; set; } = new();
    public string Location { get; set; } = "IT";
    public SearchFiltersModel? LastSearch { get; private set; }
    public string? SelectedLanguage { get; private set; }
    public bool IsDarkMode { get; private set; }
    public string? LastPage { get; private set; }
    public string? CurrentPage { get; private set; }
    
    public static readonly Dictionary<string, string> GenreBackgrounds = new() //home genres pics
    {
        ["Adventure"] = "https://image.tmdb.org/t/p/original/ueDw7djPgKPZfph0vC43aD2EMyF.jpg",
        ["Fantasy"] = "https://image.tmdb.org/t/p/original/x2RS3uTcsJJ9IfjNPcgDmukoEcQ.jpg",
        ["Animation"] = "https://image.tmdb.org/t/p/original/wXsQvli6tWqja51pYxXNG1LFIGV.jpg",
        ["Drama"] = "https://image.tmdb.org/t/p/original/kXfqcdQKsToO0OUXHcrrNCHDBzO.jpg",
        ["Horror"] = "https://image.tmdb.org/t/p/original/mmd1HnuvAzFc4iuVJcnBrhDNEKr.jpg",
        ["Action"] = "https://image.tmdb.org/t/p/original/oIwfoUFfWfESn0Y8u8jv9lc8li1.jpg",
        ["Comedy"] = "https://image.tmdb.org/t/p/original/1KgXxv6tHXOnakqYvMPvFwYKWiw.jpg",
        ["History"] = "https://image.tmdb.org/t/p/original/zb6fM1CX41D9rF9hdgclu0peUmy.jpg",
        ["Western"] = "https://image.tmdb.org/t/p/original/x4biAVdPVCghBlsVIzB6NmbghIz.jpg",
        ["Thriller"] = "https://image.tmdb.org/t/p/original/p1PLSI5Nw2krGxD7X4ulul1tDAk.jpg",
        ["Crime"] = "https://image.tmdb.org/t/p/original/tmU7GeKVybMWFButWEGl2M4GeiP.jpg",
        ["Documentary"] = "https://image.tmdb.org/t/p/original/yOpNvGSsT0YECuhhrlGBLIshh0u.jpg",
        ["Science Fiction"] = "https://image.tmdb.org/t/p/original/qr7dUqleMRd0VgollazbmyP9XjI.jpg",
        ["Mystery"] = "https://image.tmdb.org/t/p/original/77aHwg1SCy89rfvQtiruPU58qEV.jpg",
        ["Music"] = "https://image.tmdb.org/t/p/original/nlPCdZlHtRNcF6C9hzUH4ebmV1w.jpg",
        ["Romance"] = "https://image.tmdb.org/t/p/original/sCzcYW9h55WcesOqA12cgEr9Exw.jpg",
        ["Family"] = "https://image.tmdb.org/t/p/original/mXLVA0YL6tcXi6SJSuAh9ONXFj5.jpg",
        ["War"] = "https://image.tmdb.org/t/p/original/bdD39MpSVhKjxarTxLSfX6baoMP.jpg",
        ["TV Movie"] = "https://image.tmdb.org/t/p/original/jXZ2tyJl44yKvh22I6ooQwU5rFM.jpg"
    };

    public readonly List<(string Value, string Label)> SortOptions =
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

    public void UpdateCurrentPage(string uri)
    {
        LastPage = CurrentPage ?? uri;
        CurrentPage = uri;
    }

    public string GetLogicalRoute(string uri)
    {
        if (uri.Contains("/movie/", StringComparison.OrdinalIgnoreCase))
            return "detail";
        if (uri.Contains("/search", StringComparison.OrdinalIgnoreCase))
            return "search";
        return "home";
    }
}
