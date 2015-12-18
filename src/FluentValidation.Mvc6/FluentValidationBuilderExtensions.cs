using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace FluentValidation.Mvc6
{
    public static class FluentValidationBuilderExtensions
    {
        public static IApplicationBuilder UseFluentValidation(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var options = app.ApplicationServices.GetService<IOptions<MvcOptions>>();

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var modelValidatorProvider = app.ApplicationServices.GetService<IModelValidatorProvider>();

            if (modelValidatorProvider == null)
            {
                throw new ArgumentNullException(nameof(modelValidatorProvider));
            }

            options.Value.ModelValidatorProviders.Add(modelValidatorProvider);
            options.Value.Filters.Add(new ValidateModelStateAttribute());
            return app;
        }
    }
}