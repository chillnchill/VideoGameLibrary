using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VideoGameLibrary.ModelBinders
{
    public class DecimalModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));

            }

            if (context.Metadata.ModelType == typeof(decimal)
                || context.Metadata.ModelType == typeof(decimal?))
            {
                return new DecimalModelBinder();
            }

            //this returns null (you can see the modelbinder provider is nullable) because if the value is not there it goes
            //to the next
            return null;
        }
    }
}
