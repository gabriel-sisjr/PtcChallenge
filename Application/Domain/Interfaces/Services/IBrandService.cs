using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IBrandService
    {
        Task<bool> InsertAsync(BrandModel model);
        Task<bool> ChangeStatusAsync(int id);
        Task<IEnumerable<BrandModel>> Get();
        Task<IEnumerable<BrandModel>> GetAllBrandsAvailable();
        Task<bool> CheckIfBrandIsAvailable(int brandId);
        Task<BrandModel> GetByIdAsync(int brandId);
    }
}
