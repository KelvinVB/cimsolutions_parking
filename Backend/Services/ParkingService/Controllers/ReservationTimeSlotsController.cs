using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingService.Context;
using ParkingService.Interfaces;
using ParkingService.Models;

namespace ParkingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationTimeSlotsController : ControllerBase
    {
        private IReservationTimeSlotManager reservationTimeSlotManager;

        public ReservationTimeSlotsController(IReservationTimeSlotManager reservationTimeSlotManager, ParkingContext context)
        {
            this.reservationTimeSlotManager = reservationTimeSlotManager;
            reservationTimeSlotManager.SetContext(context);
        }

        // GET: api/ReservationTimeSlots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationTimeSlot>>> GetreservationTimeSlots()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get time slot of reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReservationTimeSlot</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationTimeSlot>> GetReservationTimeSlot(int id)
        {
            var reservationTimeSlot = await reservationTimeSlotManager.GetReservationTimeSlot(id);

            if (reservationTimeSlot == null)
            {
                return NotFound();
            }

            return reservationTimeSlot;
        }

        /// <summary>
        /// updates existing reservation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reservationTimeSlot"></param>
        /// <returns>ReservationTimeSlot</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ReservationTimeSlot>> PutReservationTimeSlot(int id, ReservationTimeSlot reservationTimeSlot)
        {
            if (id != reservationTimeSlot.reservationTimeSlotID)
            {
                return BadRequest();
            }

            ReservationTimeSlot reservation = await reservationTimeSlotManager.UpdateReservationTimeSlot(reservationTimeSlot);

            return reservation;
        }

        /// <summary>
        /// Create new reservation
        /// </summary>
        /// <param name="reservationTimeSlot"></param>
        /// <returns>ReservationTimeSLot</returns>
        [HttpPost]
        public async Task<ActionResult<ReservationTimeSlot>> PostReservationTimeSlot(ReservationTimeSlot reservationTimeSlot)
        {
            ReservationTimeSlot reservation = await reservationTimeSlotManager.CreateReservationTimeSlot(reservationTimeSlot);

            return reservation;
        }

        /// <summary>
        /// Deletes reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReservationTimeSlot</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReservationTimeSlot>> DeleteReservationTimeSlot(int id)
        {
            ReservationTimeSlot reservation = await reservationTimeSlotManager.DeleteReservationTimeSlot(id);

            return reservation;
        }
    }
}
