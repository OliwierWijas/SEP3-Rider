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

    [SetUp]
    public void Setup()
    {
        var authHttpClient = new Mock<IAuthService>();
        authHttpClient.SetupGet(x => x.token).Returns("customer");
        _client = new CustomerHttpClient(new HttpClient { BaseAddress = new Uri("https://localhost:7195") }, authHttpClient.Object);
    }
    
    [Test]
    public async Task creating_customer_makes_an_account()
    {
       CustomerCreationDTO dto = new CustomerCreationDTO("FirstName", "LastName", "Email3@via.dk", "1112223365", "Password123");
       await _client.CreateAsync(dto);
       var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
       var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
       JwtAuthHttpClient login = new JwtAuthHttpClient(httpClientFactory);
       ClaimsPrincipal claimsPrincipal = null;
       login.OnAuthStateChanged = principal => { claimsPrincipal = principal;}; 
       await login.LoginAsync("Email3@via.dk", "Password123");
       IEnumerable<Claim> claims = claimsPrincipal.Claims.AsEnumerable();
       string customer = claims.FirstOrDefault(c => c.Type.Equals("MustBeCustomer"))!.Value;
       Assert.AreEqual(customer, "customer");
    }
}