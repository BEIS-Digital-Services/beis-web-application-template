

namespace Beis.WebApplication.Controllers
{

    public class ApplicantFullNameController : BaseController<FullNameViewModel>
    {
        public ApplicantFullNameController(ILogger<ApplicantFullNameController> logger, IHttpContextAccessor httpContextAccessor, ISessionService sessionService) : base(logger, httpContextAccessor, sessionService)
        {
        }

        [HttpGet(RoutePaths.ApplicantFullNamePage, Name = RouteNames.ApplicantFullNamePage)]
        
        public IActionResult Index()
        {
            var model = base.LoadDtoAndModelFromSession();            
            return View(model);
        }

        [HttpPost(RoutePaths.ApplicantFullNamePage, Name = RouteNames.ApplicantFullNamePage)]
        [ValidateAntiForgeryToken]
        
        public IActionResult Index(FullNameViewModel model)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.Name) || base.StoreDtoAndModelToSession(model).IsFailed)
            {
                return View(nameof(Index), model);
            }


            return RedirectToRoute(RouteNames.ApplicantEmailAddressPage);
        }

        public override FullNameViewModel MapDtoToModel(FullNameViewModel model)
        {
            model.Name = dto.ApplicantName;
            return model;
        }

        public override void MapModelToDto(FullNameViewModel model)
        {
            dto.ApplicantName = model.Name;
        }
    }
}