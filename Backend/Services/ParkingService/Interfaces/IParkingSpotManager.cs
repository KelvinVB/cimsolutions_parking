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
        Task<List<ParkingSpot>> GetAllParkingSpots(int parkingGarageID);
        Task<ParkingSpot> UpdateParkingSpot(int id, ParkingSpot parkingSpot);
        Task<ParkingSpot> DeleteParkingSpot(int parkingSpotID);
        Task<int> GetAmountFreeParkingSpots(int id, DateTime startDate, DateTime endDate);
        Task<ParkingSpot> GetFreeParkingSpot(DateTime startDate, DateTime endDate);
    }
}
