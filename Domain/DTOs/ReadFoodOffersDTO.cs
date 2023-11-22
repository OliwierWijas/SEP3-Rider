namespace Domain.DTOs;

public class ReadFoodOffersDTO
{
    public int Id { get; set; }
    public int FoodSellerId { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public Date StartPickupTime { get; set; }
    public Date EndPickupTime { get; set; }
    public string FoodSellerName { get; set; }
    public bool IsReserved { get; set; }
    public bool IsCompleted { get; set; }

    public ReadFoodOffersDTO(int id, int foodSellerId, string description, double price, Date startPickupTime, Date endPickupTime, string foodSellerName, bool isReserved, bool isCompleted)
    {
        Id = id;
        FoodSellerId = foodSellerId;
        Description = description;
        Price = price;
        StartPickupTime = startPickupTime;
        EndPickupTime = endPickupTime;
        FoodSellerName = foodSellerName;
        IsReserved = isReserved;
        IsCompleted = isCompleted;
    }
}