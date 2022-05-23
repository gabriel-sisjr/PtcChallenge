using AutoMapper;
using Data.Entities;
using Domain.AuxModels;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.Services.Auxs;
using Domain.Models;
using Moq;
using Services.Services;
using Xunit;

namespace Tests
{
    public class OwnerServiceTest
    {
        private readonly Mock<IOwnerService> _ownerServiceMock;
        private readonly IOwnerService _ownerService;

        private readonly OwnerModel _ownerModel;
        private readonly Owner _entity;

        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IOwnerRepository<Owner>> _ownerMockRepo;
        public OwnerServiceTest()
        {
            #region Objects
            _ownerModel = new OwnerModel
            {
                Cep = "49045280",
                Document = "35024690",
                Email = "teste@teste.com",
                Id = 1,
                Name = "Test",
                Status = StatusOwner.ACTIVE
            };
            var address = new Address
            {
                Cep = "49045280",
                Street = "Test",
                City = "Test",
                Neighborhood = "Test",
                Service = "Test",
                State = "Test"
            };
            _entity = new Owner
            {
                Address = address,
                Document = "35024690",
                Email = "teste@teste.com",
                Id = 1,
                Name = "Test",
                Status = StatusOwner.ACTIVE
            };
            #endregion

            #region Mocks Instances
            _ownerServiceMock = new Mock<IOwnerService>();
            _ownerMockRepo = new Mock<IOwnerRepository<Owner>>();
            _mockMapper = new Mock<IMapper>();
            var addressMockService = new Mock<IAddressService>();
            #endregion

            #region Setups
            _mockMapper.Setup(m => m.Map<Owner>(_ownerModel)).Returns(_entity);
            _mockMapper.Setup(m => m.Map<Owner, OwnerModel>(_entity)).Returns(_ownerModel);
            addressMockService.Setup(x => x.GetAddressByCep("49045280").Result).Returns(address);
            #endregion

            _ownerService = new OwnerService(_ownerMockRepo.Object, _mockMapper.Object, addressMockService.Object);
        }

        [Fact]
        public async Task OwnerShouldBeAdded()
        {
            _ownerServiceMock.Setup(x => x.InsertAsync(_ownerModel).Result).Returns(true);
            _ownerMockRepo.Setup(x => x.InsertAsync(_entity).Result).Returns(_entity);

            var actual = await _ownerServiceMock.Object.InsertAsync(_ownerModel);
            var result = await _ownerService.InsertAsync(_ownerModel);

            Assert.Equal(result, actual);
        }

        [Fact]
        public async Task OwnerShouldBeUpdated()
        {
            _entity.Name = "New Name";
            _entity.Email = "New Email";
            _ownerModel.Name = "New Name";
            _ownerModel.Email = "New Email";

            _ownerServiceMock.Setup(x => x.UpdateAsync(_ownerModel).Result).Returns(true);
            _ownerMockRepo.Setup(x => x.UpdateAsync(_entity).Result).Returns(_entity);

            var actual = await _ownerServiceMock.Object.UpdateAsync(_ownerModel);
            var result = await _ownerService.UpdateAsync(_ownerModel);

            Assert.Equal(result, actual);
        }

        [Fact]
        public void GetAllOwners()
        {
            IEnumerable<OwnerModel> ownersModel = new List<OwnerModel>() { _ownerModel };
            var owners = new List<Owner>() { _entity };
            var actual = _ownerServiceMock.Setup(x => x.Get().Result).Returns(ownersModel);
            var result = _ownerMockRepo.Setup(x => x.GetAll().Result).Returns(owners);

            Assert.Collection(ownersModel, owner => Assert.Equal(owner, _ownerModel));
            Assert.Collection(owners, owner => Assert.Equal(owner, _entity));
        }

        [Fact]
        public async Task GetOwnerById()
        {
            _ownerServiceMock.Setup(x => x.GetByIdAsync(1).Result).Returns(_ownerModel);

            var mapper = _mockMapper.Object.Map<Owner, OwnerModel>(_entity, opt => opt.AfterMap((src, dest) => dest.Cep = src.Address.Cep));
            var result = await _ownerService.GetByIdAsync(1);

            Assert.Equal(result, mapper);
        }

    }
}