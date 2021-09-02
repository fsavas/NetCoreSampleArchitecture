using System.ComponentModel.DataAnnotations;

namespace Sample.Web.Core.Mvc.ModelValidation
{
    public class IdValidationAttribute : ValidationAttribute
    {
        #region Methods

        public string GetErrorMessage(string id) =>
            $"! Id < {id} ";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            long modelId;

            if (long.TryParse(value.ToString(), out modelId))
            {
                if (modelId < 0)
                    return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            else
                return new ValidationResult(GetErrorMessage(value.ToString()));

            return ValidationResult.Success;
        }

        #endregion Methods
    }
}