﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ModularMonolith.ModelBinders
{
    public class UlidModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(Ulid))
            {
                return new BinderTypeModelBinder(typeof(UlidModelBinder));
            }

            return null;
        }
    }
}
