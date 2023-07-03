using APMLibrary.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace APMLibrary.Web.ViewComponents
{
    [ViewComponent]
    public partial class FilterViewComponent : ViewComponent
    {
        public FilterViewComponent() : base() { }

        public virtual async Task<IViewComponentResult> InvokeAsync(FilterViewModel viewModel, 
            string page, string? handler = default)
        {

            this.ViewBag.Languages = new string[] { "Русский", "Английский", "Французский" };
            this.ViewBag.Page = page;
            this.ViewBag.Handler = handler;

            

            return this.View(viewModel);
        }
    }
}
