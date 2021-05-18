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
                throw new Exception();
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
                ParkingGarage parkingGarage = await context.ParkingGarages.SingleAsync(p => p.parkingGarageID == parkingGarageID);
                if (parkingGarage == null)
                {
                    throw new NullReferenceException();
                }

                context.ParkingGarages.Remove(parkingGarage);
                context.SaveChanges();
                return parkingGarage;
            }
            catch (Exception)
            {
                throw new Exception();
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
                ParkingGarage parkingGarage = await context.ParkingGarages.FindAsync(parkingGarageID);

                return parkingGarage;
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Updates parking garage
        /// </summary>
        /// <param name="parkingGarage"></param>
        /// <returns>ParkingGarage</returns>
        public async Task<ParkingGarage> UpdateParkingGarage(ParkingGarage parkingGarage)
        {
            try
            {
                ParkingGarage oldparkingGarage = await context.ParkingGarages.SingleAsync(p => p.parkingGarageID == parkingGarage.parkingGarageID);
                if (oldparkingGarage == null)
                {
                    throw new NullReferenceException();
                }

                oldparkingGarage = parkingGarage;
                context.SaveChanges();
                return parkingGarage;
            }
            catch (Exception)
            {
                throw new Exception();
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
                throw new Exception();
            }
        }
    }
}
