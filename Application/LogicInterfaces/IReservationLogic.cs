using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IReservationLogic
{
    Task<List<ReadCustomerReservationDTO>> ReadCustomerReservations(int customerId);
    Task<List<ReadFoodSellerReservationDTO>> ReadFoodSellerReservations(int foodSellerId);
    Task CreateAsync(ReservationCreationDTO dto);
    Task DeleteAsync(int foodOfferId);
    Task CompleteReservationAsync(int reservationNumber);
}