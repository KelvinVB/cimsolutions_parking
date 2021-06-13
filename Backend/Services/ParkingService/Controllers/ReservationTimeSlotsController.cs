using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IReservationTimeSlotManager reservationTimeSlotManager;
        private readonly IParkingSpotManager parkingSpotManager;

        public ReservationTimeSlotsController(IReservationTimeSlotManager reservationTimeSlotManager, IParkingSpotManager parkingSpotManager, ParkingContext context)
        {
            this.reservationTimeSlotManager = reservationTimeSlotManager;
            this.parkingSpotManager = parkingSpotManager;
            reservationTimeSlotManager.SetContext(context);
            parkingSpotManager.SetContext(context);
        }

        /// <summary>
        /// Gets all reservation for a parking spot
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of ReservationTimeSlot</returns>
        [HttpGet("all/{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationTimeSlot>> PutReservationTimeSlot(int id, [FromBody] ReservationTimeSlot reservationTimeSlot)
        {
            try
            {
                string accountID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                reservationTimeSlot.accountID = accountID;

                ParkingSpot parkingSpot = await parkingSpotManager.GetFreeParkingSpot(reservationTimeSlot.startReservation, reservationTimeSlot.endReservation);
                if(parkingSpot == null)
                {
                    return NotFound();
                }

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Gets all reservation for an user
        /// </summary>
        /// <returns>List of ReservationTimeSlot</returns>
        [HttpGet("list")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ReservationTimeSlot>>> GetUserReservationTimeSlots()
        {
            try
            {
                string accountID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                List<ReservationTimeSlot> reservationTimeSlot = await reservationTimeSlotManager.GetUserReservationTimeSlots(accountID);

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
    }
}
