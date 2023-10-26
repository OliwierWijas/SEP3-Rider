namespace Domain.DTOs;

public class CustomerCreationDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public CustomerCreationDTO(string firstName, string lastName, string email, string phoneNumber, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }
}