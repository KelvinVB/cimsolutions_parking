using ParkingService.Context;
using ParkingService.Interfaces;
using ParkingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Managers
{
    public class ParkingSpotManager : IParkingSpotManager
    {
        private ParkingContext context;
        public void SetContext(ParkingContext context)
        {
            this.context = context;
        }
        public Task<ParkingSpot> CreateParkingSpot(ParkingSpot parkingSpot)
        {
            throw new NotImplementedException();
        }

        public Task<ParkingSpot> DeleteParkingSpot(int parkingSpotID)
        {
            throw new NotImplementedException();
        }

        public Task<ParkingSpot> GetParkingSpot(int parkingSpotID)
        {
            throw new NotImplementedException();
        }

        public Task<ParkingSpot> UpdateParkingSpot(ParkingSpot parkingSpot)
        {
            throw new NotImplementedException();
        }
    }
}
