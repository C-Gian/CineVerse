﻿
using CineVerse.shared.ApiResponses;
using CineVerse.shared.Models;

namespace CineVerse.api.Services.Interfaces;

public interface IMovieService
{
    public Task<MovieResponse> GetNowPlayingMovies(int page, CancellationToken ct = default);

    public Task<MovieResponse> GetPopularMovies(int page, CancellationToken ct = default);

    public Task<MovieResponse> GetUpcomingMovies(int page, CancellationToken ct = default);

    public Task<MovieResponse> SearchMovie(string query, int page, CancellationToken ct = default);

    public Task<MovieDetailResponse> GetMovieDetail(int movieId, CancellationToken ct = default);

    public Task<DetailImagesResponse> GetImagesMovieDetail(int movieId, CancellationToken ct = default);

    public Task<MovieResponse> GetRecommendationsMovieDetail(int movieId, CancellationToken ct = default);

    public Task<DetailWatchProvidersResponse> GetProvidersMovieDetail(int movieId, CancellationToken ct = default);

    public Task<DetailCastApiResponse> GetCastMovieDetail(int movieId, CancellationToken ct = default);

    public Task<DetailVideoResponse> GetVideoMovieDetail(int movieId, CancellationToken ct = default);

    public Task<GeneralWatchProvidersResponse> GetGeneralWatchProviders(string language, string region, CancellationToken ct = default);

    public Task<MovieCertificationsApiResponse> GetMoviesCertifications(CancellationToken ct = default);

    public Task<MovieResponse> DiscoverMoviesAsync(SearchFiltersModel filters, int page, CancellationToken ct = default);
}
