using APMLibrary.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace APMLibrary.Web.ViewComponents
{
    [ViewComponent]
    public partial class CatalogViewComponent : ViewComponent
    {
        public CatalogViewComponent() : base() { }

        public virtual async Task<IViewComponentResult> InvokeAsync(string title, string href)
        {
            this.ViewBag.RedirectLink = href;
            this.ViewBag.CatalogTitle = title;
            var viewModel = new BookCatalogViewModel() 
            {
                Items = new List<CatalogItemViewModel>()
                {
                    new ()
                    {
                        FirstLine = "Название книги 1",
                        LastLine = "Имя автора 1",
                    },
                    new ()
                    {
                        FirstLine = "Название книги 2",
                        LastLine = "Имя автора 2",
                        Rating = 4.5,
                    },
                    new ()
                    {
                        FirstLine = "Название книги 2",
                        LastLine = "Имя автора 2",
                        Rating = 4.5,
                    },
                    new ()
                    {
                        FirstLine = "Название книги 1",
                        LastLine = "Имя автора 1",
                        Rating = 3.5,
                    },
                    new ()
                    {
                        FirstLine = "Название книги 2",
                        LastLine = "Имя автора 2",
                        Rating = 4.5,
                    },
                    new ()
                    {
                        FirstLine = "Название книги 1",
                        LastLine = "Имя автора 1",
                        Rating = 3.5,
                    },
                    new ()
                    {
                        FirstLine = "Название книги 2",
                        LastLine = "Имя автора 2",
                        Rating = 4.5,
                    },
                }
            };
            return this.View(viewModel);
        }

    }
}
