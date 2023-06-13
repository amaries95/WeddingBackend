using Application.Contracts;
using Application.Data;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class GuestsService
{
    private readonly WeddingContext _context;

    public GuestsService(WeddingContext context)
    {
        _context = context;
    }

    public async Task<List<GuestResponse>> GetAllGuests()
    {
        var guests = await _context.Guests.ToListAsync();

        return guests.Select(MapGuestToGuestResponse).ToList();
    }

    public async Task<int> AddNewGuest(GuestRequest guestRequest)
    {
        var guest = MapGuestRequestToGuest(guestRequest);

        await _context.Guests.AddAsync(guest);
        await _context.SaveChangesAsync();

        return guest.Id;
    }

    private Guest MapGuestRequestToGuest(GuestRequest guestRequest)
    {
        return new Guest
        {
            IsVegetarian = guestRequest.IsVegetarian,
            Name = guestRequest.Name,
            NumberOfGuests = guestRequest.NumberOfGuests,
            OtherDetails = guestRequest.OtherDetails
        };
    }

    private GuestResponse MapGuestToGuestResponse(Guest guest)
    {
        return new GuestResponse
        {
            Id = guest.Id,
            Name = guest.Name,
            IsVegetarian = guest.IsVegetarian,
            OtherDetails = guest.OtherDetails,
            NumberOfGuests = guest.NumberOfGuests
        };
    }
}