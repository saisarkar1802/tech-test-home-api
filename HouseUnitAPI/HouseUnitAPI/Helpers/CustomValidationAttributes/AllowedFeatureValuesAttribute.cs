using System;
using System.ComponentModel.DataAnnotations;
using static HouseUnitAPI.Helpers.EnumHelper;

namespace HouseUnitAPI.Helpers.CustomValidationAttributes
{
    public class AllowedFeatureValuesAttribute: ValidationAttribute
    {
        public AllowedFeatureValuesAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is FeatureType[] featureArray)
            {
                foreach (var feature in featureArray)
                {
                    if (!Enum.IsDefined(typeof(FeatureType), feature))
                    {
                        return new ValidationResult($"Invalid value '{feature}' in {validationContext.DisplayName}.");
                    }
                }
                return ValidationResult.Success;
            }
            return new ValidationResult($"{validationContext.DisplayName} is not a valid array of FeatureType.");
        }
    }
}
