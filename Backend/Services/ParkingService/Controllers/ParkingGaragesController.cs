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

        public ParkingGaragesController(IParkingGarageManager parkingGarageManager, ParkingContext context)
        {
            this.parkingGarageManager = parkingGarageManager;
            parkingGarageManager.SetContext(context);
        }

        /// <summary>
        /// Get all parking garages
        /// </summary>
        /// <returns>List of ParkingGarage</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingGarage>>> GetParkingGarages()
        {
            try
            {
                List<ParkingGarage> parkingGarage = await parkingGarageManager.GetAllParkingGarages();
                return Ok(parkingGarage);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get parking garage information with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ParkingGarage</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingGarage>> GetParkingGarage(int id)
        {
            try
            {
                var parkingGarage = await parkingGarageManager.GetParkingGarage(id);

                if (parkingGarage == null)
                {
                    return NotFound();
                }

                return parkingGarage;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates parking garage information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parkingGarage"></param>
        /// <returns>ParkingGarage</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ParkingGarage>> PutParkingGarage(int id, [FromBody] ParkingGarage parkingGarage)
        {
            if (id != parkingGarage.parkingGarageID)
            {
                return BadRequest();
            }

            try
            {
                await parkingGarageManager.UpdateParkingGarage(parkingGarage);

                return Ok(parkingGarage);
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
        /// Create new parking garage
        /// </summary>
        /// <param name="parkingGarage"></param>
        /// <returns>ParkingGarage</returns>
        [HttpPost]
        public async Task<ActionResult<ParkingGarage>> PostParkingGarage([FromBody] ParkingGarage parkingGarage)
        {
            try
            {
                ParkingGarage newParkingGarage = await parkingGarageManager.CreateParkingGarage(parkingGarage);

                return Ok(newParkingGarage);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete parking garage with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ParkingGarage</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ParkingGarage>> DeleteParkingGarage(int id)
        {
            try
            {
                ParkingGarage parkingGarage = await parkingGarageManager.DeleteParkingGarage(id);
                if (parkingGarage == null)
                {
                    return NotFound();
                }

                return Ok(parkingGarage);
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
