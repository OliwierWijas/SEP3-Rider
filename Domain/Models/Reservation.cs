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
    public FoodSeller FoodSeller { get; set; }
    public Customer Customer { get; set; }
    public bool IsCompleted { get; set; }

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
}