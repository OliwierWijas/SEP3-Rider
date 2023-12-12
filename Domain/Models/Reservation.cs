namespace Domain.Models;

public class Reservation
{
    public int FoodOfferId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public MyDate StartPickUpTime { get; set; }
    public MyDate EndPickUpTime { get; set; }
    public int ReservationNumber { get; set; }
    public FoodSeller? FoodSeller { get; set; }
    public Customer? Customer { get; set; }
    public bool IsCompleted { get; set; }
    public string Photo { get; set; }

    public Reservation(int foodOfferId, string title, string description, double price, MyDate startPickUpTime, MyDate endPickUpTime, int reservationNumber, FoodSeller foodSeller, Customer customer, bool isCompleted)
    {
        FoodOfferId = foodOfferId;
        Title = title;
        Description = description;
        Price = price;
        StartPickUpTime = startPickUpTime;
        EndPickUpTime = endPickUpTime;
        ReservationNumber = reservationNumber;
        FoodSeller = foodSeller;
        Customer = customer;
        IsCompleted = isCompleted;
    }


    protected bool Equals(Reservation other)
    {
        if ((FoodSeller is not null && other.FoodSeller is null) ||
            (FoodSeller is null && other.FoodSeller is not null))
            return false;
        if ((Customer is not null && other.Customer is null) ||
            (Customer is null && other.Customer is not null))
            return false;
        bool customer = (Customer is null && other.Customer is null) || Customer.Equals(other.Customer);
        bool foodSeller = (FoodSeller is null && other.FoodSeller is null) || FoodSeller.Equals(other.FoodSeller);
        return FoodOfferId == other.FoodOfferId && Title == other.Title && Description == other.Description && Price.Equals(other.Price) && StartPickUpTime.Equals(other.StartPickUpTime) && EndPickUpTime.Equals(other.EndPickUpTime) && ReservationNumber == other.ReservationNumber && foodSeller && customer && IsCompleted == other.IsCompleted;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Reservation)obj);
    }
    
    public override string ToString()
    {
        string str =  $"{FoodOfferId} {Title} {Description} {Price} {StartPickUpTime.ToString()} {EndPickUpTime.ToString()} {ReservationNumber}";
        if (FoodSeller is not null)
            str += $" {FoodSeller.ToString()}";
        if (Customer is not null)
            str += $" {Customer.ToString()}";
        str += $" {IsCompleted}";
        return str;
    }
}