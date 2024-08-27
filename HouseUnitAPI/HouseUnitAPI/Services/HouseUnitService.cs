using AutoMapper;
using HouseUnitAPI.Helpers.ExceptionHandling;
using HouseUnitAPI.Models;
using HouseUnitAPI.Repositories;
using HouseUnitAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;
using static HouseUnitAPI.Helpers.EnumHelper;

namespace HouseUnitAPI.Services
{
    /// <summary>
    /// Class for handling house unit service actions
    /// </summary>
    public class HouseUnitService : IHouseUnitService
    {
        private readonly IHouseUnitRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<HouseUnitService> _logger;

        public HouseUnitService(IMapper mapper, IHouseUnitRepository repository, ILogger<HouseUnitService> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Gets all house units in the database.
        /// </summary>
        /// <returns>List of registered houseunits.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the HouseUnits property is null</exception>
        public async Task<ReturnResult<IEnumerable<ViewHouseUnit>>> GetAllAsync()
        {
            try
            {
                var houseUnitEntity = await _repository.GetAllAsync();
                return ReturnResult<IEnumerable<ViewHouseUnit>>.Success(_mapper.Map<IEnumerable<ViewHouseUnit>>(houseUnitEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while fetching house unit data");
                return ReturnResult<IEnumerable<ViewHouseUnit>>.Failure("An error occured while fetching house unit data", (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets a specific house unit
        /// </summary>
        /// <param name="id">Primary key of the specific house unit</param>
        /// <returns>Details of the HouseUnit</returns>
        public async Task<ReturnResult<HouseUnitDetails>> GetByIdAsync(int id)
        {
            try
            {
                var houseUnitEntity = await _repository.GetByIdAsync(id);
                if(houseUnitEntity == null)
                {
                    return ReturnResult<HouseUnitDetails>.Failure($"no house unit found with {id}", (int)HttpStatusCode.NotFound);
                }
                return ReturnResult<HouseUnitDetails>.Success(_mapper.Map<HouseUnitDetails>(houseUnitEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while fetching house unit data");
                return ReturnResult<HouseUnitDetails>.Failure("An error occured while fetching house unit data", (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Register a houseunit
        /// </summary>
        /// <param name="houseUnitDetails">Details of the houseUnit</param>
        /// <returns>Details of the registered houseunit</returns>
        /// <exception cref="DbUpdateException">Error occured while adding record to the database.</exception>
        public async Task<ReturnResult<ResponseViewHouseUnit>> AddAsync(HouseUnitDetails houseUnitDetails)
        {
            try
            {
                var houseUnitEntity = _mapper.Map<HouseUnit>(houseUnitDetails);
                var addedHouseUnit = await _repository.AddAsync(houseUnitEntity);
                return ReturnResult<ResponseViewHouseUnit>.Success(_mapper.Map<ResponseViewHouseUnit>(addedHouseUnit)); 
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error occured while adding new house unit details");
                return ReturnResult<ResponseViewHouseUnit>.Failure("Error occured while adding new house unit details", (int)HttpStatusCode.InternalServerError);
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "Error occured while adding new house unit details");
                return ReturnResult<ResponseViewHouseUnit>.Failure("Error occured while adding new house unit details", (int)HttpStatusCode.InternalServerError);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occured while adding new house unit details");
                return ReturnResult<ResponseViewHouseUnit>.Failure("Error occured while adding new house unit details", (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update a houseunit
        /// </summary>
        /// <param name="houseUnitDetails">updated details of the houseUnit</param>
        /// <returns>Details of the updated houseunit</returns>
        /// <exception cref="DbUpdateException">"Error occured while updating house unit details"</exception>
        public async Task<ReturnResult<ResponseViewHouseUnit>> UpdateAsync(HouseUnitDetails houseUnitDetails, int id)
        {
            try
            {
                var houseUnitEntity = _mapper.Map<HouseUnit>(houseUnitDetails);
                houseUnitEntity.Id = id;
                var updatedHouseUnit = await _repository.UpdateAsync(houseUnitEntity);
                return ReturnResult<ResponseViewHouseUnit>.Success(_mapper.Map<ResponseViewHouseUnit>(updatedHouseUnit));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Error occured while updating house unit details for houseunit id {id}");
                return ReturnResult<ResponseViewHouseUnit>.Failure($"Error occured while updating house unit details for houseunit id {id}", (int)HttpStatusCode.InternalServerError);
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Error occured while updating house unit details for houseunit id {id}");
                return ReturnResult<ResponseViewHouseUnit>.Failure($"Error occured while updating house unit details for houseunit id {id}", (int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured while updating house unit details for houseunit id {id}");
                return ReturnResult<ResponseViewHouseUnit>.Failure($"Error occured while updating house unit details for houseunit id {id}", (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete a house unit
        /// </summary>
        /// <param name="id">Primary key of the specific house unit</param>
        /// <exception cref="KeyNotFoundException">Error occured if houseunit not found with the specific id</exception>
        public async Task<ReturnResult<bool>> DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return ReturnResult<bool>.Success(true);
            }
            catch(KeyNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                return ReturnResult<bool>.Failure(ex.Message,(int)HttpStatusCode.NotFound);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Error occured while deleting hous unit with id {id}");
                return ReturnResult<bool>.Failure($"Error occured while deleting hous unit with id {id}", (int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured while deleting hous unit with id {id}");
                return ReturnResult<bool>.Failure($"Error occured while deleting hous unit with id {id}", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
