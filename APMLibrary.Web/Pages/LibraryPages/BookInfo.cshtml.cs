using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Requests.BookRequests;
using APMLibrary.Web.ViewModels.BookViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace APMLibrary.Web.Pages.LibraryPages
{
    [ValidateAntiForgeryToken, Authorize(Policy = "User")]
    public partial class BookInfoModel : PageModel
    {
        protected readonly IMediator mediator = default!;
        protected readonly IMapper mapper = default!;

        protected int ProfileId { get => int.Parse(this.User.FindFirstValue(ClaimTypes.PrimarySid)); }
        public BookInfoModel(IMediator mediator, IMapper mapper) : base()
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public int BookId { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; } = default;

        public BookInfoViewModel BookInfo { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var requestResult = await this.mediator.Send(new GetBookRequest(this.BookId));
            if (requestResult == null) return this.BadRequest("Публикация не найдена");

            this.BookInfo = this.mapper.Map<BookInfoViewModel>(requestResult);
            return this.Page();
        }
        public record RatingRequestModel(string? Comment, double Rating, int PublishId);
        public async Task<IActionResult> OnPostAsync([FromForm] RatingRequestModel viewModel)
        {
            if (!this.ModelState.IsValid) return await this.OnGetAsync();
            try {
                await this.mediator.Send(new SetRatingCommand() 
                {
                    Id = viewModel.PublishId,
                    ReaderId = this.ProfileId,
                    Comment = viewModel.Comment,
                    Rating = (int) Math.Round(viewModel.Rating),
                });
            }
            catch(NotFoundException requestError)
            {
                this.ModelState.AddModelError("", requestError.Message);
                return await this.OnGetAsync();
            }
            return this.RedirectToPage("/LibraryPages/BookInfo", new 
            { 
                Message = "Спасибо за ваш отзыв",  BookId = viewModel.PublishId,
            });
        }

        public async Task<IActionResult> OnGetFileAsync()
        {
            var requestResult = await this.mediator.Send(new GetBookRequest(this.BookId));
            if (requestResult == null) return this.BadRequest("Публикация не найдена");
            
            return this.File(requestResult.Body, "application/octet-stream", $"{requestResult.Title}.pdf"); ;
        }
    }
}
