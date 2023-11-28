namespace Domain.DTOs;

public class ReadFoodSellerDTO
{
    public int AccountId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public ReadFoodSellerDTO(int accountId, string email, string phoneNumber, string name, string address)
    {
        AccountId = accountId;
        Email = email;
        PhoneNumber = phoneNumber;
        Name = name;
        Address = address;
    }
}