using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ModularMonolith.ModelBinders
{
    public class UlidModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;
            if (Ulid.TryParse(value, out var ulid))
            {
                bindingContext.Result = ModelBindingResult.Success(ulid);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid ULID format.");
            }

            return Task.CompletedTask;
        }
    }
}
