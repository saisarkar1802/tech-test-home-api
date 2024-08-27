using HouseUnitAPI.Helpers.ExceptionHandling;
using HouseUnitAPI.Models;
using HouseUnitAPI.Services;
using HouseUnitAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HouseUnitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseUnitController : ControllerBase
    {
        private readonly IHouseUnitService _houseUnitService;

        public HouseUnitController(IHouseUnitService houseUnitService)
        {
            _houseUnitService = houseUnitService;
        }

        /// <summary>
        /// Get all house units
        /// </summary>
        /// <permission">All users can access</permission>
        /// <returns>List of available house units</returns>
        [HttpGet]
        [Authorize(Policy = "USER")]
        [SwaggerOperation(OperationId = "GetAllHouseUnits", Summary = "Gets all house units")]
        [SwaggerResponse(200, "Returns the list of house units", typeof(IEnumerable<ViewHouseUnit>))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _houseUnitService.GetAllAsync();
            if(result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.Error.StatusCode, result.Error);
        }

        /// <summary>
        /// Get a specific house unit details by id
        /// </summary>
        /// <param name="id">Id of the house unit registered</param>
        /// <returns>The specific house unit with details</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "USER")]
        [SwaggerOperation(OperationId = "GetHouseUnitById", Summary = "Gets a house unit by ID")]
        [SwaggerResponse(200, "Returns the house unit details", typeof(HouseUnitDetails))]
        [SwaggerResponse(404, "House unit not found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _houseUnitService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.Error.StatusCode, result.Error);
        }

        /// <summary>
        /// Registers a house unit details
        /// </summary>
        /// <param name="houseUnitDetails">Details of the house unit to be added</param>
        /// <returns>New Added house unit with details</returns>
        [HttpPost]
        [Authorize(Policy = "ADMIN")]
        [SwaggerOperation(OperationId = "CreateHouseUnit", Summary = "Creates a new house unit")]
        [SwaggerResponse(201, "Creates house unit", typeof(ResponseViewHouseUnit))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public async Task<IActionResult> Create([FromBody] HouseUnitDetails houseUnitDetails)
        {
            if (houseUnitDetails == null)
            {
                return BadRequest();
            }
            var result = await _houseUnitService.AddAsync(houseUnitDetails);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Create), new { id = result.Data.Id }, result.Data);
            }
            return StatusCode(result.Error.StatusCode, result.Error);
        }

        /// <summary>
        /// Updates an existing house unit details
        /// </summary>
        /// <param name="id">id of the house unit to be updated</param>
        /// <param name="houseUnitDetails">Modified details of the houseunit.</param>
        /// <returns>Updated details of the house unit</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "ADMIN")]
        [SwaggerOperation(OperationId = "UpdateHouseUnit", Summary = "Updates an existing house unit")]
        [SwaggerResponse(200, "House unit updated successfully", typeof(ResponseViewHouseUnit))]
        [SwaggerResponse(404, "House unit not found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public async Task<IActionResult> Update(int id, [FromBody] HouseUnitDetails houseUnitDetails)
        {
            var result = await _houseUnitService.UpdateAsync(houseUnitDetails,id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.Error.StatusCode, result.Error);
        }

        /// <summary>
        /// Deletes an existing house unit
        /// </summary>
        /// <param name="id">Id of the house unit to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "ADMIN")]
        [SwaggerOperation(OperationId = "DeleteHouseUnit", Summary = "Deletes a house unit")]
        [SwaggerResponse(204, "House unit deleted successfully")]
        [SwaggerResponse(404, "House unit not found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _houseUnitService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return StatusCode(result.Error.StatusCode, result.Error);
        }
    }
}
