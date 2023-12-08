namespace Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public Customer Customer { get; set; }
    public FoodSeller FoodSeller { get; set; }
    public MyDate Date{ get; set; }
    public string Content { get; set; }

    public Comment(int id, Customer customer, FoodSeller foodSeller, MyDate date, string content)
    {
        Id = id;
        Customer = customer;
        FoodSeller = foodSeller;
        Date = date;
        Content = content;
    }
}