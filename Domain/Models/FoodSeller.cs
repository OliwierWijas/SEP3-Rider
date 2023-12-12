namespace Domain;

public class FoodSeller
{
    public int AccountId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public FoodSeller(int accountId, string email, string phoneNumber, string name, string address)
    {
        AccountId = accountId;
        Email = email;
        PhoneNumber = phoneNumber;
        Name = name;
        Address = address;
    }

    protected bool Equals(FoodSeller other)
    {
        return AccountId == other.AccountId && Email == other.Email && PhoneNumber == other.PhoneNumber && Name == other.Name && Address == other.Address;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((FoodSeller)obj);
    }

    public override string ToString()
    {
        return $"{AccountId} {Email} {PhoneNumber} {Name} {Address}";
    }
}