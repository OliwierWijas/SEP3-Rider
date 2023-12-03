namespace Domain.DTOs;

public class FoodSellerCreationDTO
{
    public string Name { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public FoodSellerCreationDTO(string name, string street, int number, string city, string email, string phoneNumber, string password)
    {
        Name = name;
        Street = street;
        Number = number;
        City = city;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }
}