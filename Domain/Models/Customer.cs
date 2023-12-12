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
        return $"{FirstName} {LastName} {Email} {PhoneNumber} {Password}";
    }

    protected bool Equals(Customer other)
    {
        return FirstName == other.FirstName && LastName == other.LastName && Email == other.Email && PhoneNumber == other.PhoneNumber && Password == other.Password;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Customer)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName, Email, PhoneNumber, Password);
    }
}