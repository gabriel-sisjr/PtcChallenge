using AutoMapper;
using Data.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Services.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;
        public BrandService(IBrandRepository<Brand> ownerRepository, IMapper mapper)
        {
            _brandRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<bool> ChangeStatusAsync(int id)
        {
            var entity = await _brandRepository.GetById(id);
            if (entity == null) return false;

            var statusBool = entity.Status == StatusBrand.ACTIVE;
            var actualStatus = Convert.ToInt16(!statusBool);
            entity.Status = (StatusBrand)actualStatus;

            await _brandRepository.UpdateAsync(entity);

            var updatedStatus = Convert.ToBoolean(entity.Status);
            return updatedStatus != statusBool;
        }

        public async Task<bool> CheckIfBrandIsAvailable(int brandId) => await _brandRepository.CheckIfBrandIsAvailable(brandId);

        public async Task<IEnumerable<BrandModel>> Get()
        {
            var result = await _brandRepository.GetAll();
            return result.Select(i => _mapper.Map<BrandModel>(i));
        }

        public async Task<IEnumerable<BrandModel>> GetAllBrandsAvailable()
        {
            var result = await _brandRepository.GetAllBrandsAvailable();
            return result.Select(i => _mapper.Map<BrandModel>(i));
        }

        public async Task<BrandModel> GetByIdAsync(int brandId)
        {
            var entity = await _brandRepository.GetById(brandId);
            return _mapper.Map<BrandModel>(entity);
        }

        public async Task<bool> InsertAsync(BrandModel model)
        {
            try
            {
                var mapped = _mapper.Map<Brand>(model);
                mapped.Status = StatusBrand.ACTIVE;
                var inserted = await _brandRepository.InsertAsync(mapped);

                return inserted != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
