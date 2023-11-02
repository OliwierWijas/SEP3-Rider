namespace Domain.DTOs;

public class FoodSellerUpdateDTO
{
    public int AccountId { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }


    public FoodSellerUpdateDTO(int accountId, string? name, string? address  , string? email, string? password, string? phoneNumber)
    {
        AccountId = accountId;
        Name = name;
        Address = address;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
    }
}