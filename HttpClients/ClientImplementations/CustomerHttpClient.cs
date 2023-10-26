using System.Net.Http.Json;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.ClientImplementations;

public class CustomerHttpClient : ICustomerService
{
    private readonly HttpClient _client;

    public CustomerHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task CreateAsync(CustomerCreationDTO dto)
    {
        HttpResponseMessage message = await _client.PostAsJsonAsync("/Customers", dto);
        if (!message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task UpdateAsync(CustomerUpdateDTO dto)
    {
        HttpResponseMessage message = await _client.PatchAsJsonAsync("/Customers", dto);
        if (!message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    
}