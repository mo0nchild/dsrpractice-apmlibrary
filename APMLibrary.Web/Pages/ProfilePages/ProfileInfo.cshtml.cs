using APMLibrary.Bll.Commands.ProfileCommands;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Requests;
using APMLibrary.Bll.Requests.ProfileRequests;
using APMLibrary.Web.ViewComponents;
using APMLibrary.Web.ViewModels.ProfileViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace APMLibrary.Web.Pages.ProfilePages
{
    [ValidateAntiForgeryToken, Authorize(Policy = "User")]
    public class ProfileInfoModel : PageModel
    {
        protected readonly IMediator _mediator = default!;
        protected readonly IMapper _mapper = default!;

        protected int ProfileId { get => int.Parse(this.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid)); }

        public ProfileInfoModel(IMapper mapper, IMediator mediator) : base()
            => (this._mapper, this._mediator) = (mapper, mediator);

        [FromForm, BindProperty(Name = "ProfileModel")]
        public EditProfileViewModel ProfileModel { get; set; } = default!;
        public byte[]? CurrentImage { get; set; } = default!;

        [BindProperty(Name = "ErrorMessage", SupportsGet = true)]
        public string? ErrorMessage { get; set; } = null;

        public async Task<IActionResult> OnGetAsync()
        {
            var requestResult = await this._mediator.Send(new GetProfileRequest(this.ProfileId));
            if (requestResult == null) return this.RedirectToRoute("logout");

            this.ProfileModel = new EditProfileViewModel()
            {
                Email = requestResult.Email,
                Name = requestResult.Name,
                Surname = requestResult.Surname,
                Phone = requestResult.Phone,
            };
            this.CurrentImage = requestResult.Image;
            return this.Page();
        }

        public async Task<IActionResult> OnGetDeleteAsync([FromServices] ILogger<ProfileInfoModel> logger)
        {
            try { await this._mediator.Send(new DeleteProfileCommand(this.ProfileId)); }
            catch(NotFoundException requestError)
            {
                logger.LogError(requestError.Message);
            }
            return this.RedirectToRoute("logout");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!this.ModelState.IsValid) return this.Page();

            var mappingModel = this._mapper.Map<EditProfileCommand>(this.ProfileModel);
            mappingModel.Id = this.ProfileId;

            try { await this._mediator.Send(mappingModel); }
            catch(NotFoundException resultError)
            {
                this.ModelState.AddModelError("", resultError.Message);
                this.ErrorMessage = resultError.Message;
                return this.Page();
            }
            return this.RedirectToPage("ProfileInfo");
        }

        public async Task<IActionResult> OnPostPasswordAsync([FromForm] string oldPassword, [FromForm] string newPassword)
        {
            try { 
                await this._mediator.Send(new ChangePasswordCommand()
                {
                    Id = this.ProfileId,
                    NewPassword = newPassword,
                    OldPassword = oldPassword,
                }); 
            }
            catch (NotFoundException resultError)
            {
                this.ModelState.AddModelError("", resultError.Message);
                return this.RedirectToPage("ProfileInfo", new { ErrorMessage = resultError.Message });
            }
            return this.RedirectToPage("ProfileInfo");
        }

        public async Task<IActionResult> OnPostImageAsync([FromForm] IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length <= 0)
            {
                this.ModelState.AddModelError("CurrentImage", "Некорректное изображение");
                return this.RedirectToPage("ProfileInfo", new { ErrorMessage = "Некорректное изображение" });
            }
            var request = new ChangeImageCommand() { Id = this.ProfileId, Image = new byte[imageFile.Length] };
            using (var fileStream = imageFile.OpenReadStream())
            {
                await fileStream.ReadAsync(request.Image, 0, (int)imageFile.Length);
            }
            try { await this._mediator.Send(request); }
            catch (NotFoundException resultError)
            {
                this.ModelState.AddModelError("", resultError.Message);
                return this.RedirectToPage("ProfileInfo", new { ErrorMessage = resultError.Message });
            }
            return this.RedirectToPage("ProfileInfo");
        }
    }
}
