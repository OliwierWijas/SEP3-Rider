using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IReservationService
{
    Task CreateAsync(ReservationCreationDTO dto);
    Task CompleteAsync(int reservationNumber);
    Task DeleteAsync(int reservationNumber);
    
    Task<IEnumerable<Reservation>> ReadCustomerReservations(int customerId);

    Task<IEnumerable<Reservation>> ReadFoodSellerReservations(int foodSellerId);

    
}