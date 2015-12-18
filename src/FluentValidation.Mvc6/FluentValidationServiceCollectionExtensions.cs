using System;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace FluentValidation.Mvc6
{
    public static class FluentValidationServiceCollectionExtensions
    {
        public static void AddFluentValidation(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddSingleton<IValidatorFactory, ValidatorFactory>();
            services.AddSingleton<IModelValidatorProvider, FluentValidationModelValidatorProvider>();
            services.AddSingleton<ValidateModelStateAttribute>();
        }
    }
}