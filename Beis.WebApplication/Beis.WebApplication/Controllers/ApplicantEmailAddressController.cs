
namespace Beis.WebApplication.Controllers
{
    public class ApplicantEmailAddressController : BaseController<EmailAddressViewModel>
    {
        public ApplicantEmailAddressController(ILogger<ApplicantEmailAddressController> logger, IHttpContextAccessor httpContextAccessor, ISessionService sessionService) : base(logger, httpContextAccessor, sessionService)
        {
        }

        [HttpGet(RoutePaths.ApplicantEmailAddressPage, Name = RouteNames.ApplicantEmailAddressPage)]
        public IActionResult Index()
        {
            var model = base.LoadDtoAndModelFromSession();
            return View(model);
        }
        
        [HttpPost(RoutePaths.ApplicantEmailAddressPage, Name = RouteNames.ApplicantEmailAddressPage)]
        [ValidateAntiForgeryToken]

        public IActionResult Index(EmailAddressViewModel model)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.EmailAddress) || base.StoreDtoAndModelToSession(model).IsFailed) // model state validity evaluated incorrectly in unit test!
            {
                return View(nameof(Index), model);
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