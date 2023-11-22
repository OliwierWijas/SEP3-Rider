namespace Domain.DTOs;

public class ReadFoodOffersDTO
{
    public int Id { get; set; }
    public int FoodSellerId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public MyDate StartPickupTime { get; set; }
    public MyDate EndPickupTime { get; set; }
    public string FoodSellerName { get; set; }
    public string FoodSellerAddress { get; set; }
    public bool IsReserved { get; set; }
    public bool IsCompleted { get; set; }

    public ReadFoodOffersDTO(int id, int foodSellerId,string title, string description, double price, MyDate startPickupTime, MyDate endPickupTime, string foodSellerName, string foodSellerAddress, bool isReserved, bool isCompleted)
    {
        Id = id;
        FoodSellerId = foodSellerId;
        Title = title;
        Description = description;
        Price = price;
        StartPickupTime = startPickupTime;
        EndPickupTime = endPickupTime;
        FoodSellerName = foodSellerName;
        FoodSellerAddress = foodSellerAddress;
        IsReserved = isReserved;
        IsCompleted = isCompleted;
    }
}