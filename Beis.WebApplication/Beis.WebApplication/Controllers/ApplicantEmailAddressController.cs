﻿
namespace Beis.WebApplication.Controllers
{
    public class ApplicantEmailAddressController : BaseController<EmailAddressViewModel>
    {
        private readonly IApplicantService applicantService;

        public ApplicantEmailAddressController(ILogger<ApplicantEmailAddressController> logger, IHttpContextAccessor httpContextAccessor, ISessionService sessionService, IApplicantService applicantService) : base(logger, httpContextAccessor, sessionService)
        {
            this.applicantService = applicantService;
        }

        [HttpGet(RoutePaths.ApplicantEmailAddressPage, Name = RouteNames.ApplicantEmailAddressPage)]
        public IActionResult Index()
        {
            var model = base.LoadDtoAndModelFromSession();
            return View(model);
        }
        
        [HttpPost(RoutePaths.ApplicantEmailAddressPage, Name = RouteNames.ApplicantEmailAddressPage)]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Index(EmailAddressViewModel model)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.EmailAddress) || base.StoreDtoAndModelToSession(model).IsFailed) // model state validity evaluated incorrectly in unit test!
            {
                return View(nameof(Index), model);
            }
            var response = await applicantService.CreateApplicantAsync(dto);
            if(response.IsFailed)
            {
                throw new Exception(response.Errors.FirstOrDefault().Message);
            }
            return RedirectToRoute(RouteNames.ConfirmEmailAddressPage);
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