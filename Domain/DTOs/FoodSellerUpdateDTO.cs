namespace Domain.DTOs;

public class FoodSellerUpdateDTO
{
    public int AccountId { get; set; }
    public string? Name { get; set; }
    public string? Street { get; set; }
    public int Number { get; set; }
    public string? City { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }


    public FoodSellerUpdateDTO(int accountId, string? name, string? street, int number, string? city  , string? email, string? password, string? phoneNumber)
    {
        AccountId = accountId;
        Name = name;
        Street = street;
        Number = number;
        City = city;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
    }
}