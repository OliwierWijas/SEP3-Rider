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

    protected bool Equals(Comment other)
    {
        return Id == other.Id && Customer.Equals(other.Customer) && FoodSeller.Equals(other.FoodSeller) && Date.Equals(other.Date) && Content == other.Content;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Comment)obj);
    }

    public override string ToString()
    {
        return $"{Id} {Customer.ToString()} {FoodSeller.ToString()} {Date.ToString()} {Content}";
    }
}