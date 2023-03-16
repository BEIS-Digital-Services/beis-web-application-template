using Beis.Common.Constants;

namespace Beis.WebApplication.Infrastructure
{
    public static class RouteNames
    {
        public const string HomePage = "HomePage";
        public const string Heartbeat = "Heartbeat";
        public const string PrivacyPage = "Privacy";
        public const string AccessibilityStatementPage = "AccessibilityStatement";
        public const string CookiesPage = "cookies";
        public const string CookieSettingsPage = "CookieSettingsPage";
        public const string SessionExpiredPage = "SessionExpiredPage";
        public const string ApplicantEmailAddressPage = "ApplicantEmailAddressPage";
        public const string VerifyEmailAddressPage = "VerifyEmailAddressPage";
        public const string ConfirmEmailAddressPage = "ConfirmEmailAddressPage";
        public const string MyAccountPage = "MyAccountPage";





    }

    public static class RoutePaths
    {
        public const string HomePage = "/";
        public const string Heartbeat = "heartbeat";
        public const string PrivacyPage = "privacy";
        public const string AccessibilityStatementPage = "accessibility-statement";
        public const string CookiesPage = "cookies";
		public const string CookieSettingsPage = "home/cookies";
		public const string SessionExpiredPage = "session-expired";
        public const string ApplicantEmailAddressPage = "what-is-your-email-address";
        public const string ConfirmEmailAddressPage = "confirm-your-email-address";
        public const string VerifyEmailAddressPage = CommonConstants.VerifyEmailAddressPath;
        public const string MyAccountPage = "welcome-back-to-your-account";



    }

    public static class RoutePathNameMap
    {
        public static Dictionary<string, string> Lookup = new()
        {
            {string.Empty, RouteNames.HomePage },           
        };
    }
}