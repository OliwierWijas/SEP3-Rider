using System.Security.Claims;
using Application.Logic;
using Domain.DTOs;
using HttpClients.ClientImplementations;
using Microsoft.Extensions.DependencyInjection;
namespace Tests.HttpClients;

public class CustomersControllerTest
{
    private CustomerHttpClient _client;

    [SetUp]
    public void Setup()
    {
        // Create an instance of the default service provider for testing purposes
        var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();

        // Use the service provider to get an instance of IHttpClientFactory
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var authHttpClient = new JwtAuthHttpClient(httpClientFactory);
        // Now you can create your CustomerHttpClient
        // Ensure JwtAuthHttpClient is initialized properly
        _client = new CustomerHttpClient(new HttpClient{ BaseAddress = new Uri("https://localhost:7195") }, authHttpClient);
    }
    [Test]
    public async Task creating_customer_makes_an_account()
    {
       /* CustomerCreationDTO dto = new CustomerCreationDTO("FirstName", "LastName", "Email2@via.dk", "1112223335", "Password123");
        await _client.CreateAsync(dto);*/
        var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        JwtAuthHttpClient authHttpClient = new JwtAuthHttpClient(httpClientFactory);
        await authHttpClient.LoginAsync("Email2@via.dk", "Password123");
        string token = authHttpClient.token;
        Assert.True(token.Contains("customer"));
        
    }
}