using System.Security.Claims;
using Domain;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientImplementations;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.HttpClients;

public class ReservationHttpClientTest
{
    private ReservationHttpClient _client;
    private JwtAuthHttpClient _authClient;
    private ClaimsPrincipal claimsPrincipal = null;

    [SetUp]
    public void Setup()
    {
        var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        _authClient = new JwtAuthHttpClient(httpClientFactory);
        _authClient.OnAuthStateChanged = principal => { claimsPrincipal = principal;}; 
        _client = new ReservationHttpClient(new HttpClient { BaseAddress = new Uri("https://localhost:7195") }, _authClient);
    }

    [Test]
    public async Task creating_reservation_successful()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("Email3@via.dk", "Password123"));

        ReservationCreationDTO dto = new ReservationCreationDTO(45, 18);
        Assert.DoesNotThrowAsync(() => _client.CreateAsync(dto));
        IEnumerable<Reservation> reservations = await _client.ReadCustomerReservations(45);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "Name", "Street 10, Horsens");
        MyDate start = new MyDate(2023, 12, 11, 10, 00);
        MyDate end = new MyDate(2023, 12, 12, 10, 00);
        Reservation reservation = new Reservation(18, "Title", "Description", 100, start, end, 21, foodSeller, null, false);
        Assert.True(reservations.Contains(reservation));
        
        Assert.DoesNotThrowAsync(() => _authClient.LogoutAsync());
    }

    [Test]
    public async Task read_food_seller_reservations_successful()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("burger.concept@gmail.com", "Password123"));

        IEnumerable<Reservation> reservations = await _client.ReadFoodSellerReservations(35);
        Customer customer = new Customer("FirstName", "LastName", "", "", null);
        MyDate start = new MyDate(2023, 12, 11, 10, 00);
        MyDate end = new MyDate(2023, 12, 12, 10, 00);
        Reservation reservation = new Reservation(18, "Title", "Description", 100, start, end, 21, null, customer, false);
        Assert.True(reservations.Contains(reservation));
        
        Assert.DoesNotThrowAsync(() => _authClient.LogoutAsync());
    }

    [Test]
    public async Task read_customer_reservations_successful()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("Email3@via.dk", "Password123"));

        IEnumerable<Reservation> reservations = await _client.ReadCustomerReservations(45);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "Name", "Street 10, Horsens");
        MyDate start = new MyDate(2023, 12, 11, 10, 00);
        MyDate end = new MyDate(2023, 12, 12, 10, 00);
        Reservation reservation = new Reservation(18, "Title", "Description", 100, start, end, 21, foodSeller, null, false);
        Assert.True(reservations.Contains(reservation));
        
        Assert.DoesNotThrowAsync(() => _authClient.LogoutAsync());
    }

    [Test]
    public async Task reserving_reserved_food_offer_throws_exception()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("Email3@via.dk", "Password123"));

        ReservationCreationDTO dto = new ReservationCreationDTO(45, 18);
        Assert.CatchAsync<Exception>(() => _client.CreateAsync(dto));
        
        Assert.DoesNotThrowAsync(() => _authClient.LogoutAsync());
    }

    [Test]
    public async Task completing_reservation_changes_its_status()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("burger.concept@gmail.com", "Password123"));
        
        Assert.DoesNotThrowAsync(() => _client.CompleteAsync(21));
        IEnumerable<Reservation> reservations = await _client.ReadCustomerReservations(45);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "Name", "Street 10, Horsens");
        MyDate start = new MyDate(2023, 12, 11, 10, 00);
        MyDate end = new MyDate(2023, 12, 12, 10, 00);
        Reservation reservation = new Reservation(18, "Title", "Description", 100, start, end, 21, foodSeller, null, true);
        Assert.True(reservations.Contains(reservation));
        
        Assert.DoesNotThrowAsync(() => _authClient.LogoutAsync());
    }

    [Test]
    public async Task canceling_reservation_successful()
    {
        Assert.DoesNotThrowAsync(() => _authClient.LoginAsync("Email3@via.dk", "Password123"));
        
        Assert.DoesNotThrowAsync(() => _client.DeleteAsync(21));
        IEnumerable<Reservation> reservations = await _client.ReadCustomerReservations(45);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "Name", "Street 10, Horsens");
        MyDate start = new MyDate(2023, 12, 11, 10, 00);
        MyDate end = new MyDate(2023, 12, 12, 10, 00);
        Reservation reservation = new Reservation(18, "Title", "Description", 100, start, end, 21, foodSeller, null, true);
        Assert.False(reservations.Contains(reservation));
        
        Assert.DoesNotThrowAsync(() => _authClient.LogoutAsync());
    }
}