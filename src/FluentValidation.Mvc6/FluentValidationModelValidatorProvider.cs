using Microsoft.AspNet.Mvc.ModelBinding.Validation;

namespace FluentValidation.Mvc6
{
    public class FluentValidationModelValidatorProvider : IModelValidatorProvider
    {
        public FluentValidationModelValidatorProvider(IValidatorFactory validatorFactory)
        {
            ValidatorFactory = validatorFactory;
        }

        public IValidatorFactory ValidatorFactory { get; }

        public void GetValidators(ModelValidatorProviderContext context)
        {
            var validator = CreateValidator(context);

            if (validator == null) return;

            if (!IsValidatingProperty(context))
            {
                context.Validators.Add(new FluentValidationModelValidator(validator));
            }
        }

        protected virtual IValidator CreateValidator(ModelValidatorProviderContext context)
        {
            if (IsValidatingProperty(context))
            {
                return ValidatorFactory.GetValidator(context.ModelMetadata.ContainerType);
            }
            return ValidatorFactory.GetValidator(context.ModelMetadata.ModelType);
        }

        protected virtual bool IsValidatingProperty(ModelValidatorProviderContext context)
        {
            return context.ModelMetadata.ContainerType != null && !string.IsNullOrEmpty(context.ModelMetadata.PropertyName);
        }
    }
}
