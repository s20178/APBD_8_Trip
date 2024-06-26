using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Trip.Models;

namespace Trip.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly TripContext _context;

    public TripsController(TripContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips(int page = 1, int pageSize = 10)
    {
        var trips = await _context.Trips
                                  .OrderByDescending(t => t.DateFrom)
                                  .Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .Include(t => t.Countries)
                                  .Include(t => t.Clients)
                                  .ToListAsync();

        var totalTrips = await _context.Trips.CountAsync();
        var allPages = (int)Math.Ceiling(totalTrips / (double)pageSize);

        var result = new
        {
            pageNum = page,
            pageSize,
            allPages,
            trips
        };

        return Ok(result);
    }
}

