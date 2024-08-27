using AutoMapper;
using HouseUnitAPI.Mappers;
using HouseUnitAPI.Models;
using HouseUnitAPI.Repositories;
using HouseUnitAPI.ViewModels;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HouseUnitAPI.Helpers.EnumHelper;

namespace HouseUnitService.Tests.Tests
{
    public class HouseUnitServicePerformanceTests
    {
        private readonly HouseUnitAPI.Services.HouseUnitService _service;
        private readonly Mock<IHouseUnitRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<HouseUnitAPI.Services.HouseUnitService>> _loggerMock;

        public HouseUnitServicePerformanceTests()
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
        public async Task GetAllAsync_Performance_ShouldCompleteWithinExpectedTime()
        {
            // Arrange
            var houseUnits = Enumerable.Range(1, 1000).Select(i => new HouseUnit { Id = i, UnitType = "Apartment" }).ToList();
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(houseUnits);

            var stopwatch = Stopwatch.StartNew();

            // Act
            var result = await _service.GetAllAsync();

            stopwatch.Stop();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(stopwatch.ElapsedMilliseconds < 1000); // Adjust as needed
        }

        [Fact]
        public async Task AddAsync_Performance_ShouldCompleteWithinExpectedTime()
        {
            // Arrange
            var houseUnitDetails = new HouseUnitDetails { UnitType = HouseUnitType.Apartment };
            var houseUnit = new HouseUnit { Id = 1, UnitType = "Apartment" };

            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<HouseUnit>())).ReturnsAsync(houseUnit);

            var stopwatch = Stopwatch.StartNew();

            // Act
            var result = await _service.AddAsync(houseUnitDetails);

            stopwatch.Stop();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(stopwatch.ElapsedMilliseconds < 1000); // Adjust as needed
        }

        [Fact]
        public async Task UpdateAsync_Performance_ShouldCompleteWithinExpectedTime()
        {
            // Arrange
            var houseUnitDetails = new HouseUnitDetails { UnitType = HouseUnitType.Apartment };
            var houseUnit = new HouseUnit { Id = 1, UnitType = "Apartment" };

            _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<HouseUnit>())).ReturnsAsync(houseUnit);

            var stopwatch = Stopwatch.StartNew();

            // Act
            var result = await _service.UpdateAsync(houseUnitDetails, 1);

            stopwatch.Stop();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(stopwatch.ElapsedMilliseconds < 1000); // Adjust as needed
        }

        [Fact]
        public async Task DeleteAsync_Performance_ShouldCompleteWithinExpectedTime()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            var stopwatch = Stopwatch.StartNew();

            // Act
            var result = await _service.DeleteAsync(1);

            stopwatch.Stop();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(stopwatch.ElapsedMilliseconds < 1000); // Adjust as needed
        }
    }
}
