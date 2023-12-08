using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain;
using Domain.DTOs;
using Domain.Models;
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
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        _client.DefaultRequestHeaders.Add("MustBeFoodSeller", "foodseller");
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
        string foodOfferAsJson = JsonSerializer.Serialize(foodOffer);
        StringContent body = new StringContent(foodOfferAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage message = await _client.PatchAsync("/FoodOffers", body);
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

    public async Task<FoodOffer> GetFoodOfferAsync(int foodSellerId, int foodOfferId)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        HttpResponseMessage response = await _client.GetAsync($"/FoodOffers/{foodSellerId}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        IEnumerable<FoodOffer> foodOffers = JsonSerializer.Deserialize<IEnumerable<FoodOffer>>(content, new JsonSerializerOptions{PropertyNameCaseInsensitive = true})!;
        FoodOffer foodOffer = foodOffers.FirstOrDefault(f => f.Id == foodOfferId);
        return foodOffer;
    }

    public async Task<FoodOffer> GetFoodOfferByIdAsync(int id)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        HttpResponseMessage response = await _client.GetAsync($"/FoodOffers/FoodOffer/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        FoodOffer foodOffer = JsonSerializer.Deserialize<FoodOffer>(content, new JsonSerializerOptions{PropertyNameCaseInsensitive = true})!;
        
        return foodOffer;
    }
}