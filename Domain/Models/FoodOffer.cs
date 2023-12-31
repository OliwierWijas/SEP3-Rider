namespace Domain.Models;

public class FoodOffer
{
    public int Id { get; set; }
    public FoodSeller FoodSeller { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public MyDate StartPickupTime { get; set; }
    public MyDate EndPickupTime { get; set; }
    public bool IsReserved { get; set; }
    public bool IsCompleted { get; set; }
    public string? Photo { get; set; }

    public FoodOffer(int id, FoodSeller foodSeller, string title, string description, double price, MyDate startPickupTime, MyDate endPickupTime, bool isReserved, bool isCompleted)
    {
        Id = id;
        FoodSeller = foodSeller;
        Title = title;
        Description = description;
        Price = price;
        StartPickupTime = startPickupTime;
        EndPickupTime = endPickupTime;
        IsReserved = isReserved;
        IsCompleted = isCompleted;
    }
}