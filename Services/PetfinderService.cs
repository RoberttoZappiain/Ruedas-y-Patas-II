using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RuedaYPatas.DTOs;

namespace RuedaYPatas.Services;

public class PetfinderService : IPetfinderService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private static string _accessToken;
    private static DateTime _tokenExpiration;

    public PetfinderService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    private async Task<string> GetAccessTokenAsync()
    {
        if (!string.IsNullOrEmpty(_accessToken) && _tokenExpiration > DateTime.UtcNow)
        {
            return _accessToken;
        }

        var client = _httpClientFactory.CreateClient("Petfinder");
        var requestBody = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", _configuration["PetfinderSettings:ClientId"]),
            new KeyValuePair<string, string>("client_secret", _configuration["PetfinderSettings:ClientSecret"])
        });

        var response = await client.PostAsync("oauth2/token", requestBody);
        response.EnsureSuccessStatusCode();

        var contentStream = await response.Content.ReadAsStreamAsync();
        var tokenResponse = await JsonSerializer.DeserializeAsync<PetfinderTokenResponse>(contentStream);

        _accessToken = tokenResponse.AccessToken;
        _tokenExpiration = DateTime.UtcNow.AddHours(1).AddMinutes(-5); // Token dura 1 hora, lo renovamos 5 mins antes

        return _accessToken;
    }

    public async Task<IEnumerable<PetfinderBreed>> GetBreedsAsync(string animalType)
    {
        var token = await GetAccessTokenAsync();
        var client = _httpClientFactory.CreateClient("Petfinder");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync($"types/{animalType}/breeds");
        if (!response.IsSuccessStatusCode) return Enumerable.Empty<PetfinderBreed>();

        var contentStream = await response.Content.ReadAsStreamAsync();
        var breedsResponse = await JsonSerializer.DeserializeAsync<PetfinderBreedsResponse>(contentStream);

        return breedsResponse?.Breeds ?? Enumerable.Empty<PetfinderBreed>();
    }
}