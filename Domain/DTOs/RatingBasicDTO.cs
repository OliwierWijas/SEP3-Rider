namespace Domain.DTOs;

public class RatingBasicDTO
{
    public int FoodSellerId { get; set; }
    public int CustomerId {get; set; }
    public int Rate { get; set; }

    public RatingBasicDTO(int foodSellerId, int customerId, int rate)
    {
        FoodSellerId = foodSellerId;
        CustomerId = customerId;
        Rate = rate;
    }
}