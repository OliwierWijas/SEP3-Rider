using System.Security.Claims;
using Application.Logic;
using Domain.DTOs;
using HttpClients.ClientImplementations;
using HttpClients.ClientInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Moq;

namespace Tests.HttpClients;

public class CustomersControllerTest
{
    private CustomerHttpClient _client;
    private JwtAuthHttpClient _authClient;
    private ClaimsPrincipal claimsPrincipal = null;

    [SetUp]
    public void Setup()
    {
        var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        _authClient = new JwtAuthHttpClient(httpClientFactory);
        _authClient.OnAuthStateChanged = principal => { claimsPrincipal = principal;}; 
        _client = new CustomerHttpClient(new HttpClient { BaseAddress = new Uri("https://localhost:7195") }, _authClient);
    }
    
    [Test]
    public async Task creating_customer_makes_an_account()
    {
       CustomerCreationDTO dto = new CustomerCreationDTO("FirstName", "LastName", "Email3@via.dk", "1112223365", "Password123");
       await _client.CreateAsync(dto);
       _authClient.OnAuthStateChanged = principal => { claimsPrincipal = principal;}; 
       await _authClient.LoginAsync("Email3@via.dk", "Password123");
       IEnumerable<Claim> claims = claimsPrincipal.Claims.AsEnumerable();
       string customer = claims.FirstOrDefault(c => c.Type.Equals("MustBeCustomer"))!.Value;
       Assert.AreEqual(customer, "customer");
    }

    [Test]
    public async Task creating_cutomer_with_already_exsisting_information_throws_exception()
    {
        CustomerCreationDTO dto = new CustomerCreationDTO("FirstName", "LastName", "Email3@via.dk", "1112223365", "Password123");
        Assert.CatchAsync<Exception>(() => _client.CreateAsync(dto));
    }

    [Test]
    public async Task editing_customer_account_successful()
    {
        await _authClient.LoginAsync("Email3@via.dk", "Password123");
        
        CustomerUpdateDTO update = new CustomerUpdateDTO(43, "NewEmail3@via.dk", "NewPassword123", "44556622");
        await _client.UpdateAsync(update);
        
        ClaimsPrincipal claimsPrincipal = null;
        _authClient.OnAuthStateChanged = principal => { claimsPrincipal = principal;}; 
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("NewEmail3@via.dk", "NewPassword123"));
    }

    [Test]
    public async Task deleting_customer_account_successful()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("NewEmail3@via.dk", "NewPassword123"));
        Assert.DoesNotThrowAsync(() => _client.DeleteAsync(43));
        Assert.CatchAsync<Exception>(() => _authClient.LoginAsync("NewEmail3@via.dk", "NewPassword123"));
    }
}