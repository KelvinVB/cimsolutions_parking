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
    public class ParkingSpotsController : ControllerBase
    {
        private readonly IParkingSpotManager parkingSpotManager;
        private readonly IReservationTimeSlotManager reservationTimeSlotManager;

        public ParkingSpotsController(IParkingSpotManager parkingSpotManager, IReservationTimeSlotManager reservationTimeSlotManager, ParkingContext context)
        {
            this.parkingSpotManager = parkingSpotManager;
            this.reservationTimeSlotManager = reservationTimeSlotManager;
            parkingSpotManager.SetContext(context);
            reservationTimeSlotManager.SetContext(context);
        }

        // <summary>
        // Get all parking spots in a parking garage
        // </summary>
        // <returns>List of ParkingSpot</returns>
        [HttpGet("all/{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingSpots(int id)
        {
            try
            {
                List<ParkingSpot> parkingSpots = await parkingSpotManager.GetAllParkingSpots(id);
                if (parkingSpots == null)
                {
                    return NotFound();
                }

                return Ok(parkingSpots);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get parking spot information with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ParkingSpot</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParkingSpot>> GetParkingSpot(int id)
        {
            try
            {
                var parkingSpot = await parkingSpotManager.GetParkingSpot(id);

                if (parkingSpot == null)
                {
                    return NotFound();
                }

                return Ok(parkingSpot);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates parking spot information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parkingSpot"></param>
        /// <returns>ParkingSpot</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParkingSpot>> PutParkingSpot(int id, [FromBody] ParkingSpot parkingSpot)
        {
            try
            {
                ParkingSpot updatedParkingSpot = await parkingSpotManager.UpdateParkingSpot(id, parkingSpot);
                if (updatedParkingSpot == null)
                {
                    return NotFound();
                }

                return Ok(updatedParkingSpot);
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
        /// Create new parking spot
        /// </summary>
        /// <param name="parkingSpot"></param>
        /// <returns>ParkingSpot</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ParkingSpot>> PostParkingSpot([FromBody] ParkingSpot parkingSpot)
        {
            try
            {
                ParkingSpot newParkingSpot = await parkingSpotManager.CreateParkingSpot(parkingSpot);

                return Ok(newParkingSpot);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete parking spot with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ParkingSpot</returns>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParkingSpot>> DeleteParkingSpot(int id)
        {
            try
            {
                ParkingSpot parkingSpot = await parkingSpotManager.DeleteParkingSpot(id);
                if (parkingSpot == null)
                {
                    return NotFound();
                }

                return Ok(parkingSpot);
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
        /// returns amount of free spots within a timeslot
        /// </summary>
        /// <param name="timeSlot"></param>
        /// <returns>ReservationTimeSlot</returns>
        [HttpPost("freespots")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationTimeSlot>> FreeSpots([FromBody] ReservationTimeSlot timeSlot)
        {
            try
            {
                int amount = await parkingSpotManager.GetAmountFreeParkingSpots(timeSlot.startReservation, timeSlot.endReservation);
                if (amount == -1)
                {
                    return NotFound();
                }

                return Ok(amount);
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
        /// Reserve a parking spot
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns>ReservationTimeSlot</returns>
        [HttpPost("reservation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationTimeSlot>> Reservation([FromBody] ReservationTimeSlot reservation)
        {
            string accountID = this.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountID == null)
            {
                return Unauthorized();
            }

            try
            {
                ParkingSpot parkingSpot = await parkingSpotManager.GetFreeParkingSpot(reservation.startReservation, reservation.endReservation);

                if (parkingSpot == null)
                {
                    return NotFound("No parking spots available");
                }
                reservation.parkingSpotID = parkingSpot.parkingSpotID;
                reservation.accountID = accountID;
                ReservationTimeSlot newReservation = await reservationTimeSlotManager.CreateReservationTimeSlot(reservation);
                return Ok(newReservation);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
