using Data.Entities;
using Data.Entities.Context;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BrandRepository : IBrandRepository<Brand>
    {
        private readonly ContextDB _context;
        private readonly DbSet<Brand> _dbSet;
        public BrandRepository(ContextDB context)
        {
            _context = context;
            _dbSet = context.Set<Brand>();
        }

        public async Task<bool> CheckIfBrandIsAvailable(int brandId)
            => await _dbSet.AnyAsync(x => x.Id == brandId && x.Status == StatusBrand.ACTIVE);

        public async Task<List<Brand>> GetAll()
            => await _dbSet.AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<List<Brand>> GetAllBrandsAvailable()
            => await _dbSet.AsNoTrackingWithIdentityResolution().Where(x => x.Status == StatusBrand.ACTIVE).ToListAsync();

        public async Task<Brand> GetById(int id)
            => await _dbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Brand> InsertAsync(Brand entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Brand> UpdateAsync(Brand entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
