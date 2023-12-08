namespace Domain.Models;

public class Rate
{
    public Customer CustomerId { get; set; }
    public FoodSeller FoodSellerId{ get; set; }
    public int rate { get; set; }

    public Rate(Customer customerId, FoodSeller foodSellerId, int rate)
    {
        CustomerId = customerId;
        FoodSellerId = foodSellerId;
        this.rate = rate;
    }
}