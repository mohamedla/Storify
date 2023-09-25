using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Reflection;
using System.Web.Http.Controllers;

namespace StorifyAPI.ModelBinders
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync (ModelBindingContext bindingContext)
        {
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var value = bindingContext.ValueProvider.GetValue (bindingContext.ModelName).ToString ();

            if(string.IsNullOrEmpty(value)) 
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var genericType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
            
            var converter = TypeDescriptor.GetConverter (genericType);

            var array = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim()))
                .ToArray();

            var gridArray = Array.CreateInstance(genericType, array.Length);
            array.CopyTo(gridArray, 0);
            bindingContext.Model = gridArray;

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;

        }
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }
    }
}
