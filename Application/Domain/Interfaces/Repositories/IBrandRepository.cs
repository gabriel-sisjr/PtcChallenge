namespace Domain.Interfaces.Repositories
{
    public interface IBrandRepository<T> where T : class
    {
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<List<T>> GetAllBrandsAvailable();
        Task<bool> CheckIfBrandIsAvailable(int brandId);
    }
}
