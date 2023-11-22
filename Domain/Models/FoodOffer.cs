namespace Domain;

public class FoodOffer
{
    public int FoodOfferId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public MyDate StartPickupTime { get; set; }
    public MyDate EndPickupTime { get; set; }

    public FoodOffer(int foodOfferId, string title, string description, double price, MyDate startPickupTime, MyDate endPickupTime)
    {
        FoodOfferId = foodOfferId;
        Title = title;
        Description = description;
        Price = price;
        StartPickupTime = startPickupTime;
        EndPickupTime = endPickupTime;
    }
}