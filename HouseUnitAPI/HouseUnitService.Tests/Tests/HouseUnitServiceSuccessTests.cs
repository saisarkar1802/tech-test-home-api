using Xunit;
using Moq;
using HouseUnitAPI.Services;
using HouseUnitAPI.Repositories;
using HouseUnitAPI.Models;
using HouseUnitAPI.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using HouseUnitAPI.Mappers;
using static HouseUnitAPI.Helpers.EnumHelper;

namespace HouseUnitService.Tests.Tests
{
    public class HouseUnitServiceSuccessTests
    {
        private readonly HouseUnitAPI.Services.HouseUnitService _service;
        private readonly Mock<IHouseUnitRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<HouseUnitAPI.Services.HouseUnitService>> _loggerMock;

        public HouseUnitServiceSuccessTests()
        {
            _repositoryMock = new Mock<IHouseUnitRepository>();
            _loggerMock = new Mock<ILogger<HouseUnitAPI.Services.HouseUnitService>>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new HouseUnitMappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _service = new HouseUnitAPI.Services.HouseUnitService(_mapper, _repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllHouseUnits()
        {
            // Arrange
            var houseUnits = new List<HouseUnit> { new HouseUnit { Id = 1, UnitType = "Apartment" } };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(houseUnits);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsHouseUnit()
        {
            // Arrange
            var houseUnit = new HouseUnit { Id = 1, UnitType = "Apartment" };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(houseUnit);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Data.Id);
        }

        [Fact]
        public async Task AddAsync_ValidHouseUnit_ReturnsHouseUnit()
        {
            // Arrange
            var houseUnitDetails = new HouseUnitDetails { UnitType = HouseUnitType.Apartment };
            var houseUnit = new HouseUnit { Id = 1, UnitType = "Apartment" };

            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<HouseUnit>())).ReturnsAsync(houseUnit);

            // Act
            var result = await _service.AddAsync(houseUnitDetails);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Data.Id);
        }

        [Fact]
        public async Task UpdateAsync_ValidHouseUnit_ReturnsUpdatedHouseUnit()
        {
            // Arrange
            var houseUnitDetails = new HouseUnitDetails { UnitType = HouseUnitType.Apartment };
            var houseUnit = new HouseUnit { Id = 1, UnitType = "Apartment" };

            _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<HouseUnit>())).ReturnsAsync(houseUnit);

            // Act
            var result = await _service.UpdateAsync(houseUnitDetails, 1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Data.Id);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_ReturnsSuccess()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}

