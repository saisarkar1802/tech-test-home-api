using HouseUnitAPI.Helpers.ExceptionHandling;
using HouseUnitAPI.Models;
using HouseUnitAPI.ViewModels;

namespace HouseUnitAPI.Services
{
    /// <summary>
    /// Interface for handling service actions
    /// </summary>
    public interface IHouseUnitService
    {
        // <summary>
        /// Gets all house units in the database.
        /// </summary>
        /// <returns>List of registered houseunits.</returns>
        Task<ReturnResult<IEnumerable<ViewHouseUnit>>> GetAllAsync();

        /// <summary>
        /// Gets a specific house unit
        /// </summary>
        /// <param name="id">Primary key of the specific house unit</param>
        /// <returns>Details of the HouseUnit</returns>
        Task<ReturnResult<HouseUnitDetails>> GetByIdAsync(int id);

        /// <summary>
        /// Register a houseunit
        /// </summary>
        /// <param name="houseUnitDetails">Details of the house unit.</param>
        /// <returns>Details of the registered houseunit</returns>
        Task<ReturnResult<ResponseViewHouseUnit>> AddAsync(HouseUnitDetails houseUnitDetails);

        /// <summary>
        /// Update a houseunit
        /// </summary>
        /// <param name="houseUnitDetails">Updated details of the house unit.</param>
        /// <param name="id">Id of the houseUnit to be updated.</param>
        /// <returns>Details of the updated house unit.</returns>
        Task<ReturnResult<ResponseViewHouseUnit>> UpdateAsync(HouseUnitDetails houseUnitDetails, int id);

        /// <summary>
        /// Delete a house unit.
        /// </summary>
        /// <param name="id">Primary key of the specific house unit.</param>
        Task <ReturnResult<bool>> DeleteAsync(int id);
    }
}
