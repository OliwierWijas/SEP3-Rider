using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IReservationLogic
{
    Task<IEnumerable<ReadCustomerReservationDTO>> ReadCustomerReservations(int customerId);
    Task<IEnumerable<ReadFoodSellerReservationDTO>> ReadFoodSellerReservations(int foodSellerId);
    Task CreateAsync(ReservationCreationDTO dto);
    Task DeleteAsync(int foodOfferId);
    Task CompleteReservationAsync(int reservationNumber);
}