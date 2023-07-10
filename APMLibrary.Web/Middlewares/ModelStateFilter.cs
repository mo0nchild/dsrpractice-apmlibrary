using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace APMLibrary.Web.Middlewares
{
    using ModelStateCollection = List<KeyValuePair<string, string>>;
    public partial class ModelStateFilter : Attribute, IAsyncPageFilter
    {
        public ModelStateFilter() : base() { }

        public virtual async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, 
            PageHandlerExecutionDelegate next)
        {
            var pageModel = (context.HandlerInstance as PageModel);
            if (pageModel != null && pageModel.TempData.ContainsKey("ModelState") && pageModel.TempData["ModelState"] != null)
            {
                var tempData = JsonConvert.DeserializeObject<ModelStateCollection>(pageModel.TempData["ModelState"]!.ToString()!);
                if (tempData != null)
                {
                    foreach(var item in tempData) pageModel.ModelState.AddModelError(item.Key, item.Value);
                }
            }
            await next.Invoke();
        }
        public virtual Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context) => Task.CompletedTask;

        public static string GetState(ModelStateDictionary modelState)
        {
            var resultCollection = new ModelStateCollection();
            foreach(var state in modelState)
            {
                if (state.Value.ValidationState == ModelValidationState.Valid) continue;
                foreach(var error in state.Value.Errors)
                {
                    resultCollection.Add(new(state.Key, error.ErrorMessage));
                }
            }
            return JsonConvert.SerializeObject(resultCollection);
        }
    }
}
