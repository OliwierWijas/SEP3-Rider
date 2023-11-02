namespace Domain.DTOs;

public class FoodSellerCreationDTO
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public FoodSellerCreationDTO(string name, string address, string email, string phoneNumber, string password)
    {
        Name = name;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }
}