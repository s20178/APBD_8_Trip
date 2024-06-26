using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Trip.Models;

namespace Trip.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly TripContext _context;

    public ClientsController(TripContext context)
    {
        _context = context;
    }

    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        
        var client = await _context.Clients
                                   .Include(c => c.Trips)
                                   .FirstOrDefaultAsync(c => c.Id == idClient);

        if (client == null)
            return NotFound();

        if (client.Trips.Any())
            return BadRequest("Client has assigned trips. Cannot delete.");

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

