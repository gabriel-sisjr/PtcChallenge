namespace Domain.Interfaces.Repositories
{
    public interface IOwnerRepository<T> where T : class
    {
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<List<T>> GetAllOwnersAvailable();
        Task<bool> CheckIfOwnerIsAvailable(int ownerId);
    }
}
