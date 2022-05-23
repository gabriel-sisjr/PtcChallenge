using AutoMapper;
using Data.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.Services.Auxs;
using Domain.Models;

namespace Services.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository<Owner> _ownerRepository;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;
        public OwnerService(IOwnerRepository<Owner> ownerRepository, IMapper mapper, IAddressService addressService)
        {
            _ownerRepository = ownerRepository;
            _addressService = addressService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OwnerModel>> Get()
        {
            var result = await _ownerRepository.GetAll();
            var mappedList = result.Select(i => _mapper.Map<Owner, OwnerModel>(i, opt => opt.AfterMap((src, dest) => dest.Cep = src.Address.Cep)));
            return mappedList;
        }

        public async Task<bool> InsertAsync(OwnerModel model)
        {
            try
            {
                var address = await _addressService.GetAddressByCep(model.Cep);
                if (address == null) return false;

                var mapped = _mapper.Map<Owner>(model);
                mapped.Address = address!;
                mapped.Status = StatusOwner.ACTIVE;
                var inserted = await _ownerRepository.InsertAsync(mapped);

                return inserted != null;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ChangeStatusAsync(int id)
        {
            var entity = await _ownerRepository.GetById(id);
            if (entity == null) return false;

            var statusBool = entity.Status == StatusOwner.ACTIVE;
            var actualStatus = Convert.ToInt16(!statusBool);
            entity.Status = (StatusOwner)actualStatus;

            await _ownerRepository.UpdateAsync(entity);

            var updatedStatus = Convert.ToBoolean(entity.Status);
            return updatedStatus != statusBool;
        }

        public async Task<bool> UpdateAsync(OwnerModel model)
        {
            var address = await _addressService.GetAddressByCep(model.Cep);
            if (address == null) return false;

            var mapped = _mapper.Map<Owner>(model);
            mapped.Address = address!;
            var inserted = await _ownerRepository.UpdateAsync(mapped);

            return inserted != null;
        }

        public async Task<OwnerModel> GetByIdAsync(int id)
        {
            var entity = await _ownerRepository.GetById(id);
            var model = _mapper.Map<Owner, OwnerModel>(entity, opt => opt.AfterMap((src, dest) => dest.Cep = src.Address.Cep));
            return model;
        }

        public async Task<IEnumerable<OwnerModel>> GetAllOwnersAvailable()
        {
            var result = await _ownerRepository.GetAllOwnersAvailable();
            var mappedList = result.Select(i => _mapper.Map<Owner, OwnerModel>(i, opt => opt.AfterMap((src, dest) => dest.Cep = src.Address.Cep)));
            return mappedList;
        }

        public async Task<bool> CheckIfOwnerIsAvailable(int ownerId) => await _ownerRepository.CheckIfOwnerIsAvailable(ownerId);
    }
}
