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
        ReservationCreationDTO dto = new ReservationCreationDTO(10, 16);
        //Assert.DoesNotThrowAsync(() => _logic.CreateAsync(dto));
        IEnumerable<Reservation> reservations = await _logic.ReadCustomerReservations(10);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "Name", "Street 10, Horsens");
        MyDate start = new MyDate(2023, 12, 11, 10, 00);
        MyDate end = new MyDate(2023, 12, 12, 10, 00);
        Reservation reservation = new Reservation(16, "Title", "Description", 100, start, end, 13, foodSeller, null, false);
        Console.WriteLine(reservation.ToString());
        Assert.True(reservations.Contains(reservation));
    }
}