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
    public class ParkingSpotsController : ControllerBase
    {
        private IParkingSpotManager parkingSpotManager;

        public ParkingSpotsController(IParkingSpotManager parkingSpotManager, ParkingContext context)
        {
            this.parkingSpotManager = parkingSpotManager;
            parkingSpotManager.SetContext(context);
        }

        /// <summary>
        /// Get all parking spots
        /// </summary>
        /// <returns>List of ParkingSpot</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingSpots()
        {
            throw new NotImplementedException();
        }

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
    }
}
