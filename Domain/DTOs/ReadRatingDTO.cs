namespace Domain.DTOs;

public class ReadRatingDTO
{
    public int CustomerId { get; set; }
    public int FoodSellerId{ get; set; }

    public ReadRatingDTO(int customerId, int foodSellerId)
    {
        CustomerId = customerId;
        FoodSellerId = foodSellerId;
    }
}