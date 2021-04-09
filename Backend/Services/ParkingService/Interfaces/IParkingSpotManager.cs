using ParkingService.Context;
using ParkingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Interfaces
{
    public interface IParkingSpotManager
    {
        void SetContext(ParkingContext context);
        Task<ParkingSpot> CreateParkingSpot(ParkingSpot parkingSpot);
        Task<ParkingSpot> GetParkingSpot(int parkingSpotID);
        Task<ParkingSpot> UpdateParkingSpot(ParkingSpot parkingSpot);
        Task<ParkingSpot> DeleteParkingSpot(int parkingSpotID);

        int GetAllFreeParkingSpots(DateTime startDate, DateTime endDate);
    }
}
