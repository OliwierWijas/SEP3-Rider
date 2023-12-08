using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IReservationLogic
{
    Task<IEnumerable<Reservation>> ReadCustomerReservations(int customerId);
    Task<IEnumerable<Reservation>> ReadFoodSellerReservations(int foodSellerId);
    Task CreateAsync(ReservationCreationDTO dto);
    Task DeleteAsync(int foodOfferId);
    Task CompleteReservationAsync(int reservationNumber);
}