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
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingGaragesController : ControllerBase
    {
        private readonly ParkingContext _context;

        public ParkingGaragesController(ParkingContext context)
        {
            _context = context;
        }

        // GET: api/ParkingGarages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingGarage>>> GetParkingGarages()
        {
            return await _context.ParkingGarages.ToListAsync();
        }

        // GET: api/ParkingGarages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingGarage>> GetParkingGarage(int id)
        {
            var parkingGarage = await _context.ParkingGarages.FindAsync(id);

            if (parkingGarage == null)
            {
                return NotFound();
            }

            return parkingGarage;
        }

        // PUT: api/ParkingGarages/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingGarage(int id, ParkingGarage parkingGarage)
        {
            if (id != parkingGarage.parkingGarageID)
            {
                return BadRequest();
            }

            _context.Entry(parkingGarage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingGarageExists(id))
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

        // POST: api/ParkingGarages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ParkingGarage>> PostParkingGarage(ParkingGarage parkingGarage)
        {
            _context.ParkingGarages.Add(parkingGarage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParkingGarage", new { id = parkingGarage.parkingGarageID }, parkingGarage);
        }

        // DELETE: api/ParkingGarages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ParkingGarage>> DeleteParkingGarage(int id)
        {
            var parkingGarage = await _context.ParkingGarages.FindAsync(id);
            if (parkingGarage == null)
            {
                return NotFound();
            }

            _context.ParkingGarages.Remove(parkingGarage);
            await _context.SaveChangesAsync();

            return parkingGarage;
        }

        private bool ParkingGarageExists(int id)
        {
            return _context.ParkingGarages.Any(e => e.parkingGarageID == id);
        }
    }
}
