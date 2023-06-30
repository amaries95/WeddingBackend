namespace Domain.Model;

public class Guest
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int NumberOfGuests { get; set; }

    public bool IsComing { get; set; }

    public int NumberOfVeggiesMenus { get; set; }

    public string OtherDetails { get; set; }
}
