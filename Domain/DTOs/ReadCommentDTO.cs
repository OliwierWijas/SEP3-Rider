namespace Domain.DTOs;

public class ReadCommentDTO
{
    public int Id { get; set; }
    public int CustomerId{ get; set; }
    public int FoodSellerId{ get; set; }
    public string CustomerFirstName{ get; set; }
    public string CustomerLastName{ get; set; }
    public MyDate Date{ get; set; }
    public string Content { get; set; }

    public ReadCommentDTO(int id, int customerId, int foodSellerId, string customerFirstName, string customerLastName, MyDate date, string content)
    {
        Id = id;
        CustomerId = customerId;
        FoodSellerId = foodSellerId;
        CustomerFirstName = customerFirstName;
        CustomerLastName = customerLastName;
        Date = date;
        Content = content;
    }
}