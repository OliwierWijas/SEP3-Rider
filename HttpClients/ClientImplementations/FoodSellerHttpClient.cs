using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Domain;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.ClientImplementations;

public class FoodSellerHttpClient : IFoodSellerService
{
    private readonly HttpClient _client;
    private readonly IAuthService _authService;
    public FoodSellerHttpClient(HttpClient client, IAuthService authService)
    {
        _client = client;
        _authService = authService;
    }
    
    public async Task CreateAsync(FoodSellerCreationDTO dto)
    {
        HttpResponseMessage message = await _client.PostAsJsonAsync("/FoodSellers", dto);
        if (!message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task UpdateAsync(FoodSellerUpdateDTO dto)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        _client.DefaultRequestHeaders.Add("MustBeFoodSeller", "foodseller");
        HttpResponseMessage message = await _client.PatchAsJsonAsync("/FoodSellers", dto);
        if (!message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            throw new Exception(content);
        }    
    }

    public async Task DeleteAsync(int accountId)
    { 
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        _client.DefaultRequestHeaders.Add("MustBeFoodSeller", "foodseller");
        HttpResponseMessage response = await _client.DeleteAsync($"/FoodSellers/{accountId}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }    
    }

    public async Task<FoodSeller> GetAsync(int accountId)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        HttpResponseMessage response = await _client.GetAsync($"/FoodSellers/{accountId}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        FoodSeller seller = JsonSerializer.Deserialize<FoodSeller>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return seller;
    }

    public async Task<List<FoodSeller>> GetAllAsync()
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        _client.DefaultRequestHeaders.Add("MustBeCustomer", "customer");
        HttpResponseMessage response = await _client.GetAsync($"/FoodSellers");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        List<FoodSeller> sellers = JsonSerializer.Deserialize<List<FoodSeller>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return sellers;
    }

    public async Task<string> GetPhotoAsync(int accountId)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        HttpResponseMessage response = await _client.GetAsync($"/FoodSellers/photo/{accountId}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        return content;
    }
}