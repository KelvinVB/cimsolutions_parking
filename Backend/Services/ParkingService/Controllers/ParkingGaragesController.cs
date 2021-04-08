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
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingGaragesController : ControllerBase
    {
        private IParkingGarageManager parkingGarageManager;

        public ParkingGaragesController(IParkingGarageManager parkingGarageManager)
        {
            this.parkingGarageManager = parkingGarageManager;
        }

        /// <summary>
        /// Get all parking garages
        /// </summary>
        /// <returns>List of ParkingGarage</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingGarage>>> GetParkingGarages()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get parking garage information with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ParkingGarage</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingGarage>> GetParkingGarage(int id)
        {
            var parkingGarage = await parkingGarageManager.GetParkingGarage(id);

            if (parkingGarage == null)
            {
                return NotFound();
            }

            return parkingGarage;
        }

        /// <summary>
        /// Updates parking garage information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parkingGarage"></param>
        /// <returns>ParkingGarage</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ParkingGarage>> PutParkingGarage(int id, ParkingGarage parkingGarage)
        {
            if (id != parkingGarage.parkingGarageID)
            {
                return BadRequest();
            }

            parkingGarage.parkingGarageID = id;

            await parkingGarageManager.UpdateParkingGarage(parkingGarage);

            return parkingGarage;
        }

        /// <summary>
        /// Create new parking garage
        /// </summary>
        /// <param name="parkingGarage"></param>
        /// <returns>ParkingGarage</returns>
        [HttpPost]
        public async Task<ActionResult<ParkingGarage>> PostParkingGarage(ParkingGarage parkingGarage)
        {
            ParkingGarage newParkingGarage = await parkingGarageManager.CreateParkingGarage(parkingGarage);

            return newParkingGarage;
        }

        /// <summary>
        /// Delete parking garage with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ParkingGarage</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ParkingGarage>> DeleteParkingGarage(int id)
        {
            ParkingGarage parkingGarage = await parkingGarageManager.DeleteParkingGarage(id);
            if (parkingGarage == null)
            {
                return NotFound();
            }

            return parkingGarage;
        }
    }
}
