using Data.Entities;
using Data.Entities.Context;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class VehicleRepository : IVehicleRepository<Vehicle>
    {
        private readonly ContextDB _context;
        private readonly DbSet<Vehicle> _dbSet;

        private readonly DbSet<Owner> _ownerDbSet;
        private readonly DbSet<Brand> _brandDbSet;
        public VehicleRepository(ContextDB context)
        {
            _context = context;
            _dbSet = context.Set<Vehicle>();
            _ownerDbSet = context.Set<Owner>();
            _brandDbSet = context.Set<Brand>();
        }

        public async Task<List<Vehicle>> GetAll()
            => await _dbSet.AsNoTrackingWithIdentityResolution().Include(x => x.Owner).Include(x => x.Brand).ToListAsync();

        public async Task<Vehicle> GetById(int id)
            => await _dbSet.AsNoTrackingWithIdentityResolution().Include(x => x.Owner).Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Vehicle> InsertAsync(Vehicle entity)
        {
            entity = await AddBrandAndOwner(entity);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Vehicle> UpdateAsync(Vehicle entity)
        {
            entity = await AddBrandAndOwner(entity);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        private async Task<Vehicle> AddBrandAndOwner(Vehicle vehicle)
        {
            vehicle.Brand = await _brandDbSet.FirstOrDefaultAsync(x => x.Id == vehicle.BrandId);
            vehicle.Owner = await _ownerDbSet.FirstOrDefaultAsync(x => x.Id == vehicle.OwnerId);

            return vehicle;
        }
    }
}
