using APMLibrary.Bll.Requests.SupportRequests;
using APMLibrary.Web.ViewModels.SupportViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APMLibrary.Web.Pages.LibraryPages
{
    public partial class LibraryCatalogModel : PageModel
    {
        protected readonly IMediator _mediator = default!;
        protected readonly IMapper _mapper = default!;

        public LibraryCatalogModel(IMapper mapper, IMediator mediator) : base()
            => (this._mapper, this._mediator) = (mapper, mediator);

        public List<GenreViewModel> Genres { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            this.Genres = this._mapper.Map<List<GenreViewModel>>(await this._mediator.Send(new GetGenresRequest()));
            return this.Page();
        }
    }
}
