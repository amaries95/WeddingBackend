﻿namespace Application.Contracts.Guest;

public class GuestRequest
{
    public string Name { get; set; }

    public int NumberOfGuests { get; set; }

    public bool IsComing { get; set; }

    public string TypesOfMenu { get; set; }

    public string OtherDetails { get; set; }
}
