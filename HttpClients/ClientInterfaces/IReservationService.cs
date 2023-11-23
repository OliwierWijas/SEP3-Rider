using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IReservationService
{
    Task CreateAsync(ReservationCreationDTO dto);
    Task CompleteAsync(int reservationNumber);
    Task DeleteAsync(int reservationNumber);
    Task<List<ReadCustomerReservationDTO>> ReadCustomerReservations(int customerId);
    Task<List<ReadCustomerReservationDTO>> ReadCompletedCustomerReservations(int customerId);
    Task<List<ReadFoodSellerReservationDTO>> ReadFoodSellerReservations(int foodSellerId);
    Task<List<ReadFoodSellerReservationDTO>> ReadCompletedFoodSellerReservations(int foodSellerId);
}