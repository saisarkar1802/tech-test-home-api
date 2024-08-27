using HouseUnitAPI.Helpers.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static HouseUnitAPI.Helpers.EnumHelper;

namespace HouseUnitAPI.ViewModels
{
    /// <summary>
    /// House unit model base data
    /// </summary>
    public class BaseHouseUnit
    {
        /// <summary>
        /// Id of the HouseUnit
        /// </summary>
        [JsonIgnore]
        public virtual int Id { get; set; }

        /// <summary>
        /// Address of the HouseUnit(Do not use postal code/city name)
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s,]*$", ErrorMessage = "Address can only contain letters, numbers, spaces and commas.")]
        public string Address { get; set; }
    }

    /// <summary>
    /// House unit model details
    /// </summary>
    public class HouseUnitDetails : BaseHouseUnit
    {
        /// <summary>
        /// Number of floors available in the unit
        /// </summary>
        [Required]
        [Range(1,int.MaxValue)]
        public int NumberOfFloors { get; set; }
        /// <summary>
        /// Type Of HouseUnit
        /// </summary>
        [Required]
        [AllowedUnitTypeValues()]
        public HouseUnitType UnitType { get; set; }
        /// <summary>
        /// Array of features available in the unit. Check 
        /// </summary>
        [Required]
        [AllowedFeatureValues()]
        public FeatureType[] Features { get; set; }
    }

    /// <summary>
    /// View model for updating house unit.
    /// </summary>
    public class ViewHouseUnit : BaseHouseUnit
    {
        public override int Id { get; set; }
    }

    /// <summary>
    /// View model for response after updating house unit.
    /// </summary>
    public class ResponseViewHouseUnit : HouseUnitDetails
    {
        public override int Id { get; set; }  
    }
}
