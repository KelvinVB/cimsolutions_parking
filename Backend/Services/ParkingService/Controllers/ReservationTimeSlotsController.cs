using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Gets all reservation for a parking spot
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of ReservationTimeSlot</returns>
        [HttpGet("all/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<List<ReservationTimeSlot>>> GetAllreservationTimeSlots(int id)
        {
            try
            {
                List<ReservationTimeSlot> reservationTimeSlot = await reservationTimeSlotManager.GetAllReservationTimeSlots(id);

                if (reservationTimeSlot == null)
                {
                    return NotFound();
                }

                return Ok(reservationTimeSlot);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// get time slot of reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReservationTimeSlot</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationTimeSlot>> GetReservationTimeSlot(int id)
        {
            try
            {
                var reservationTimeSlot = await reservationTimeSlotManager.GetReservationTimeSlot(id);

                if (reservationTimeSlot == null)
                {
                    return NotFound();
                }

                return Ok(reservationTimeSlot);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// updates existing reservation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reservationTimeSlot"></param>
        /// <returns>ReservationTimeSlot</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ReservationTimeSlot>> PutReservationTimeSlot(int id, [FromBody] ReservationTimeSlot reservationTimeSlot)
        {
            try
            {
                ReservationTimeSlot reservation = await reservationTimeSlotManager.UpdateReservationTimeSlot(id, reservationTimeSlot);

                if (reservation == null)
                {
                    return NotFound();
                }

                return Ok(reservation);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Create new reservation
        /// </summary>
        /// <param name="reservationTimeSlot"></param>
        /// <returns>ReservationTimeSLot</returns>
        [HttpPost]
        public async Task<ActionResult<ReservationTimeSlot>> PostReservationTimeSlot([FromBody] ReservationTimeSlot reservationTimeSlot)
        {
            try
            {
                ReservationTimeSlot reservation = await reservationTimeSlotManager.CreateReservationTimeSlot(reservationTimeSlot);

                if (reservation == null)
                {
                    return BadRequest();
                }

                return Ok(reservation);
            }
            catch (Exception)
            {
                return BadRequest();
            }
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

            try
            {
                if (reservation == null)
                {
                    return NotFound();
                }

                return Ok(reservation);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
