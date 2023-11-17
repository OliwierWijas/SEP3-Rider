using System.Net.Http.Headers;
using System.Net.Http.Json;
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
        HttpResponseMessage message = await _client.PatchAsJsonAsync("/FoodSellers", dto);
        if (!message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            throw new Exception(content);
        }    }

    public async Task DeleteAsync(int accountId)
    { 
        //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authService.Jwt);
        HttpResponseMessage response = await _client.DeleteAsync($"/FoodSellers/{accountId}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }    }
}