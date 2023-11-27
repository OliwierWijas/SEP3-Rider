namespace Domain.DTOs;

public class CommentBasicDTO
{
    public int FoodSellerId { get; set; }
    public int CustomerId { get; set; }
    public string Comment { get; set; }

    public CommentBasicDTO(int foodSellerId, int customerId, string comment)
    {
        FoodSellerId = foodSellerId;
        CustomerId = customerId;
        Comment = comment;
    }
}