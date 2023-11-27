using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.ClientImplementations;

public class ReservationHttpClient : IReservationService
{
    private readonly HttpClient _client;
    private readonly IAuthService _authService;

    public ReservationHttpClient(HttpClient client, IAuthService authService)
    {
        _client = client;
        _authService = authService;
    }

    public async Task CreateAsync(ReservationCreationDTO dto)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        _client.DefaultRequestHeaders.Add("MustBeCustomer", "customer");
        HttpResponseMessage message = await _client.PostAsJsonAsync("/Reservations", dto);
        if (!message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task CompleteAsync(int reservationNumber)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        _client.DefaultRequestHeaders.Add("MustBeFoodSeller", "foodseller");
        HttpResponseMessage message = await _client.PatchAsJsonAsync("/Reservations", reservationNumber);
        if (!message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            throw new Exception(content);
        } 
    }

    public async Task DeleteAsync(int reservationNumber)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        _client.DefaultRequestHeaders.Add("MustBeCustomer", "customer");
        HttpResponseMessage response = await _client.DeleteAsync($"/Reservations/{reservationNumber}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }    
    }

    public async Task<IEnumerable<ReadCustomerReservationDTO>> ReadCustomerReservations(int customerId)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        _client.DefaultRequestHeaders.Add("MustBeCustomer", "customer");
        HttpResponseMessage response = await _client.GetAsync($"/CustomerReservations/{customerId}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        IEnumerable<ReadCustomerReservationDTO> reservations = JsonSerializer.Deserialize<IEnumerable<ReadCustomerReservationDTO>>(content, new JsonSerializerOptions{PropertyNameCaseInsensitive = true})!;
        return reservations;
    }
    
    public async Task<IEnumerable<ReadFoodSellerReservationDTO>> ReadFoodSellerReservations(int foodSellerId)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_authService.token);
        _client.DefaultRequestHeaders.Add("MustBeFoodSeller", "foodseller");
        HttpResponseMessage response = await _client.GetAsync($"/FoodSellerReservations/{foodSellerId}");
        string content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        IEnumerable<ReadFoodSellerReservationDTO> reservations = JsonSerializer.Deserialize<IEnumerable<ReadFoodSellerReservationDTO>>(content, new JsonSerializerOptions{PropertyNameCaseInsensitive = true})!;
     
        return reservations;
    }
    
}