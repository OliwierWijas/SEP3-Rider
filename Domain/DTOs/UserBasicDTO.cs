namespace Domain.DTOs;

public class UserBasicDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Type { get; set; }

    public UserBasicDTO(int id, string email, string password, string phoneNumber, string? address, string? name, string? firstName, string? lastName, string type)
    {
        Id = id;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
        Address = address;
        Name = name;
        FirstName = firstName;
        LastName = lastName;
        Type = type;
    }
}