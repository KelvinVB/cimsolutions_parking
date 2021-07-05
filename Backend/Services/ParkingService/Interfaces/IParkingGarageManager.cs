using ParkingService.Context;
using ParkingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Interfaces
{
    public interface IParkingGarageManager
    {
        void SetContext(ParkingContext context);
        Task<ParkingGarage> CreateParkingGarage(ParkingGarage parkingGarage);
        Task<ParkingGarage> GetParkingGarage(int parkingGarageID);
        Task<List<ParkingGarage>> GetAllParkingGarages();
        Task<ParkingGarage> UpdateParkingGarage(int id, ParkingGarage parkingGarage);
        Task<ParkingGarage> DeleteParkingGarage(int parkingGarageID);
    }
}
