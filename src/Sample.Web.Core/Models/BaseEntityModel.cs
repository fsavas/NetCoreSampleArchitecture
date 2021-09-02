using Sample.Web.Core.Mvc.ModelValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample.Web.Core.Models
{
    public partial class BaseEntityModel : IValidatableObject
    {
        [IdValidationAttribute]
        public virtual long Id { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidationResult.Success;
        }
    }
}