using AutoMapper;
using HouseUnitAPI.Mappers;
using HouseUnitAPI.Models;
using HouseUnitAPI.Repositories;
using HouseUnitAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static HouseUnitAPI.Helpers.EnumHelper;

namespace HouseUnitService.Tests.Tests
{
    public class HouseUnitServiceExceptionTests
    {
        private readonly HouseUnitAPI.Services.HouseUnitService _service;
        private readonly Mock<IHouseUnitRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<HouseUnitAPI.Services.HouseUnitService>> _loggerMock;

        public HouseUnitServiceExceptionTests()
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
        public async Task GetAllAsync_WhenExceptionThrown_ReturnsFailure()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new System.Exception("Database error"));

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.Error.StatusCode);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((HouseUnit)null);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.NotFound, result.Error.StatusCode);
        }

        [Fact]
        public async Task AddAsync_WhenDbUpdateExceptionThrown_ReturnsFailure()
        {
            // Arrange
            var houseUnitDetails = new HouseUnitDetails { UnitType = HouseUnitType.Apartment };

            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<HouseUnit>())).ThrowsAsync(new DbUpdateException());

            // Act
            var result = await _service.AddAsync(houseUnitDetails);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.Error.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenDbUpdateExceptionThrown_ReturnsFailure()
        {
            // Arrange
            var houseUnitDetails = new HouseUnitDetails { UnitType = HouseUnitType.Apartment };

            _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<HouseUnit>())).ThrowsAsync(new DbUpdateException());

            // Act
            var result = await _service.UpdateAsync(houseUnitDetails, 1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.Error.StatusCode);
        }

        [Fact]
        public async Task DeleteAsync_WhenKeyNotFoundExceptionThrown_ReturnsNotFound()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.DeleteAsync(1)).ThrowsAsync(new KeyNotFoundException("HouseUnit not found."));

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.NotFound, result.Error.StatusCode);
        }

        [Fact]
        public async Task DeleteAsync_WhenDbUpdateExceptionThrown_ReturnsFailure()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.DeleteAsync(1)).ThrowsAsync(new DbUpdateException());

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.Error.StatusCode);
        }
    }
}
