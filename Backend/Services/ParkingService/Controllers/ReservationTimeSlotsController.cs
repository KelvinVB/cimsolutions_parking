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
    public class ReservationTimeSlotsController : ControllerBase
    {
        private readonly ParkingContext _context;

        public ReservationTimeSlotsController(ParkingContext context)
        {
            _context = context;
        }

        // GET: api/ReservationTimeSlots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationTimeSlot>>> GetreservationTimeSlots()
        {
            return await _context.reservationTimeSlots.ToListAsync();
        }

        // GET: api/ReservationTimeSlots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationTimeSlot>> GetReservationTimeSlot(int id)
        {
            var reservationTimeSlot = await _context.reservationTimeSlots.FindAsync(id);

            if (reservationTimeSlot == null)
            {
                return NotFound();
            }

            return reservationTimeSlot;
        }

        // PUT: api/ReservationTimeSlots/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservationTimeSlot(int id, ReservationTimeSlot reservationTimeSlot)
        {
            if (id != reservationTimeSlot.reservationTimeSlotID)
            {
                return BadRequest();
            }

            _context.Entry(reservationTimeSlot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationTimeSlotExists(id))
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

        // POST: api/ReservationTimeSlots
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ReservationTimeSlot>> PostReservationTimeSlot(ReservationTimeSlot reservationTimeSlot)
        {
            _context.reservationTimeSlots.Add(reservationTimeSlot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservationTimeSlot", new { id = reservationTimeSlot.reservationTimeSlotID }, reservationTimeSlot);
        }

        // DELETE: api/ReservationTimeSlots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReservationTimeSlot>> DeleteReservationTimeSlot(int id)
        {
            var reservationTimeSlot = await _context.reservationTimeSlots.FindAsync(id);
            if (reservationTimeSlot == null)
            {
                return NotFound();
            }

            _context.reservationTimeSlots.Remove(reservationTimeSlot);
            await _context.SaveChangesAsync();

            return reservationTimeSlot;
        }

        private bool ReservationTimeSlotExists(int id)
        {
            return _context.reservationTimeSlots.Any(e => e.reservationTimeSlotID == id);
        }
    }
}
