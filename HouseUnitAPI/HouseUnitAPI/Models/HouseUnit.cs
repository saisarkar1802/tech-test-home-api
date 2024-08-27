using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static HouseUnitAPI.Helpers.EnumHelper;
using HouseUnitAPI.Helpers.CustomValidationAttributes;
using System.Text.Json.Serialization;

namespace HouseUnitAPI.Models
{
    /// <summary>
    /// Entity model for HouseUnit
    /// </summary>
    public class HouseUnit
    {
        /// <summary>
        /// Id of the HouseUnit
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// Address of the HouseUnit(Do not use postal code/city name)
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Number of floors available in the unit
        /// </summary>
        public int NumberOfFloors { get; set; }
        /// <summary>
        /// Type Of HouseUnit
        /// </summary>
        public string UnitType { get; set; }
        /// <summary>
        /// Array of features available in the unit
        /// </summary>
        public string[] Features { get; set; }
    }
}
