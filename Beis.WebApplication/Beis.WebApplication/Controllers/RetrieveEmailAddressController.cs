
namespace Beis.WebApplication.Controllers
{
    public class RetrieveEmailAddressController : BaseController<EmailAddressViewModel>
    {
        public RetrieveEmailAddressController(ILogger<ApplicantEmailAddressController> logger, IHttpContextAccessor httpContextAccessor, ISessionService sessionService) : base(logger, httpContextAccessor, sessionService)
        {
        }

        [HttpGet(RoutePaths.RetrieveEmailAddressPage, Name = RouteNames.RetrieveEmailAddressPage)]
        public IActionResult Index()
        {
            var model = base.LoadDtoAndModelFromSession();
            return View(model);
        }
        
        [HttpPost(RoutePaths.RetrieveEmailAddressPage, Name = RouteNames.RetrieveEmailAddressPage)]
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