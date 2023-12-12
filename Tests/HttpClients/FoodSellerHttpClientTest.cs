using Domain;
using Domain.DTOs;
using HttpClients.ClientImplementations;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.HttpClients;
using System.Security.Claims;

public class FoodSellerHttpClientTest
{
    private FoodSellerHttpClient _client;
    private JwtAuthHttpClient _authClient;
    private ClaimsPrincipal claimsPrincipal = null;

    [SetUp]
    public void Setup()
    {
        var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        _authClient = new JwtAuthHttpClient(httpClientFactory);
        _authClient.OnAuthStateChanged = principal => { claimsPrincipal = principal;}; 
        _client = new FoodSellerHttpClient(new HttpClient { BaseAddress = new Uri("https://localhost:7195") }, _authClient);
    }
    [Test]
    public async Task creating_food_seller_makes_an_account()
    {
        
        FoodSellerCreationDTO dto = new FoodSellerCreationDTO("FoodSeller1", "Main Street", 4, "Horsens", "EmailFoodSeller@via.dk", "11223344", "password", "");
        await _client.CreateAsync(dto);
        _authClient.OnAuthStateChanged = principal => { claimsPrincipal = principal;}; 
        await _authClient.LoginAsync("EmailFoodSeller@via.dk", "password");
        IEnumerable<Claim> claims = claimsPrincipal.Claims.AsEnumerable();
        string foodseller = claims.FirstOrDefault(c => c.Type.Equals("MustBeFoodSeller"))!.Value;
        Assert.AreEqual(foodseller, "foodseller");
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());

    }
    [Test]
    public async Task creating_food_seller_with_already_exsisting_information_throws_exception()
    {
        FoodSellerCreationDTO dto = new FoodSellerCreationDTO("FoodSeller1", "Main Street", 4, "Horsens", "EmailFoodSeller@via.dk", "11223344", "password", "");
        Assert.CatchAsync<Exception>(() => _client.CreateAsync(dto));
    }
    [Test]
    public async Task editing_food_seller_account_successful()
    {
        await _authClient.LoginAsync("EmailFoodSeller@via.dk", "password");
        FoodSellerUpdateDTO update = new FoodSellerUpdateDTO(19, "FoodSeller1","MainStreet1",5,"Aarhus","NewEmailFoodSeller@via.dk", "NewPassword", "44556622");
        await _client.UpdateAsync(update);
        FoodSeller foodSeller = await _client.GetAsync(19);
        ClaimsPrincipal claimsPrincipal = null;
        _authClient.OnAuthStateChanged = principal => { claimsPrincipal = principal;}; 
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("NewEmailFoodSeller@via.dk", "NewPassword"));
        
        Assert.That(foodSeller.AccountId, Is.EqualTo(19));
        Assert.That(foodSeller.Email, Is.EqualTo("NewEmailFoodSeller@via.dk"));
        Assert.That(foodSeller.Address, Is.EqualTo("MainStreet1 5, Aarhus"));
        Assert.That(foodSeller.Name, Is.EqualTo("FoodSeller1"));
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());
    }
    [Test]
    public async Task getting_food_seller_with_account_id_successful()
    {
        await _authClient.LoginAsync("NewEmailFoodSeller@via.dk", "NewPassword");
        FoodSeller foodSeller = await _client.GetAsync(19);
        ClaimsPrincipal claimsPrincipal = null;
        _authClient.OnAuthStateChanged = principal => { claimsPrincipal = principal;}; 
        Assert.That(foodSeller.AccountId, Is.EqualTo(19));
        Assert.That(foodSeller.Email, Is.EqualTo("NewEmailFoodSeller@via.dk"));
        Assert.That(foodSeller.Address, Is.EqualTo("MainStreet1 5, Aarhus"));
        Assert.That(foodSeller.Name, Is.EqualTo("FoodSeller1"));
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());
    }
    [Test]
    public async Task deleting_customer_account_successful()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("NewEmailFoodSeller@via.dk", "NewPassword"));
        Assert.DoesNotThrowAsync(() => _client.DeleteAsync(18));
        Assert.CatchAsync<Exception>(() => _authClient.LoginAsync("NewEmailFoodSeller@via.dk", "NewPassword"));
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());
    }
}