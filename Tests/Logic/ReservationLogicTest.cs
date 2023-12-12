using Application.Logic;
using Domain;
using Domain.DTOs;
using Domain.Models;

namespace Tests.Logic;

public class ReservationLogicTest
{
    private ReservationLogic _logic;
        
    [SetUp]
    public void Setup()
    {
        _logic = new ReservationLogic(new GRPCService());
    }

    [Test]
    public async Task creating_reservation_updates_status_of_food_offer()
    {
        ReservationCreationDTO dto = new ReservationCreationDTO(10, 17);
        Assert.DoesNotThrowAsync(() => _logic.CreateAsync(dto));
        IEnumerable<Reservation> reservations = await _logic.ReadCustomerReservations(10);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "Name", "Street 10, Horsens");
        MyDate start = new MyDate(2023, 12, 11, 10, 00);
        MyDate end = new MyDate(2023, 12, 12, 10, 00);
        Reservation reservation = new Reservation(16, "Title", "Description", 100, start, end, 13, foodSeller, null, false);
        Assert.True(reservations.Contains(reservation));
    }

    [Test]
    public async Task reserving_reserved_food_offer_throws_exception()
    {
        ReservationCreationDTO dto = new ReservationCreationDTO(4, 16);
        Assert.CatchAsync<Exception>(() => _logic.CreateAsync(dto));
    }

    [Test]
    public async Task reading_customer_reservations_returns_correct_data()
    {
        IEnumerable<Reservation> reservations = await _logic.ReadCustomerReservations(10);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "Name", "Street 10, Horsens");
        MyDate start = new MyDate(2023, 12, 11, 10, 00);
        MyDate end = new MyDate(2023, 12, 12, 10, 00);
        Reservation reservation = new Reservation(16, "Title", "Description", 100, start, end, 13, foodSeller, null, false);
        Assert.True(reservations.Contains(reservation));
    }

    [Test]
    public async Task reading_food_seller_reservations_returns_correct_data()
    {
        IEnumerable<Reservation> reservations = await _logic.ReadFoodSellerReservations(35);
        Customer customer = new Customer("firstName", "lastName", "", "", null);
        MyDate start = new MyDate(2023, 12, 11, 10, 00);
        MyDate end = new MyDate(2023, 12, 12, 10, 00);
        Reservation reservation = new Reservation(16, "Title", "Description", 100, start, end, 13, null, customer, false);
        Assert.True(reservations.Contains(reservation));
    }

    [Test]
    public async Task completing_reservation_changes_its_status()
    {
        await _logic.CompleteReservationAsync(13);
        IEnumerable<Reservation> reservations = await _logic.ReadCustomerReservations(10);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "Name", "Street 10, Horsens");
        MyDate start = new MyDate(2023, 12, 11, 10, 00);
        MyDate end = new MyDate(2023, 12, 12, 10, 00);
        Reservation reservation = new Reservation(16, "Title", "Description", 100, start, end, 13, foodSeller, null, true);
        Assert.True(reservations.Contains(reservation));
    }

    [Test]
    public async Task deleting_reservation_removes_data_and_changes_its_status()
    {
        Assert.DoesNotThrowAsync(() => _logic.DeleteAsync(18));
        IEnumerable<Reservation> reservations = await _logic.ReadCustomerReservations(10);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "Name", "Street 10, Horsens");
        MyDate start = new MyDate(2023, 12, 12, 11, 18);
        MyDate end = new MyDate(2023, 12, 13, 11, 18);
        Reservation reservation = new Reservation(17, "Title", "Description", 100, start, end, 13, foodSeller, null, false);
        Assert.False(reservations.Contains(reservation));
    }
}