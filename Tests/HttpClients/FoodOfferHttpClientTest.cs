using System.Security.Claims;
using Domain;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientImplementations;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.HttpClients;

public class FoodOfferHttpClientTest
{
    private FoodOfferHttpClient _client;
    private JwtAuthHttpClient _authClient;
    private ClaimsPrincipal claimsPrincipal = null;

    [SetUp]
    public void Setup()
    {
        var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        _authClient = new JwtAuthHttpClient(httpClientFactory);
        _authClient.OnAuthStateChanged = principal => { claimsPrincipal = principal; };
        _client = new FoodOfferHttpClient(new HttpClient { BaseAddress = new Uri("https://localhost:7195") }, _authClient);
    }
    [Test]
    public async Task creating_food_offer_adds_it_to_the_database()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("burgerhaven@gmail.com", "password1"));
        MyDate start = new MyDate(2023, 12, 14, 10, 00);
        MyDate end = new MyDate(2023, 12, 15, 10, 00);
        FoodSeller foodSeller = new FoodSeller(1, "", "", "Burger Haven", "Main Street 123, Horsens");
        FoodOfferCreationDTO dto = new FoodOfferCreationDTO(1, "Title", "Description", 100, start, end, "");
        Assert.DoesNotThrowAsync(() => _client.CreateAsync(dto));
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("janczyszyndominika@gmail.com", "password1"));
        FoodOffer foodOffer = await _client.GetFoodOfferByIdAsync(4);
        Assert.That(foodOffer.Id, Is.EqualTo(4));
        Assert.That(foodOffer.Title, Is.EqualTo("Title"));
        Assert.That(foodOffer.Description, Is.EqualTo("Description"));
        Assert.That(foodOffer.FoodSeller, Is.EqualTo(foodSeller));
        Assert.That(foodOffer.StartPickupTime, Is.EqualTo(start));
        Assert.That(foodOffer.EndPickupTime, Is.EqualTo(end));
        Assert.False(foodOffer.IsReserved);
        Assert.False(foodOffer.IsCompleted);
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());
    }
    [Test] 
    public async Task reading_available_food_offers_returns_all_available_food_offers()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("burgerhaven@gmail.com", "password1"));
        List<FoodOffer> foodOffers = await _client.GetAvailableFoodOffersAsync();
        Assert.True(foodOffers.Count > 0);
        foreach (var var in foodOffers)
        {
            Assert.False(var.IsReserved);
            Assert.False(var.IsCompleted);
        }
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());
    }
    [Test]
    public async Task reading_food_offers_by_food_seller_id_returns_offers_that_belong_only_to_specific_food_seller()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("burgerhaven@gmail.com", "password1"));
        List<FoodOffer> foodOffers = await _client.GetFoodOffersByFoodSellerIdAsync(1);
        Assert.True(foodOffers.Count > 0);
        foreach (var var in foodOffers)
        {
            Assert.That(var.FoodSeller.AccountId, Is.EqualTo(1));
        }
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());
    }
    [Test]
    public async Task reading_specific_food_offer_returns_all_its_information()
    {
        MyDate start = new MyDate(2023, 12, 14, 10, 00);
        MyDate end = new MyDate(2023, 12, 15, 10, 00);
        FoodSeller foodSeller = new FoodSeller(1, "", "", "Burger Haven", "Main Street 123, Horsens");
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("janczyszyndominika@gmail.com", "password1"));
        FoodOffer foodOffer = await _client.GetFoodOfferByIdAsync(4);
        Assert.That(foodOffer.Id, Is.EqualTo(4));
        Assert.That(foodOffer.Title, Is.EqualTo("Title"));
        Assert.That(foodOffer.Description, Is.EqualTo("Description"));
        Assert.That(foodOffer.FoodSeller, Is.EqualTo(foodSeller));
        Assert.That(foodOffer.StartPickupTime, Is.EqualTo(start));
        Assert.That(foodOffer.EndPickupTime, Is.EqualTo(end));
        Assert.False(foodOffer.IsReserved);
        Assert.False(foodOffer.IsCompleted);
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());
    }
    [Test]
    public async Task updating_food_offer_changes_its_data()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("burgerhaven@gmail.com", "password1"));
        MyDate start = new MyDate(2023, 12, 12, 11, 00);
        MyDate end = new MyDate(2023, 12, 13, 13, 00);
        FoodSeller foodSeller = new FoodSeller(1, "", "", "Burger Haven", "Main Street 123, Horsens");
        FoodOffer offer = new FoodOffer(4, foodSeller, "NewTitle", "NewDescription", 120, start, end, false, false);
        Assert.DoesNotThrowAsync(() => _client.UpdateAsync(offer));
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("janczyszyndominika@gmail.com", "password1"));
        FoodOffer foodOffer = await _client.GetFoodOfferByIdAsync(4);
        Assert.That(foodOffer.Id, Is.EqualTo(4));
        Assert.That(foodOffer.Title, Is.EqualTo("NewTitle"));
        Assert.That(foodOffer.Description, Is.EqualTo("NewDescription"));
        Assert.That(foodOffer.Price, Is.EqualTo(120));
        Assert.That(foodOffer.FoodSeller, Is.EqualTo(foodSeller));
        Assert.That(foodOffer.StartPickupTime, Is.EqualTo(start));
        Assert.That(foodOffer.EndPickupTime, Is.EqualTo(end));
        Assert.False(foodOffer.IsReserved);
        Assert.False(foodOffer.IsCompleted);
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());
    }
    [Test]
    public async Task deleting_food_offer_removes_it_from_the_database()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("burgerhaven@gmail.com", "password1"));
        Assert.DoesNotThrowAsync(() => _client.DeleteAsync(4));
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("janczyszyndominika@gmail.com", "password1"));
        Assert.ThrowsAsync<Exception>(() => _client.GetFoodOfferByIdAsync(4));
        Assert.DoesNotThrowAsync(()=>_authClient.LogoutAsync());
    }
}