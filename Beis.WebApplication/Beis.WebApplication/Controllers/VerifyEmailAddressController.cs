
using Beis.Common.Constants;
using Beis.Common.Models;
using Beis.Common.Services;
using System.Web;

namespace Beis.WebApplication.Controllers
{
    public class VerifyEmailAddressController : Controller
    {
     
        private readonly IEmailVerificationService _emailVerificationService;
        private readonly IRestoreSessionService _restoreSessionService;

        public VerifyEmailAddressController(IEmailVerificationService emailVerificationService, IRestoreSessionService restoreSessionService)
        {

            _emailVerificationService = emailVerificationService;
            _restoreSessionService = restoreSessionService;
        }

        [Route(RoutePaths.VerifyEmailAddressPage, Name = RouteNames.VerifyEmailAddressPage)]
        public async Task<IActionResult> Index(EmailVerificationModel model)
        {

            var emailAddress = HttpUtility.UrlDecode(model.EmailAddress);
            var result = await _emailVerificationService.VerifyEnterpriseFromLinkAsync(model);

            if (result.IsSuccess)
            {
                await restoreSessionFromDb(emailAddress);
                return RedirectToRoute(model.Target);
            }
                

            await _emailVerificationService.SendVerifyEmailNotificationAsync(emailAddress, model.Target);
            
            if (result.HasError<ExpiredCodeError>())
                return View("InvalidCode"); // Todo make an expired view
            
            return View("InvalidCode");
           
        }

        private async Task restoreSessionFromDb(string emailAddress)
        {
            await _restoreSessionService.RestoreSessionFromDb(emailAddress);
        }
    }
}