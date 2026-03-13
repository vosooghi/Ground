using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace Ground.Endpoints.WebApi.ModelBinding
{
    /// <summary>
    /// It effectively turns off MVC object validation for the entire app (once controllers are added).
    /// </summary>
    public sealed class NonValidatingValidator : IObjectModelValidator
    {
        public void Validate(ActionContext actionContext, ValidationStateDictionary validationState, string prefix, object model)
        {
            foreach (var entry in actionContext.ModelState.Values)
                entry.ValidationState = ModelValidationState.Skipped;

        }
    }
}
