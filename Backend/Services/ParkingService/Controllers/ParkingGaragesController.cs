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
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingGaragesController : ControllerBase
    {
        private readonly IParkingGarageManager parkingGarageManager;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParkingGarage>> GetParkingGarage(int id)
        {
            try
            {
                var parkingGarage = await parkingGarageManager.GetParkingGarage(id);

                if (parkingGarage == null)
                {
                    return NotFound();
                }

                return Ok(parkingGarage);
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParkingGarage>> PutParkingGarage(int id, [FromBody] ParkingGarage parkingGarage)
        {
            try
            {
                ParkingGarage updatedParkingGarage = await parkingGarageManager.UpdateParkingGarage(id, parkingGarage);
                if(updatedParkingGarage == null)
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

        /// <summary>
        /// Create new parking garage
        /// </summary>
        /// <param name="parkingGarage"></param>
        /// <returns>ParkingGarage</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
