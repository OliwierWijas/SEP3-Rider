namespace Domain;

public class FoodOffer
{
    public int FoodOfferId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public Date StartPickupTime { get; set; }
    public Date EndPickupTime { get; set; }

    public FoodOffer(int foodOfferId, string title, string description, double price, Date startPickupTime, Date endPickupTime)
    {
        FoodOfferId = foodOfferId;
        Title = title;
        Description = description;
        Price = price;
        StartPickupTime = startPickupTime;
        EndPickupTime = endPickupTime;
    }
}