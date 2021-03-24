using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingService.Context;
using ParkingService.Models;

namespace ParkingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpotsController : ControllerBase
    {
        private readonly ParkingContext _context;

        public ParkingSpotsController(ParkingContext context)
        {
            _context = context;
        }

        // GET: api/ParkingSpots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetparkingSpots()
        {
            return await _context.parkingSpots.ToListAsync();
        }

        // GET: api/ParkingSpots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpot>> GetParkingSpot(int id)
        {
            var parkingSpot = await _context.parkingSpots.FindAsync(id);

            if (parkingSpot == null)
            {
                return NotFound();
            }

            return parkingSpot;
        }

        // PUT: api/ParkingSpots/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingSpot(int id, ParkingSpot parkingSpot)
        {
            if (id != parkingSpot.parkingSpotID)
            {
                return BadRequest();
            }

            _context.Entry(parkingSpot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingSpotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ParkingSpots
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ParkingSpot>> PostParkingSpot(ParkingSpot parkingSpot)
        {
            _context.parkingSpots.Add(parkingSpot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParkingSpot", new { id = parkingSpot.parkingSpotID }, parkingSpot);
        }

        // DELETE: api/ParkingSpots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ParkingSpot>> DeleteParkingSpot(int id)
        {
            var parkingSpot = await _context.parkingSpots.FindAsync(id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            _context.parkingSpots.Remove(parkingSpot);
            await _context.SaveChangesAsync();

            return parkingSpot;
        }

        private bool ParkingSpotExists(int id)
        {
            return _context.parkingSpots.Any(e => e.parkingSpotID == id);
        }
    }
}
