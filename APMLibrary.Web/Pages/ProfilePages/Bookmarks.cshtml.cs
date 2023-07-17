using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Requests.BookRequests;
using APMLibrary.Web.ViewModels.BookViewModels;
using APMLibrary.Web.ViewModels.ProfileViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace APMLibrary.Web.Pages.ProfilePages
{
    [Authorize(Roles = "User")]
    public partial class BookmarksModel : PageModel
    {
        private readonly IMediator mediator = default!;
        private readonly IMapper mapper = default!;

        protected int ProfileId { get => int.Parse(this.User.FindFirstValue(ClaimTypes.PrimarySid)); }

        public BookmarksModel(IMediator mediator, IMapper mapper) : base()
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public IEnumerable<BookmarkViewModel> Bookmarks { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var requestResult = await this.mediator.Send(new GetBookmarksRequest(this.ProfileId));
                this.Bookmarks = this.mapper.Map<IEnumerable<BookmarkViewModel>>(requestResult);
            }
            catch (NotFoundException requestError)
            {
                return this.BadRequest(requestError.Message);
            }
            return this.Page();
        }

        public async Task<IActionResult> OnGetRemoveAsync(int bookId)
        {
            await this.mediator.Send(new DeleteBookmarkCommand()
            {
                ReaderId = this.ProfileId,
                BookId = bookId,
            });
            return this.RedirectToPage("/ProfilePages/Bookmarks");
        }
    }
}
