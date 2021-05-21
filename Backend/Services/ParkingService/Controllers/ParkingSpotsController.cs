using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class ParkingSpotsController : ControllerBase
    {
        private IParkingSpotManager parkingSpotManager;
        private IReservationTimeSlotManager reservationTimeSlotManager;

        public ParkingSpotsController(IParkingSpotManager parkingSpotManager, IReservationTimeSlotManager reservationTimeSlotManager, ParkingContext context)
        {
            this.parkingSpotManager = parkingSpotManager;
            this.reservationTimeSlotManager = reservationTimeSlotManager;
            parkingSpotManager.SetContext(context);
            reservationTimeSlotManager.SetContext(context);
        }

        /// <summary>
        /// Get all parking spots
        /// </summary>
        /// <returns>List of ParkingSpot</returns>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingSpots()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Get parking spot information with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ParkingSpot</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpot>> GetParkingSpot(int id)
        {
            var parkingSpot = await parkingSpotManager.GetParkingSpot(id);

            if (parkingSpot == null)
            {
                return NotFound();
            }

            return parkingSpot;
        }

        /// <summary>
        /// Updates parking spot information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parkingSpot"></param>
        /// <returns>ParkingSpot</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ParkingSpot>> PutParkingSpot(int id, ParkingSpot parkingSpot)
        {
            if (id != parkingSpot.parkingSpotID)
            {
                return BadRequest();
            }

            parkingSpot.parkingSpotID = id;

            await parkingSpotManager.UpdateParkingSpot(parkingSpot);

            return parkingSpot;
        }

        /// <summary>
        /// Create new parking spot
        /// </summary>
        /// <param name="parkingSpot"></param>
        /// <returns>ParkingSpot</returns>
        [HttpPost]
        public async Task<ActionResult<ParkingSpot>> PostParkingSpot(ParkingSpot parkingSpot)
        {
            ParkingSpot newParkingSpot = await parkingSpotManager.CreateParkingSpot(parkingSpot);

            return newParkingSpot;
        }

        /// <summary>
        /// Delete parking spot with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ParkingSpot</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ParkingSpot>> DeleteParkingSpot(int id)
        {
            ParkingSpot parkingSpot = await parkingSpotManager.DeleteParkingSpot(id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            return parkingSpot;
        }

        [HttpPost("freespots")]
        public async Task<ActionResult<ReservationTimeSlot>> FreeSpots([FromBody] TimeSlot timeSlot)
        {
            int amount = await parkingSpotManager.GetAmountFreeParkingSpots(timeSlot.startDateTime, timeSlot.endDateTime);

            return Ok(amount);
        }

        [HttpPost("reserve")]
        public async Task<ActionResult<ReservationTimeSlot>> Reserve([FromBody] ReservationTimeSlot reservation)
        {
            string accountID = this.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ParkingSpot parkingSpot = await parkingSpotManager.GetFreeParkingSpot(reservation.startReservation, reservation.endReservation);
            if(accountID == null)
            {
                return Unauthorized();
            }
            if (parkingSpot == null)
            {
                return BadRequest("No parking spots available");
            }
            else
            {
                reservation.parkingSpotID = parkingSpot.parkingSpotID;
                reservation.accountID = accountID;
                await reservationTimeSlotManager.CreateReservationTimeSlot(reservation);
                return Ok(parkingSpot);
            }
        }
    }
}
