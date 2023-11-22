using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Domain;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.ClientImplementations;

public class FoodOfferHttpClient : IFoodOfferService
{
    private readonly HttpClient _client;
    private readonly IAuthService _authService;
    
    public FoodOfferHttpClient(HttpClient client, IAuthService authService)
    {
        _client = client;
        _authService = authService;
    }
    
    public async Task CreateAsync(FoodOfferCreationDTO dto)
    {
        HttpResponseMessage message = await _client.PostAsJsonAsync("/FoodOffers", dto);
        if (!message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task UpdateAsync(FoodOffer foodOffer)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        _client.DefaultRequestHeaders.Add("MustBeFoodSeller", "foodseller");
        HttpResponseMessage message = await _client.PatchAsJsonAsync("/FoodOffers", foodOffer);
        if (!message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            throw new Exception(content);
        } 
    }

    public async Task DeleteAsync(int foodOfferId)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        _client.DefaultRequestHeaders.Add("MustBeFoodSeller", "foodseller");
        HttpResponseMessage response = await _client.DeleteAsync($"/FoodOffers/{foodOfferId}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }    
    }

    public async Task<List<FoodOffer>> GetAvailableFoodOffersAsync()
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        HttpResponseMessage response = await _client.GetAsync("/FoodOffers");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        List<FoodOffer> foodOffers = JsonSerializer.Deserialize<List<FoodOffer>>(content, new JsonSerializerOptions{PropertyNameCaseInsensitive = true})!;
        return foodOffers;
    }

    public async Task<List<FoodOffer>> GetFoodOffersByFoodSellerIdAsync(int foodSellerId)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        HttpResponseMessage response = await _client.GetAsync($"/FoodOffers/{foodSellerId}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        List<FoodOffer> foodOffers = JsonSerializer.Deserialize<List<FoodOffer>>(content, new JsonSerializerOptions{PropertyNameCaseInsensitive = true})!;
        return foodOffers;
    }
}