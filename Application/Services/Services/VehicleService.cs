using AutoMapper;
using Data.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.Services.Auxs;
using Domain.Models;

namespace Services.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository<Vehicle> _vehicleRepository;
        private readonly IOwnerService _ownerService;
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;
        private readonly IQueueManager _queueManager;

        public VehicleService(IVehicleRepository<Vehicle> vehicleRepository, IOwnerService ownerRepository, IBrandService brandRepository, IMapper mapper, IQueueManager queueManager)
        {
            _vehicleRepository = vehicleRepository;
            _ownerService = ownerRepository;
            _brandService = brandRepository;
            _mapper = mapper;
            _queueManager = queueManager;
        }

        public async Task<bool> ChangeStatusAsync(int id, StatusVehicle status)
        {
            var vehicle = await _vehicleRepository.GetById(id);
            if (vehicle == null) return false;
            vehicle.Status = status;
            await _vehicleRepository.UpdateAsync(vehicle);
            return true;
        }

        public async Task<IEnumerable<VehicleModel>> Get()
        {
            var entities = await _vehicleRepository.GetAll();
            var result = entities.Select(i => _mapper.Map<VehicleModel>(i));
            return result;
        }

        public async Task<VehicleModel> GetByIdAsync(int id)
        {
            var entity = await _vehicleRepository.GetById(id);
            return _mapper.Map<VehicleModel>(entity);
        }

        public async Task<bool> InsertAsync(VehicleModel vehicle)
        {
            try
            {
                if (!await _ownerService.CheckIfOwnerIsAvailable(vehicle.Owner.Id) || !await _brandService.CheckIfBrandIsAvailable(vehicle.Brand.Id)) return false;
                var mapped = _mapper.Map<Vehicle>(vehicle);
                mapped.Status = StatusVehicle.AVAILABLE;

                var inserted = await _vehicleRepository.InsertAsync(mapped);
                _queueManager.SendMessage(inserted, QueueName.EMAIL);
                return inserted != null;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(VehicleModel vehicle)
        {
            var mapped = _mapper.Map<Vehicle>(vehicle);
            var updated = await _vehicleRepository.UpdateAsync(mapped);
            return updated != null;
        }
    }
}
