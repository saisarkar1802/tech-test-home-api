using AutoMapper.Features;
using System.ComponentModel.DataAnnotations;
using static HouseUnitAPI.Helpers.EnumHelper;

namespace HouseUnitAPI.Helpers.CustomValidationAttributes
{
    public class AllowedUnitTypeValuesAttribute : ValidationAttribute
    {
        public AllowedUnitTypeValuesAttribute()
        {
            
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is HouseUnitType houseUnit)
            {
                if (!Enum.IsDefined(typeof(HouseUnitType), houseUnit))
                {
                    return new ValidationResult($"Invalid value '{houseUnit}' in {validationContext.DisplayName}.");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult($"{validationContext.DisplayName} is not a valid array of FeatureType.");
        }
    }
}
