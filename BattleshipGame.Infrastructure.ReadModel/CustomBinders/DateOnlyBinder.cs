using frm.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BattleshipGame.Infrastructure.ReadModel.CustomBinders;

public class DateOnlyBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }
        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }
        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
        var value = valueProviderResult.FirstValue;
        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }
        if (!value.TryFromISOShortDateStringToDateOnly(out var dateOnly))
        {
            return Task.CompletedTask;
        }
        bindingContext.Result = ModelBindingResult.Success(dateOnly);
        return Task.CompletedTask;
    }
}