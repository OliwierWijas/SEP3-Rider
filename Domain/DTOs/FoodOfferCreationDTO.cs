namespace Domain.DTOs;

public class FoodOfferCreationDTO
{
    public int FoodSellerId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public Date StartPickupTime { get; set; }
    public Date EndPickupTime { get; set; }

    public FoodOfferCreationDTO(int foodSellerId, string title, string description, double price, Date startPickupTime, Date endPickupTime)
    {
        FoodSellerId = foodSellerId;
        Title = title;
        Description = description;
        Price = price;
        StartPickupTime = startPickupTime;
        EndPickupTime = endPickupTime;
    }
}