namespace Application.Contracts;

public class GuestRequest
{
    public string Name { get; set; }

    public int NumberOfGuests { get; set; }

    public bool IsVegetarian { get; set; }

    public string OtherDetails { get; set; }
}
