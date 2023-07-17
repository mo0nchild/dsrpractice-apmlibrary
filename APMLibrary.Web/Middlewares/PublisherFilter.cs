using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Requests.PublisherRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace APMLibrary.Web.Middlewares
{
    public partial class PublisherFilter : Attribute, IAsyncPageFilter
    {
        private readonly IMediator mediator = default!;
        public PublisherFilter(IMediator mediator) : base() => this.mediator = mediator;

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var profileId = int.Parse(context.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid));
            var bookId = context.HttpContext.Request.Query["BookId"];

            var publisherInfo = await this.mediator.Send(new GetPublisherInfoRequest(profileId));
            if (int.TryParse(bookId, out var id) && publisherInfo.FirstOrDefault(item => item.Id == id) == null)
            {
                context.Result = new BadRequestObjectResult("Данная публикаций не принадлежит издателю");
                return;
            }
            await next.Invoke();
        }
        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context) => Task.CompletedTask;
    }
}
