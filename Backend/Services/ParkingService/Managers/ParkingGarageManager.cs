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

        public Task<ParkingGarage> CreateParkingGarage(ParkingGarage parkingGarage)
        {
            throw new NotImplementedException();
        }

        public Task<ParkingGarage> DeleteParkingGarage(int parkingGarageID)
        {
            throw new NotImplementedException();
        }

        public async Task<ParkingGarage> GetParkingGarage(int parkingGarageID)
        {
            ParkingGarage parkingGarage = await context.ParkingGarages.FindAsync(parkingGarageID);

            return parkingGarage;
        }

        public Task<ParkingGarage> UpdateParkingGarage(ParkingGarage parkingGarage)
        {
            throw new NotImplementedException();
        }
    }
}
