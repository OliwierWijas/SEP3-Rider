using System.Security.Claims;
using Domain;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientImplementations;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.HttpClients;

public class RatingAndCommentHttpClientTest
{
    private RatingAndCommentHttpClient _client;
    private JwtAuthHttpClient _authClient;
    private ClaimsPrincipal claimsPrincipal = null;

    [SetUp]
    public void Setup()
    {
        var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        _authClient = new JwtAuthHttpClient(httpClientFactory);
        _authClient.OnAuthStateChanged = principal => { claimsPrincipal = principal;}; 
        _client = new RatingAndCommentHttpClient(new HttpClient { BaseAddress = new Uri("https://localhost:7195") }, _authClient);
    }

    [Test]
    public async Task creating_rating_successful()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("Email3@via.dk", "Password123"));

        RatingBasicDTO dto = new RatingBasicDTO(35, 45, 3);
        Assert.DoesNotThrowAsync(() => _client.CreateRatingAsync(dto));
        
        Assert.DoesNotThrowAsync(() => _authClient.LogoutAsync());
    }

    [Test]
    public async Task creating_comment_successful()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("Email3@via.dk", "Password123"));

        CommentBasicDTO dto = new CommentBasicDTO(35, 45, "comment1");
        Assert.DoesNotThrowAsync(() => _client.CreateCommentAsync(dto));
        List<Comment> comments = await _client.ReadCommentsByFoodSellerIdAsync(35);
        Customer customer = new Customer("FirstName", "LastName", "", "", null);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "", "");
        MyDate date = new MyDate(2023, 12, 12, 0, 0);
        Comment comment = new Comment(18, customer, foodSeller, date, "comment1");
        Assert.True(comments.Contains(comment));
        
        Assert.DoesNotThrowAsync(() => _authClient.LogoutAsync());
    }

    [Test]
    public async Task updating_rating_successful()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("Email3@via.dk", "Password123"));

        RatingBasicDTO dto = new RatingBasicDTO(35, 45, 4);
        Assert.DoesNotThrowAsync(() => _client.UpdateRatingAsync(dto));
        
        Assert.DoesNotThrowAsync(() => _authClient.LogoutAsync());
    }

    [Test]
    public async Task reading_average_rating_of_food_seller_returns_correct_value()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("Email3@via.dk", "Password123"));

        double avg = await _client.ReadAverageRatingByFoodSellerIdAsync(35);
        Assert.That(avg, Is.EqualTo(4));
        
        Assert.DoesNotThrowAsync(() => _authClient.LogoutAsync());
    }

    [Test]
    public async Task reading_comments_of_food_seller_returns_correct_data()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("Email3@via.dk", "Password123"));
        
        List<Comment> comments = await _client.ReadCommentsByFoodSellerIdAsync(35);
        Customer customer = new Customer("FirstName", "LastName", "", "", null);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "", "");
        MyDate date = new MyDate(2023, 12, 12, 0, 0);
        Comment comment = new Comment(18, customer, foodSeller, date, "comment1");
        Assert.True(comments.Contains(comment));
        
        Assert.DoesNotThrowAsync(() => _authClient.LogoutAsync());
    }
}