using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;

namespace FluentValidation.Mvc6
{
    public class FluentValidationModelValidator : IModelValidator
    {
        private readonly IValidator _validator;

        public FluentValidationModelValidator(IValidator validator)
        {
            _validator = validator;
        }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var result = _validator.Validate(context.Model);

            return from error in result.Errors
                   select new ModelValidationResult(error.PropertyName, error.ErrorMessage);

        }

        public bool IsRequired => false;
    }
}