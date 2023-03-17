using Microsoft.AspNetCore.Mvc;

namespace Beis.WebApplication.Controllers
{
    public class ConfirmYourEmailAddressController : BaseController<EmailAddressViewModel>
    {
        private readonly IApplicantService _applicantService;
        public ConfirmYourEmailAddressController(ILogger<ConfirmYourEmailAddressController> logger, IHttpContextAccessor httpContextAccessor, ISessionService sessionService, IApplicantService applicantService) : base(logger, httpContextAccessor, sessionService)
        {
            _applicantService = applicantService;
        }

        [HttpGet(RoutePaths.ConfirmEmailAddressPage, Name = RouteNames.ConfirmEmailAddressPage)]
        public async Task<IActionResult> Index(bool resend)
        {
            var model = base.LoadDtoAndModelFromSession();
            var result = await _applicantService.LoadAplicantAndGenerateVerificationLinkAsync(dto);
            return result.IsFailed ? View("Error") : View(model);
        }

        public override EmailAddressViewModel MapDtoToModel(EmailAddressViewModel model)
        {
            model.EmailAddress = dto.ApplicantEmailAddress;
            return model;
        }

        public override void MapModelToDto(EmailAddressViewModel model)
        {
            dto.ApplicantEmailAddress = model.EmailAddress;
        }
    }
}
