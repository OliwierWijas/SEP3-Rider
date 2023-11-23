namespace Domain.DTOs;

public class ReadCustomerReservationDTO
{
    public int FoodOfferId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public MyDate StartPickUpTime { get; set; }
    public MyDate EndPickUpTime { get; set; }
    public int ReservationNumber { get; set; }
    public int FoodSellerId { get; set; }
    public string FoodSellerName { get; set; }
    public string Address { get; set; }
    public bool IsCompleted { get; set; }

    public ReadCustomerReservationDTO(int foodOfferId, string title, string description, double price, MyDate startPickUpTime, MyDate endPickUpTime, int reservationNumber, int foodSellerId, string foodSellerName, string address, bool isCompleted)
    {
        FoodOfferId = foodOfferId;
        Title = title;
        Description = description;
        Price = price;
        StartPickUpTime = startPickUpTime;
        EndPickUpTime = endPickUpTime;
        ReservationNumber = reservationNumber;
        FoodSellerId = foodSellerId;
        FoodSellerName = foodSellerName;
        Address = address;
        IsCompleted = isCompleted;
    }
}