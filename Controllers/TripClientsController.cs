using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Trip.Models;

namespace Trip.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TripClientsController : ControllerBase
{
    private readonly TripContext _context;

    public TripClientsController(TripContext context)
    {
        _context = context;
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] ClientDto clientDto)
    {
        var trip = await _context.Trips.FindAsync(idTrip);
        if (trip == null || trip.DateFrom <= DateTime.Now)
            return BadRequest("Trip does not exist or has already occurred.");

        if (await _context.Clients.AnyAsync(c => c.Pesel == clientDto.Pesel))
            return BadRequest("Client with this PESEL already exists.");

        var client = new Client
        {
            FirstName = clientDto.FirstName,
            LastName = clientDto.LastName,
            Email = clientDto.Email,
            Telephone = clientDto.Telephone,
            Pesel = clientDto.Pesel,
            RegisteredAt = DateTime.Now
        };

        trip.Clients.Add(client);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
            return NotFound();

        return Ok(client);
    }
}

