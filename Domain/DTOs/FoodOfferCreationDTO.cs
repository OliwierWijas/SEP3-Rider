namespace Domain.DTOs;

public class FoodOfferCreationDTO
{
    public int FoodSellerId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public MyDate StartPickupTime { get; set; }
    public MyDate EndPickupTime { get; set; }
    public string Photo { get; set; }

    public FoodOfferCreationDTO(int foodSellerId, string title, string description, double price, MyDate startPickupTime, MyDate endPickupTime, string photo)
    {
        FoodSellerId = foodSellerId;
        Title = title;
        Description = description;
        Price = price;
        StartPickupTime = startPickupTime;
        EndPickupTime = endPickupTime;
        Photo = photo;
    }
}