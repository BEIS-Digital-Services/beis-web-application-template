using Beis.Common.Models;
using Beis.WebApplication.Interfaces;
using Beis.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Beis.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly ISessionService _sessionService;
		private readonly ICookieService _cookieService;

		public HomeController(ILogger<HomeController> logger, ISessionService sessionService, ICookieService cookieService)
		{
			_logger = logger;
			_sessionService = sessionService;
			_cookieService = cookieService;
		}

		[Route(RoutePaths.HomePage, Name = RouteNames.HomePage)]
		public IActionResult Index()
        {
			var cookieBannerViewModel = _cookieService.SyncCookieSelection(ControllerContext.HttpContext.Request, new CookieBannerViewModel());
			var dto = new WebApplicationDto();
			_sessionService.Remove(nameof(WebApplicationDto), ControllerContext.HttpContext);
			dto.CookieBannerViewModel = cookieBannerViewModel;
			_sessionService.Set(nameof(WebApplicationDto), dto, ControllerContext.HttpContext);

			return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

		[Route(RoutePaths.CookieSettingsPage, Name = RouteNames.CookieSettingsPage)]
		public IActionResult Cookies()
		{
			var dto = _sessionService.Get<WebApplicationDto>(nameof(WebApplicationDto), ControllerContext.HttpContext);

			return View(dto.CookieBannerViewModel);
		}

		public async Task<IActionResult> ProcessCookie(string controllerName, string actionName, string cookieType, bool? isAccept)
		{
			var cookieProcessed = await _cookieService.ProcessCookie(cookieType, isAccept, ControllerContext.HttpContext.Response);
			var dto = _sessionService.Get<WebApplicationDto>(nameof(WebApplicationDto), ControllerContext.HttpContext);

			if (cookieType == "act")
			{
				dto.CookieBannerViewModel.IsCookieProcessed = cookieProcessed;
				dto.CookieBannerViewModel.GoogleAnalyticsCookieAccepted = isAccept.HasValue ? isAccept.Value ? "true" : "false" : "";
				dto.CookieBannerViewModel.MarketingCookieAccepted = isAccept.HasValue ? isAccept.Value ? "true" : "false" : "";
				dto.CookieBannerViewModel.IsAllCookieAccepted = isAccept.HasValue && isAccept.Value;
				dto.CookieBannerViewModel.IsBannerClosed = true;

				_sessionService.Set(nameof(WebApplicationDto), dto, ControllerContext.HttpContext);

			

				return RedirectToAction(actionName, controllerName);
			}

			if (cookieType == "close")
			{
				dto.CookieBannerViewModel.IsBannerClosed = true;
			}

			_sessionService.Set(nameof(WebApplicationDto), dto, ControllerContext.HttpContext);


			return RedirectToAction(actionName, controllerName);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SaveCookiesPreferences(CookieBannerViewModel viewModel)
		{
			var cookieProcessed = await _cookieService.SaveCookiesPreferences(HttpContext);
			var dto = _sessionService.Get<WebApplicationDto>(nameof(WebApplicationDto), ControllerContext.HttpContext);
			viewModel.IsAllCookieAccepted = viewModel.IsGoogleAnalyticsCookieAccepted && viewModel.IsMarketingCookieAccepted;
			viewModel.IsCookieProcessed = cookieProcessed;
			viewModel.IsBannerClosed = true;//Not required yet.
			dto.CookieBannerViewModel = viewModel;
			_sessionService.Set(nameof(WebApplicationDto), dto, ControllerContext.HttpContext);

			return RedirectToRoute(RouteNames.CookieSettingsPage, RoutePaths.CookieSettingsPage);
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}