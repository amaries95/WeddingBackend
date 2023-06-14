using Application.Contracts.Guest;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class GuestController : Controller
{
    private readonly GuestsService _guestsService;

    public GuestController(GuestsService guestsService)
    {
        this._guestsService = guestsService;
    }

    [HttpPost("/newGuest")]
    public async Task<IActionResult> AddNewGuest([FromBody] GuestRequest guestRequest)
    {
        var guestId = await _guestsService.AddNewGuest(guestRequest);

        return Ok(guestId);
    }

    [Authorize]
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllGuests()
    {
        var guests = await _guestsService.GetAllGuests();

        return Ok(guests);
    }

}