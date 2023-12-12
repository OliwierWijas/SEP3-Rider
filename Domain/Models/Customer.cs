namespace Domain;

public class Customer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public Customer( string firstName, string lastName, string email, string phoneNumber, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }

    public override string ToString()
    {
        return $"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Email)}: {Email}, {nameof(PhoneNumber)}: {PhoneNumber}, {nameof(Password)}: {Password}";
    }
}