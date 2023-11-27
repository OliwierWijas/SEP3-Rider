using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IReservationService
{
    Task CreateAsync(ReservationCreationDTO dto);
    Task CompleteAsync(int reservationNumber);
    Task DeleteAsync(int reservationNumber);
    
    Task<IEnumerable<ReadCustomerReservationDTO>> ReadCustomerReservations(int customerId);

    Task<IEnumerable<ReadFoodSellerReservationDTO>> ReadFoodSellerReservations(int foodSellerId);

    
}