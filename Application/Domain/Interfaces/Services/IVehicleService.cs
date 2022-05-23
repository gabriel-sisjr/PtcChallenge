using Domain.Enums;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<bool> InsertAsync(VehicleModel vehicle);
        Task<bool> UpdateAsync(VehicleModel vehicle);
        Task<IEnumerable<VehicleModel>> Get();
        Task<bool> ChangeStatusAsync(int id, StatusVehicle status);
        Task<VehicleModel> GetByIdAsync(int id);
    }
}
