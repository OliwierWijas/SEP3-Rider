namespace Domain;

public class MyDate
{
    public int year { get; set; }
    public int month { get; set; }
    public int day { get; set; }
    public int hour { get; set; }
    public int minute { get; set; }

    public MyDate(int year, int month, int day, int hour, int minute)
    {
        this.year = year;
        this.month = month;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    public string ToString()
    {
        return $"{day}/{month}/{year} {hour}:{minute}";
    }

    public string ToStringWithOutHourAndMinutes()
    {
        return $"{day}/{month}/{year}";
    }

    protected bool Equals(MyDate other)
    {
        return year == other.year && month == other.month && day == other.day && hour == other.hour && minute == other.minute;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((MyDate)obj);
    }
}