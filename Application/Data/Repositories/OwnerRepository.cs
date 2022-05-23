using Data.Entities;
using Data.Entities.Context;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class OwnerRepository : IOwnerRepository<Owner>
    {
        private readonly ContextDB _context;
        private readonly DbSet<Owner> _dbSet;
        public OwnerRepository(ContextDB context)
        {
            _context = context;
            _dbSet = context.Set<Owner>();
        }

        public async Task<bool> CheckIfOwnerIsAvailable(int ownerId)
            => await _dbSet.AnyAsync(x => x.Id == ownerId && x.Status == StatusOwner.ACTIVE);

        public async Task<List<Owner>> GetAll()
            => await _dbSet.AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<List<Owner>> GetAllOwnersAvailable()
            => await _dbSet.AsNoTrackingWithIdentityResolution().Where(x => x.Status == StatusOwner.ACTIVE).ToListAsync();

        public async Task<Owner> GetById(int id)
            => await _dbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Owner> InsertAsync(Owner entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Owner> UpdateAsync(Owner entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
