using HouseUnitAPI.Data;
using HouseUnitAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseUnitAPI.Repositories
{
    /// <summary>
    /// Class for handling house unit repository actions
    /// </summary>
    public class HouseUnitRepository : IHouseUnitRepository
    {
        private readonly HouseUnitDbContext _context;

        public HouseUnitRepository(HouseUnitDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all house units in the database.
        /// </summary>
        /// <returns>List of registered houseunits.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the HouseUnits property is null</exception>
        public async Task<IEnumerable<HouseUnit>> GetAllAsync()
        {
            return await _context.HouseUnits.ToListAsync();
        }

        /// <summary>
        /// Gets a specific house unit
        /// </summary>
        /// <param name="id">Primary key of the specific house unit</param>
        /// <returns>Details of the HouseUnit</returns>
        public async Task<HouseUnit> GetByIdAsync(int id)
        {
            return await _context.HouseUnits.FindAsync(id);
        }

        /// <summary>
        /// Register a houseunit
        /// </summary>
        /// <param name="houseUnit">Details of the houseUnit</param>
        /// <returns>Details of the registered houseunit</returns>
        /// <exception cref="DbUpdateException">Error occured while adding record to the database.</exception>
        public async Task<HouseUnit> AddAsync(HouseUnit houseUnit)
        {
            _context.HouseUnits.Add(houseUnit);
            await _context.SaveChangesAsync();
            return houseUnit;
        }

        /// <summary>
        /// Update a houseunit
        /// </summary>
        /// <param name="houseUnit">updated details of the houseUnit</param>
        /// <returns>Details of the updated houseunit</returns>
        /// <exception cref="DbUpdateException">Error occured while updating record in the database.</exception>
        public async Task<HouseUnit> UpdateAsync(HouseUnit houseUnit)
        {
            _context.Entry(houseUnit).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return houseUnit;
        }

        /// <summary>
        /// Delete a house unit
        /// </summary>
        /// <param name="id">Primary key of the specific house unit</param>
        /// <exception cref="KeyNotFoundException">Error occured if houseunit not found with the specific id</exception>
        public async Task DeleteAsync(int id)
        {
            var houseUnit = await _context.HouseUnits.FindAsync(id);
            if (houseUnit != null)
            {
                _context.HouseUnits.Remove(houseUnit);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"HouseUnit with {id} not found.");
            }
        }
    }
}
