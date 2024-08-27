using HouseUnitAPI.Models;

namespace HouseUnitAPI.Repositories
{
    /// <summary>
    /// Interface for handling repository actions
    /// </summary>
    public interface IHouseUnitRepository
    {
        /// <summary>
        /// Gets all house units in the database.
        /// </summary>
        /// <returns>List of registered houseunits.</returns>
        Task<IEnumerable<HouseUnit>> GetAllAsync();

        /// <summary>
        /// Gets a specific house unit
        /// </summary>
        /// <param name="id">Primary key of the specific house unit</param>
        /// <returns>Details of the HouseUnit</returns>
        Task<HouseUnit> GetByIdAsync(int id);

        /// <summary>
        /// Register a houseunit
        /// </summary>
        /// <param name="houseUnit">Details of the houseUnit</param>
        /// <returns>Details of the registered houseunit</returns>
        Task<HouseUnit> AddAsync(HouseUnit houseUnit);

        /// <summary>
        /// Update a houseunit
        /// </summary>
        /// <param name="houseUnit">updated details of the houseUnit</param>
        /// <returns>Details of the updated houseunit</returns>
        Task<HouseUnit> UpdateAsync(HouseUnit houseUnit);

        /// <summary>
        /// Delete a house unit
        /// </summary>
        /// <param name="id">Primary key of the specific house unit</param>
        Task DeleteAsync(int id);
    }
}
