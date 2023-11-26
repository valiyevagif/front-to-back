using Bigon.Infrastructure.Exceptions;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.Infrastructure.Middlewares
{
    public class ValidatorInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                var errors = result.Errors.GroupBy(m => m.PropertyName).ToDictionary(k => k.Key, v => v.Select(m => m.ErrorMessage));

                throw new BadRequestException("BAD_DATA", errors);
            }

            return result;
        }

        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }
    }
}
