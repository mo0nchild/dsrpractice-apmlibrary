using APMLibrary.Bll.Commands.SupportCommand;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Requests.SupportRequests;
using APMLibrary.Web.ViewModels.SupportViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using APMLibrary.Web.Middlewares;

namespace APMLibrary.Web.Pages.AdminPages
{
    [Authorize(Policy = "Admin"), ValidateAntiForgeryToken, ModelStateFilter]
    public partial class SupportSettingsModel : PageModel
    {
        protected readonly IMediator _mediator = default!;
        protected readonly IMapper _mapper = default!;

        public SupportSettingsModel(IMapper mapper, IMediator mediator) : base()
            => (this._mapper, this._mediator) = (mapper, mediator);

        public List<LanguageViewModel> Languages { get; set; } = new();
        public List<GenreViewModel> Genres { get; set; } = new();
        public List<CategoryViewModel> Categories { get; set; } = new();
        public List<BookTypeViewModel> BookTypes { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            this.Languages = this._mapper.Map<List<LanguageViewModel>>(await this._mediator.Send(new GetLanguagesRequest()));
            this.Genres = this._mapper.Map<List<GenreViewModel>>(await this._mediator.Send(new GetGenresRequest()));

            this.Categories = this._mapper.Map<List<CategoryViewModel>>(await this._mediator.Send(new GetCategoriesRequest()));
            this.BookTypes = this._mapper.Map<List<BookTypeViewModel>>(await this._mediator.Send(new GetBookTypesRequest()));

            return this.Page();
        } 

        protected virtual async Task<IActionResult> SupportPostAction(object supportRequest)
        {
            if (!this.ModelState.IsValid) return this.Page();

            try { await this._mediator.Send(supportRequest); }
            catch (CollisionException requestError)
            {
                this.ModelState.AddModelError("", requestError.Message);
                this.TempData["ModelState"] = ModelStateFilter.GetState(this.ModelState);

                return this.RedirectToPage("/AdminPages/SupportSettings");
            }
            return this.RedirectToPage("/AdminPages/SupportSettings");
        }

        public async Task<IActionResult> OnPostLanguageAsync([FromForm, Required] string languageName)
        {
            return await this.SupportPostAction(new LanguageAddCommand() { Name = languageName });
        }
        public async Task<IActionResult> OnPostCategoryAsync([FromForm, Required] string categoryName)
        {
            return await this.SupportPostAction(new CategoryAddCommand() { Name = categoryName });
        }

        public async Task<IActionResult> OnPostGenreAsync([FromForm, Required] string genreName,
            [FromForm, Required] string categoryName)
        {
            return await this.SupportPostAction(new GenreAddCommand() 
            {
                Category = categoryName, Name = genreName, 
            });
        }

        public async Task<IActionResult> OnPostBookTypeAsync([FromForm, Required] string typeName)
        {
            return await this.SupportPostAction(new BookTypeAddCommand() { Name = typeName });
        }

        public async Task<IActionResult> OnGetDeleteAsync(int id, SupportType type)
        {
            if (!this.ModelState.IsValid) return this.Page();

            await this._mediator.Send(new SupportDeleteCommand() { SupportId = id, SupportType = type });
            return this.RedirectToPage("/AdminPages/SupportSettings");
        }
    }
}
