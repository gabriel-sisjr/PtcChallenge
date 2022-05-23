using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IOwnerService
    {
        Task<bool> InsertAsync(OwnerModel model);
        Task<bool> UpdateAsync(OwnerModel model);
        Task<bool> ChangeStatusAsync(int id);
        Task<IEnumerable<OwnerModel>> Get();
        Task<OwnerModel> GetByIdAsync(int id);
        Task<IEnumerable<OwnerModel>> GetAllOwnersAvailable();
        Task<bool> CheckIfOwnerIsAvailable(int ownerId);
    }
}
