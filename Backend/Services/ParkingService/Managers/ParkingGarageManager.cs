using Microsoft.EntityFrameworkCore;
using ParkingService.Context;
using ParkingService.Interfaces;
using ParkingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Managers
{
    public class ParkingGarageManager : IParkingGarageManager
    {
        private ParkingContext context;

        public void SetContext(ParkingContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// creates new parking garage
        /// </summary>
        /// <param name="parkingGarage"></param>
        /// <returns>ParkingGarage</returns>
        public async Task<ParkingGarage> CreateParkingGarage(ParkingGarage parkingGarage)
        {
            try
            {
                await context.ParkingGarages.AddAsync(parkingGarage);
                context.SaveChanges();
                return parkingGarage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// deletes parking garage
        /// </summary>
        /// <param name="parkingGarageID"></param>
        /// <returns>ParkingGarage</returns>
        public async Task<ParkingGarage> DeleteParkingGarage(int parkingGarageID)
        {
            try
            {
                ParkingGarage parkingGarage = await context.ParkingGarages.Where(p => p.parkingGarageID == parkingGarageID).FirstOrDefaultAsync();
                if (parkingGarage == null)
                {
                    return null;
                }

                context.ParkingGarages.Remove(parkingGarage);
                context.SaveChanges();
                return parkingGarage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get parking garage with id
        /// </summary>
        /// <param name="parkingGarageID"></param>
        /// <returns>ParkingGarage</returns>
        public async Task<ParkingGarage> GetParkingGarage(int parkingGarageID)
        {
            try
            {
                ParkingGarage parkingGarage = await context.ParkingGarages.Where(p=> p.parkingGarageID == parkingGarageID).FirstOrDefaultAsync();
                
                return parkingGarage;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates parking garage
        /// </summary>
        /// <param name="parkingGarage"></param>
        /// <returns>ParkingGarage</returns>
        public async Task<ParkingGarage> UpdateParkingGarage(int id, ParkingGarage parkingGarage)
        {
            try
            {
                ParkingGarage oldparkingGarage = await context.ParkingGarages.Where(p => p.parkingGarageID == id).FirstOrDefaultAsync();
                if (oldparkingGarage == null)
                {
                    return null;
                }

                oldparkingGarage = parkingGarage;
                context.ParkingGarages.Update(oldparkingGarage);
                context.SaveChanges();
                return oldparkingGarage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ParkingGarage>> GetAllParkingGarages()
        {
            try
            {
                List<ParkingGarage> parkingGarage = await context.ParkingGarages.ToListAsync();
                return parkingGarage;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
