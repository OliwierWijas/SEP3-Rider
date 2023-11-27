namespace Domain.DTOs;

public class ReadFoodSellerReservationDTO
{
    public int FoodOfferId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public MyDate StartPickUpTime { get; set; }
    public MyDate EndPickUpTime { get; set; }
    public int ReservationNumber { get; set; }
    public int CustomerId { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public bool IsCompleted { get; set; }


    public ReadFoodSellerReservationDTO(int foodOfferId, string title, string description, double price, MyDate startPickUpTime, MyDate endPickUpTime, int reservationNumber, int customerId, string customerFirstName, string customerLastName, bool isCompleted)
    {
        FoodOfferId = foodOfferId;
        Title = title;
        Description = description;
        Price = price;
        StartPickUpTime = startPickUpTime;
        EndPickUpTime = endPickUpTime;
        ReservationNumber = reservationNumber;
        CustomerId = customerId;
        CustomerFirstName = customerFirstName;
        CustomerLastName = customerLastName;
        IsCompleted = isCompleted;
    }
    
    
}