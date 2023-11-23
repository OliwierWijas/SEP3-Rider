namespace Domain.DTOs;

public class ReservationCreationDTO
{
    public int CustomerId { get; set; }
    public int FoodOfferId { get; set; }

    public ReservationCreationDTO(int customerId, int foodOfferId)
    {
        CustomerId = customerId;
        FoodOfferId = foodOfferId;
    }
}