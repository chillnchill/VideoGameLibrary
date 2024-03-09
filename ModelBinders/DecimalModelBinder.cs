using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace VideoGameLibrary.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            //ValueProviderResult returns the first available value, it cannot be null but might be none, this is why 
            //we also check the IsNullOrEmpty of the firstValue
            if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
            {
                decimal result = 0M;
                bool isConvertSuccessful = false;

                try
                {
                    string value = valueResult.FirstValue.Trim();
                    value = value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    value = value.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    result = Convert.ToDecimal(value, CultureInfo.CurrentCulture);
                    isConvertSuccessful = true;
                }
                catch (FormatException ex)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex, bindingContext.ModelMetadata);
                }

                if (isConvertSuccessful)
                {
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
            }

            return Task.CompletedTask;
        }
    }
}
